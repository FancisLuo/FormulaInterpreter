using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.Exceptions
{
    public class ParameterNotFoundException: Exception
    {
        private string error;
        private Exception innerException;

        public ParameterNotFoundException()
        {
        }

        public ParameterNotFoundException(string msg) : base(msg)
        {
            error = msg;
        }

        public ParameterNotFoundException(string msg, Exception innerException) :
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
