using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace SplitAndMerge
{
    public partial class Interpreter
    {
        public void InitStandalone()
        {
            ParserFunction.RegisterFunction(Constants.GOTO, new GotoGosubFunction(true));
            ParserFunction.RegisterFunction(Constants.GOSUB, new GotoGosubFunction(false));

            ParserFunction.AddAction(Constants.LABEL_OPERATOR, new LabelFunction());
            ParserFunction.AddAction(Constants.POINTER, new PointerFunction());
            ParserFunction.AddAction(Constants.POINTER_REF, new PointerReferenceFunction());

#if UNITY_EDITOR == false && UNITY_STANDALONE == false
            // Math Top level functions
            ParserFunction.RegisterFunction(Constants.ABS, new AbsFunction());
            ParserFunction.RegisterFunction(Constants.ACOS, new AcosFunction());
            ParserFunction.RegisterFunction(Constants.ASIN, new AsinFunction());
            ParserFunction.RegisterFunction(Constants.CEIL, new CeilFunction());
            ParserFunction.RegisterFunction(Constants.COS, new CosFunction());
            ParserFunction.RegisterFunction(Constants.EXP, new ExpFunction());
            ParserFunction.RegisterFunction(Constants.FLOOR, new FloorFunction());
            ParserFunction.RegisterFunction(Constants.LOG, new LogFunction());
            ParserFunction.RegisterFunction(Constants.PI, new PiFunction());
            ParserFunction.RegisterFunction(Constants.POW, new PowFunction());
            ParserFunction.RegisterFunction(Constants.ROUND, new RoundFunction());
            ParserFunction.RegisterFunction(Constants.RANDOM, new GetRandomFunction());
            ParserFunction.RegisterFunction(Constants.SIN, new SinFunction());
            ParserFunction.RegisterFunction(Constants.SQRT, new SqrtFunction());

            //ParserFunction.CleanUp();
            ParserFunction.RegisterFunction(Constants.APPEND, new AppendFunction());
            ParserFunction.RegisterFunction(Constants.APPENDLINE, new AppendLineFunction());
            ParserFunction.RegisterFunction(Constants.APPENDLINES, new AppendLinesFunction());
            ParserFunction.RegisterFunction(Constants.CALL_NATIVE, new InvokeNativeFunction());
            ParserFunction.RegisterFunction(Constants.CD, new CdFunction());
            ParserFunction.RegisterFunction(Constants.CD__, new Cd__Function());
            ParserFunction.RegisterFunction(Constants.CONNECTSRV, new ClientSocket());
            ParserFunction.RegisterFunction(Constants.COPY, new CopyFunction());
            ParserFunction.RegisterFunction(Constants.DELETE, new DeleteFunction());
            ParserFunction.RegisterFunction(Constants.DIR, new DirFunction());
            ParserFunction.RegisterFunction(Constants.EXISTS, new ExistsFunction());
            ParserFunction.RegisterFunction(Constants.EXIT, new ExitFunction());
            ParserFunction.RegisterFunction(Constants.FINDFILES, new FindfilesFunction());
            ParserFunction.RegisterFunction(Constants.FINDSTR, new FindstrFunction());
            ParserFunction.RegisterFunction(Constants.GET_NATIVE, new GetNativeFunction());
            ParserFunction.RegisterFunction(Constants.KILL, new KillFunction());
            ParserFunction.RegisterFunction(Constants.MKDIR, new MkdirFunction());
            ParserFunction.RegisterFunction(Constants.MORE, new MoreFunction());
            ParserFunction.RegisterFunction(Constants.MOVE, new MoveFunction());
            ParserFunction.RegisterFunction(Constants.PSINFO, new PsInfoFunction());
            ParserFunction.RegisterFunction(Constants.PWD, new PwdFunction());
            ParserFunction.RegisterFunction(Constants.READFILE, new ReadCSCSFileFunction());
            ParserFunction.RegisterFunction(Constants.RUN, new RunFunction());
            ParserFunction.RegisterFunction(Constants.SET_NATIVE, new SetNativeFunction());
            ParserFunction.RegisterFunction(Constants.STARTSRV, new ServerSocket());
            ParserFunction.RegisterFunction(Constants.STOPWATCH_ELAPSED, new StopWatchFunction(StopWatchFunction.Mode.ELAPSED));
            ParserFunction.RegisterFunction(Constants.STOPWATCH_START, new StopWatchFunction(StopWatchFunction.Mode.START));
            ParserFunction.RegisterFunction(Constants.STOPWATCH_STOP, new StopWatchFunction(StopWatchFunction.Mode.STOP));
            ParserFunction.RegisterFunction(Constants.TAIL, new TailFunction());
            ParserFunction.RegisterFunction(Constants.TIMESTAMP, new TimestampFunction());
            ParserFunction.RegisterFunction(Constants.WRITE, new PrintFunction(false));
            ParserFunction.RegisterFunction(Constants.WRITELINE, new WriteLineFunction());
            ParserFunction.RegisterFunction(Constants.WRITELINES, new WriteLinesFunction());
            ParserFunction.RegisterFunction(Constants.WRITE_CONSOLE, new WriteToConsole());

#if __ANDROID__ == false && __IOS__ == false
            ParserFunction.RegisterFunction(Constants.ADD_COMP_DEFINITION, new EditCompiledEntry(EditCompiledEntry.EditMode.ADD_DEFINITION));
            ParserFunction.RegisterFunction(Constants.ADD_COMP_NAMESPACE, new EditCompiledEntry(EditCompiledEntry.EditMode.ADD_NAMESPACE));
            ParserFunction.RegisterFunction(Constants.CLEAR_COMP_DEFINITIONS, new EditCompiledEntry(EditCompiledEntry.EditMode.CLEAR_DEFINITIONS));
            ParserFunction.RegisterFunction(Constants.CLEAR_COMP_NAMESPACES, new EditCompiledEntry(EditCompiledEntry.EditMode.CLEAR_NAMESPACES));
            ParserFunction.RegisterFunction(Constants.CSHARP_FUNCTION, new CompiledFunctionCreator(true));

            ParserFunction.RegisterFunction(Constants.CONSOLE_CLR, new ClearConsole());
            ParserFunction.RegisterFunction(Constants.PRINT_BLACK, new PrintColorFunction(ConsoleColor.Black));
            ParserFunction.RegisterFunction(Constants.PRINT_GRAY, new PrintColorFunction(ConsoleColor.DarkGray));
            ParserFunction.RegisterFunction(Constants.PRINT_GREEN, new PrintColorFunction(ConsoleColor.Green));
            ParserFunction.RegisterFunction(Constants.PRINT_RED, new PrintColorFunction(ConsoleColor.Red));
            ParserFunction.RegisterFunction(Constants.READ, new ReadConsole());
            ParserFunction.RegisterFunction(Constants.READNUMBER, new ReadConsole(true));
            ParserFunction.RegisterFunction(Constants.TRANSLATE, new TranslateFunction());

            ParserFunction.RegisterFunction(Constants.ENCODE_FILE, new EncodeFileFunction(true));
            ParserFunction.RegisterFunction(Constants.DECODE_FILE, new EncodeFileFunction(false));

            CSCS_SQL.Init();
#endif
#endif
            //ReadConfig();
        }
   }
}
