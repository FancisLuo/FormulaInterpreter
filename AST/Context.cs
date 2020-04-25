using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.AST
{
    public class Context
    {
        private Dictionary<string, double> parameterMap = new Dictionary<string, double>();

        public Context()
        {
            parameterMap.Clear();
        }

        public void AddParameter(string paramKey, double paramValue)
        {
            if(string.IsNullOrEmpty(paramKey))
            {
                throw new ArgumentNullException(nameof(paramKey));
            }

            if(parameterMap.ContainsKey(paramKey))
            {
                parameterMap[paramKey] = paramValue;
            }
            else
            {
                parameterMap.Add(paramKey, paramValue);
            }
        }

        public bool GetParameterValue(string paramKey, out double paramValue)
        {
            if(string.IsNullOrEmpty(paramKey))
            {
                throw new ArgumentNullException(paramKey);
            }

            return parameterMap.TryGetValue(paramKey, out paramValue);
        }
    }
}
