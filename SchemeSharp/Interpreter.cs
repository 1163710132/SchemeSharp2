using System;
using System.Collections.Generic;

namespace SchemeSharp
{
    public class Interpreter
    {
        public Dictionary<string, Action<ISchemeValue, SchemeContext, Action<ISchemeValue>>> SpecialFormTable { get; }

        public Interpreter()
        {
            SpecialFormTable = new Dictionary<string, Action<ISchemeValue, SchemeContext, Action<ISchemeValue>>>();
            RegisterSpecialForms();
        }
        public void Evaluate(ISchemeValue ast, SchemeContext context, Action<ISchemeValue> consumer)
        {
            if (ast.Kind != ISchemeValue.KindCode.Pair)
            {
                switch (ast.Kind)
                {
                    case ISchemeValue.KindCode.Symbol:
                        consumer(context.Get(((SchemeSymbol) ast).Value));
                        break;
                    default:
                        consumer(ast);
                        break;
                }
            }
            else
            {
                var (left, right) = ((SchemePair) ast).Pair;

                if (left.Kind == ISchemeValue.KindCode.Symbol)
                {
                    var symbol = ((SchemeSymbol) left).Value;
                    if (context.ContainsKey(symbol))
                    {
                        var procedure = (SchemeProcedure)context.Get(symbol);
                        EvaluateEach(right, context, args =>
                        {
                            procedure.Invoke(args, consumer);
                        });
                    }
                    else if (SpecialFormTable.ContainsKey(symbol))
                    {
                        SpecialFormTable[symbol](right, context, consumer);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    Evaluate(left, context, procedure =>
                    {
                        EvaluateEach(right, context, args =>
                        {
                            ((SchemeProcedure) procedure).Invoke(args, consumer);
                        });
                    });
                }
            }
        }

        public void EvaluateEach(ISchemeValue list, SchemeContext context, Action<ISchemeValue> consumer)
        {
            switch (list.Kind)
            {
                case ISchemeValue.KindCode.Pair:
                {
                    var (left, right) = ((SchemePair) list).Pair;
                    Evaluate(left, context, leftValue =>
                    {
                        EvaluateEach(right, context, rightValue =>
                        {
                            consumer(Scheme.Cons(leftValue, rightValue));
                        });
                    });
                    break;
                }
                case ISchemeValue.KindCode.Null:
                {
                    consumer(list);
                    break;
                }
                default:
                    throw new Exception();
            }
        }

        public void RegisterSpecialForm(string name, Action<ISchemeValue, SchemeContext, Action<ISchemeValue>> body)
        {
            SpecialFormTable[name] = body;
        }

        public void RegisterSpecialForm(string name, Func<ISchemeValue, SchemeContext, ISchemeValue> body)
        {
            SpecialFormTable[name] = (ast, context, consumer) =>
            {
                consumer(body(ast, context));
            };
        }
        
        public static void DefineArgs(ISchemeValue parameters, ISchemeValue args, SchemeContext context)
        {
            // while (true)
            // {
            //     switch (parameters.Kind)
            //     {
            //         case ISchemeValue.KindCode.Pair:
            //         {
            //             ISchemeValue parameter, arg;
            //             (parameter, parameters) = ((SchemePair) parameters).Pair;
            //             (arg, args) = ((SchemePair) args).Pair;
            //             context.Define(((SchemeString) parameter).Value, arg);
            //             break;
            //         }
            //         case ISchemeValue.KindCode.Null:
            //         {
            //             return;
            //         }
            //         default:
            //             throw new Exception();
            //     }
            // }
            DefineArgs(Scheme.Tie(parameters, args), context);
        }
        
        public static void DefineArgs(ISchemeValue parameterAndArgList, SchemeContext context)
        {
            while (true)
            {
                switch (parameterAndArgList.Kind)
                {
                    case ISchemeValue.KindCode.Pair:
                    {
                        ISchemeValue parameterAndArg;
                        (parameterAndArg, parameterAndArgList) = ((SchemePair) parameterAndArgList).Pair;
                        var (parameter, arg) = ((SchemePair)parameterAndArg).Pair;
                        context.Define(((SchemeSymbol) parameter).Value, arg);
                        break;
                    }
                    case ISchemeValue.KindCode.Null:
                    {
                        return;
                    }
                    default:
                        throw new Exception();
                }
            }
        }

        internal void RegisterSpecialForms()
        {
            RegisterSpecialForm("lambda", (ast, context, consumer) =>
            {
                var (parameters, statements) = ((SchemePair) ast).Pair;
                consumer(new SchemeProcedure((args, procedureConsumer)=>
                {
                    
                    DefineArgs(parameters, args, context);
                    ISchemeValue result = Scheme.Null();
                    while (true)
                    {
                        switch (statements.Kind)
                        {
                            case ISchemeValue.KindCode.Pair:
                            {
                                ISchemeValue statement;
                                (statement, statements) = ((SchemePair) statements).Pair;
                                Evaluate(statement, context, statementResult =>
                                {
                                    result = statementResult;
                                });
                                break;
                            }
                            case ISchemeValue.KindCode.Null:
                            {
                                procedureConsumer(result);
                                break;
                            }
                            default:
                            {
                                throw new Exception();
                            }
                        }
                    }
                }));
            });
            
            RegisterSpecialForm("define", (ast, context, consumer) =>
            {
                var (left, right) = ((SchemePair) ast).Pair;
                var (expr, _) = ((SchemePair) right).Pair;
                switch (left.Kind)
                {
                    case ISchemeValue.KindCode.Symbol:
                    {
                        Evaluate(expr, context, value =>
                        {
                            context.Define(((SchemeSymbol) left).Value, value);
                        });
                        break;
                    }
                    case ISchemeValue.KindCode.Pair:
                    {
                        var (symbol, parameters) = ((SchemePair) left).Pair;
                        var symbolText = ((SchemeSymbol) symbol).Value;
                        var procedure = new SchemeProcedure((args, procedureConsumer) =>
                        {
                            Evaluate(expr, context, procedureConsumer);
                        });
                        context.Define(symbolText, procedure);
                        break;
                    }
                }
            });
            
            RegisterSpecialForm("quote", (ast, context, consumer) => { consumer(ast); });
        }
    }
}