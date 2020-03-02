using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Antlr4.Runtime;
using AntlrGen;

namespace SchemeSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // new Thread(() =>
            // {
                const string path = "/home/chenjinsong/RiderProjects/SchemeSharp/SchemeSharp/example.txt";
                var lexer = new SexprLexer(CharStreams.fromTextReader(new StreamReader(path)));
                var parser = new SexprParser(new BufferedTokenStream(lexer));
                var sexpr = new SexprVisitor().VisitSexpr(parser.sexpr());
                var ast = Scheme.FromSexpr(sexpr);
                var globalContext = new SchemeContext()
                .Define("write", new SchemeProcedure((value, context, consumer) =>
                {
                    var (arg0, _) = ((SchemePair) value).Pair;
                    Console.Write(arg0);
                    consumer(context, Scheme.Null());
                }))
                .Define("call/cc", new SchemeProcedure((args, context, continuation) =>
                {
                    var (arg0, _) = ((SchemePair) args).Pair;
                    var procedure = (SchemeProcedure) arg0;
                    var continuationProcedure = new SchemeProcedure((args, context2, cc) =>
                    {
                        var arg0 = args.Item(0);
                        continuation(context, arg0);
                    });
                    procedure.Invoke(Scheme.Cons(continuationProcedure, Scheme.Null()), context, continuation);
                }))
                .Define("+", new SchemeProcedure((args, context, continuation) =>
                {
                    continuation(context, args.Item(0).AsNumber() + args.Item(1).AsNumber());
                }))
                .Define("-", new SchemeProcedure((args, context, continuation) =>
                {
                    continuation(context, args.Item(0).AsNumber() - args.Item(1).AsNumber());
                }))
                .Define("*", new SchemeProcedure((args, context, continuation) =>
                {
                    continuation(context, args.Item(0).AsNumber() * args.Item(1).AsNumber());
                }));
                var interpreter = new Interpreter(globalContext);
                Scheme.ForEachInList(ast, statement =>
                {
                    // interpreter.Evaluate(statement, globalContext, (context, value) =>
                    // {
                    //     globalContext = context;
                    //     Console.WriteLine(value);
                    // });
                    interpreter.EvaluateGlobal(statement, Console.WriteLine);
                });
                Console.WriteLine("Program exited");

            // }, 1024*1024*512).Start();
        }
    }
}