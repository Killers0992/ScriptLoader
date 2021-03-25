using Exiled.API.Features;
using SplitAndMerge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptLoader.CSCS
{
    class ExiledLogFunction : ParserFunction
    {
        internal ExiledLogFunction(bool newLine = true)
        {
            m_newLine = newLine;
        }
        protected override Variable Evaluate(ParsingScript script)
        {
            List<Variable> args = script.GetFunctionArgs();
            AddOutput(args, script, m_newLine);

            return Variable.EmptyInstance;
        }
        protected override async Task<Variable> EvaluateAsync(ParsingScript script)
        {
            List<Variable> args = await script.GetFunctionArgsAsync();
            AddOutput(args, script, m_newLine);

            return Variable.EmptyInstance;
        }

        public static void AddOutput(List<Variable> args, ParsingScript script = null,
                                     bool addLine = true, bool addSpace = true, string start = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(start);
            foreach (var arg in args)
            {
                sb.Append(arg.AsString() + (addSpace ? " " : ""));
            }

            string output = sb.ToString() + (addLine ? Environment.NewLine : string.Empty);
            output = output.Replace("\\t", "\t").Replace("\\n", "\n");
            Log.Info(output);
        }

        private bool m_newLine = true;
    }
}
