using System;
using System.Collections.Generic;
using System.Text;
using AG.Utilities.ErrorHandling;

namespace AG.JsonExtensions
{
    public class JsonFileErrorArgs : ErrorDetailsArgs<Exception>
    {
        public readonly ErrorType ErrorType;

        public JsonFileErrorArgs(ErrorType errorType, string errorMessage, Exception threwnException)
            : base(errorMessage, threwnException)
        {
            ErrorType = errorType;
        }
    }
}
