using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.Parser
{
    public interface ILexicalAnalyzer
    {
        string CurrentParameter { get; }

        double CurrentValue { get; }

        Token GetCurrentToken();
    }
}
