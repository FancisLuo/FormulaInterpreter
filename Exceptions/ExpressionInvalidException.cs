using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.Exceptions
{
    public class ExpressionInvalidException : Exception
    {
        private string error;
        private Exception innerException;

        public ExpressionInvalidException()
        {
        }

        public ExpressionInvalidException(string msg) : base(msg)
        {
            error = msg;
        }

        public ExpressionInvalidException(string msg, Exception innerException) :
            base(msg, innerException)
        {
            error = msg;
            this.innerException = innerException;
        }

        public string GetError()
        {
            return error;
        }
    }
}
