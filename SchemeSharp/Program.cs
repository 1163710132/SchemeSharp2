using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using AntlrGen;

namespace SchemeSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = "/home/chenjinsong/RiderProjects/SchemeSharp/SchemeSharp/example.txt";
            var lexer = new SexprLexer(CharStreams.fromTextReader(new StreamReader(path)));
            var parser = new SexprParser(new BufferedTokenStream(lexer));
            var sexpr = new SexprVisitor().VisitSexpr(parser.sexpr());
            var ast = Scheme.FromSexpr(sexpr);
            var context = new SchemeContext();
            context.Define("print", new SchemeProcedure((value, consumer) =>
            {
                var (arg0, _) = ((SchemePair) value).Pair;
                Console.WriteLine(arg0);
                consumer(Scheme.Null());
            }));
            var interpreter = new Interpreter();
            Scheme.ForEachInList(ast, statement =>
            {
                interpreter.Evaluate(statement, context, value => {});
            });
        }
    }
}