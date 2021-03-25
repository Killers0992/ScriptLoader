using SplitAndMerge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptLoader.CSCS
{
    class SCPSLRoundLockFunction : ParserFunction
    {
        protected override Variable Evaluate(ParsingScript script)
        {
            List<Variable> args = script.GetFunctionArgs();
            if (args.Count == 1)
                ChangeRoundlock(args[0].AsBool());

            return Variable.EmptyInstance;
        }
        protected override async Task<Variable> EvaluateAsync(ParsingScript script)
        {
            List<Variable> args = await script.GetFunctionArgsAsync();
            if (args.Count == 1)
                ChangeRoundlock(args[0].AsBool());

            return Variable.EmptyInstance;
        }

        private void ChangeRoundlock(bool state)
        {
            RoundSummary.RoundLock = state;
        }
    }

    class SCPSLRoundRestartFunction : ParserFunction
    {
        protected override Variable Evaluate(ParsingScript script)
        {
            RoundRestart();
            return Variable.EmptyInstance;
        }
        protected override async Task<Variable> EvaluateAsync(ParsingScript script)
        {
            RoundRestart();
            return Variable.EmptyInstance;
        }

        private void RoundRestart()
        {
            Exiled.API.Features.Round.RestartSilently();
        }
    }
}
