using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.Exceptions
{
    public class IllegalTokenException : Exception
    {
        private string error;
        private Exception innerException;

        public IllegalTokenException()
        {
        }

        public IllegalTokenException(string msg):base(msg)
        {
            error = msg;
        }

        public IllegalTokenException(string msg, Exception innerException):base(msg, innerException)
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
