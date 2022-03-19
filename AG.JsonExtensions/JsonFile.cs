using Newtonsoft.Json;
using System;
using System.IO;
using AG.PathStringOperations;
using AG.Utilities.ErrorHandling;

namespace AG.JsonExtensions
{
    public class JsonFile<TJsonTypeClass> : ErrorNotifier<Exception, JsonFileErrorArgs>, IFileSaver<TJsonTypeClass>
        where TJsonTypeClass : new()
    {
        public TJsonTypeClass Load(string fileName, bool dontCreateDefaultSettings = false)
        {
            return Load(fileName, null, dontCreateDefaultSettings);
        }

        public TJsonTypeClass Load(string fileName, JsonSerializerSettings jsonSerializerSettings, bool dontCreateDefaultSettings = false)
        {
            TJsonTypeClass deserializedObj = default(TJsonTypeClass);
            tryAgain:
            ActionWithFileAfterBackupResult actionWithFileAfterBackupResult = ActionWithFileAfterBackupResult.NoActionPerformed;
            try
            {
                actionWithFileAfterBackupResult = OperationsWithBackingUpFile.ActionWithFileAfterBackUp(fileName, (fName) =>
                {
                    using (StreamReader sr = new StreamReader(fName))
                    {
                        var fileContent = sr.ReadToEnd();
                        deserializedObj = JsonConvert.DeserializeObject<TJsonTypeClass>(fileContent, jsonSerializerSettings);
                    }
                });
                if (deserializedObj == null && !dontCreateDefaultSettings)
                {
                    deserializedObj = new TJsonTypeClass();
                }
            }
            catch (Exception thrownException)
            {
                string errorMessage = string.Format("Can't load '{0}' from a file \"{1}\". Reason: {2}\r\nPerformed action: {3}",
                    typeof(TJsonTypeClass), fileName, thrownException.Message, actionWithFileAfterBackupResult);
                var errorAction = NotifyAboutError(ErrorType.Loading, errorMessage, thrownException);
                if (errorAction == ErrorActions.Retry)
                {
                    goto tryAgain;
                }
                if (!dontCreateDefaultSettings)
                {
                    deserializedObj = new TJsonTypeClass();
                }
            }
            return deserializedObj;
        }

        public void Save(string fileName, TJsonTypeClass obj)
        {
            Save(fileName, obj, null);
        }

        public void Save(string fileName, TJsonTypeClass obj, JsonSerializerSettings jsonSerializerSettings)
        {
            tryAgain:
            try
            {
                OperationsWithBackingUpFile.ActionWithFileWithBackingUp(fileName, (fName) =>
                {
                    using (StreamWriter sw = new StreamWriter(fName, false))
                    {
                        string json = JsonConvert.SerializeObject(obj, jsonSerializerSettings);
                        sw.Write(json);
                    }
                });
            }
            catch (Exception thrownException)
            {
                string errorMessage = string.Format("Can't save '{0}' to a file \"{1}\". Reason: {2}", typeof(TJsonTypeClass), fileName, thrownException.Message);
                var errorAction = NotifyAboutError(ErrorType.Saving, errorMessage, thrownException);
                if (errorAction == ErrorActions.Retry)
                {
                    goto tryAgain;
                }
            }
        }

        protected virtual ErrorActions NotifyAboutError(ErrorType errorType, string errorMessage, Exception threwnException)
        {
            return NotifyAboutError(new JsonFileErrorArgs(errorType, errorMessage, threwnException));
        }
    }
}
