using System;
using System.Collections.Generic;

namespace SchemeSharp
{
    public delegate void SpecialForm(ISchemeValue args, SchemeContext context, Continuation continuation);
    
    public class Interpreter
    {
        public Dictionary<string, SpecialForm> SpecialFormTable { get; }
        public SchemeContext GlobalContext { get; private set; }

        public Interpreter(SchemeContext globalContext = null)
        {
            SpecialFormTable = new Dictionary<string, SpecialForm>();
            GlobalContext = globalContext ?? new SchemeContext();
            RegisterSpecialForms();
        }

        public void EvaluateGlobal(ISchemeValue statement, Action<ISchemeValue> valueConsumer)
        {
            Evaluate(statement, GlobalContext, (context, value) =>
            {
                GlobalContext = context;
                valueConsumer?.Invoke(value);
            });
        }
        public void Evaluate(ISchemeValue ast, SchemeContext context, Continuation continuation)
        {
            if (ast.Kind != ISchemeValue.KindCode.Pair)
            {
                switch (ast.Kind)
                {
                    case ISchemeValue.KindCode.Symbol:
                        continuation(context, context.Get(((SchemeSymbol) ast).Value));
                        break;
                    default:
                        continuation(context, ast);
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
                        EvaluateAndTakeAll(right, context, (context2, args) =>
                        {
                            procedure.Invoke(args, context2, continuation);
                        });
                    }
                    else if (SpecialFormTable.ContainsKey(symbol))
                    {
                        SpecialFormTable[symbol](right, context, continuation);
                    }
                    else
                    {
                        throw new Exception($"Symbol \"{symbol}\" undefined");
                    }
                }
                else
                {
                    Evaluate(left, context, (context2, procedure) =>
                    {
                        EvaluateAndTakeAll(right, context2, (context3, args) =>
                        {
                            ((SchemeProcedure) procedure).Invoke(args, context3, continuation);
                        });
                    });
                }
            }
        }

        public void EvaluateAndTakeAll(ISchemeValue list, SchemeContext context, Continuation continuation)
        {
            switch (list.Kind)
            {
                case ISchemeValue.KindCode.Pair:
                {
                    Evaluate(list.Left(), context, (context2, arg) =>
                    {
                        EvaluateAndTakeAll(list.Right(), context2, (context3, args) =>
                        {
                            continuation(context3, Scheme.Cons(arg, args));
                        });
                    });
                    break;
                }
                case ISchemeValue.KindCode.Null:
                {
                    continuation(context, list);
                    break;
                }
                default:
                    throw new Exception();
            }
        }

        public void EvaluateAndTakeLast(ISchemeValue list, SchemeContext context, Continuation continuation)
        {
            switch (list.Kind)
            {
                case ISchemeValue.KindCode.Pair:
                {
                    var (left, right) = ((SchemePair) list).Pair;
                    if (right.IsNull())
                    {
                        Evaluate(left, context, continuation);
                    }
                    else
                    {
                        Evaluate(left, context, (context2, _) =>
                        {
                            EvaluateAndTakeLast(right, context2, continuation);
                        });
                    }
                    break;
                }
                default:
                    throw new Exception();
            }
        }

        public void RegisterSpecialForm(string name, SpecialForm body)
        {
            SpecialFormTable[name] = body;
        }
        
        public static SchemeContext DefineArgs(ISchemeValue parameters, ISchemeValue args, SchemeContext context)
        {
            return DefineArgs(Scheme.Tie(parameters, args), context);
        }
        
        public static SchemeContext DefineArgs(ISchemeValue parameterAndArgList, SchemeContext context)
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
                        context = context.Define(((SchemeSymbol) parameter).Value, arg);
                        break;
                    }
                    case ISchemeValue.KindCode.Null:
                    {
                        return context;
                    }
                    default:
                        throw new Exception();
                }
            }
        }

        public void LetStarHelper(ISchemeValue localExprList, ISchemeValue statementList, SchemeContext context, Continuation continuation)
        {
            if (localExprList.IsNull())
            {
                EvaluateAndTakeLast(statementList, context, continuation);
            }
            else
            {
                var parameterAndArgExpr = localExprList.Left();
                var parameter = parameterAndArgExpr.Item(0);
                var argExpr = parameterAndArgExpr.Item(1);
                Evaluate(argExpr, context, (context2, argValue) =>
                {
                    var context3 = context2.Define(parameter.AsSymbol().Value, argValue);
                    LetStarHelper(localExprList.Right(), statementList, context3, continuation);
                });
            }
        }

        public void LetHelper(ISchemeValue localExprList, ISchemeValue localList, ISchemeValue statementList, SchemeContext context, Continuation continuation)
        {
            if (localExprList.IsNull())
            {
                while (!localList.IsNull())
                {
                    var (parameter, arg) = localList.Left().Pair();
                    localList = localList.Right();
                    context = context.Define(parameter.AsSymbol().Value, arg);
                }

                EvaluateAndTakeLast(statementList, context, continuation);
            }
            else
            {
                var parameterAndArgExpr = localExprList.Left();
                var parameter = parameterAndArgExpr.Item(0);
                var argExpr = parameterAndArgExpr.Item(1);
                Evaluate(argExpr, context, (context2, argValue) =>
                {
                    localList = Scheme.Cons(Scheme.Cons(parameter, argValue), localList);
                    LetHelper(localExprList.Right(), localList, 
                        statementList, context2, continuation);
                });
            }
        }

        internal void RegisterSpecialForms()
        {
            RegisterSpecialForm("lambda", (ast, context, continuation) =>
            {
                var (parameters, statements) = ((SchemePair) ast).Pair;
                var procedure = new SchemeProcedure((args, context2, continuation2) =>
                {
                    var context3 = DefineArgs(parameters, args, new SchemeContext(context2));
                    EvaluateAndTakeLast(statements, context3, (context4, value) =>
                    {
                        continuation2(context4.Parent, value);
                    });
                });
                continuation(context, procedure);
            });
            
            RegisterSpecialForm("define", (ast, context, continuation) =>
            {
                var (left, right) = ((SchemePair) ast).Pair;
                var (expr, _) = ((SchemePair) right).Pair;
                switch (left.Kind)
                {
                    case ISchemeValue.KindCode.Symbol:
                    {
                        Evaluate(expr, context, (context2, value) =>
                        {
                            var context3 = context2.Define(((SchemeSymbol) left).Value, value);
                            continuation(context3, null);
                        });
                        break;
                    }
                    case ISchemeValue.KindCode.Pair:
                    {
                        var (symbol, parameters) = ((SchemePair) left).Pair;
                        var symbolText = ((SchemeSymbol) symbol).Value;
                        var procedure = new SchemeProcedure((args, context3, procedureConsumer) =>
                        {
                            var context4 = DefineArgs(parameters, args, context3);
                            Evaluate(expr, context4, procedureConsumer);
                        });
                        var context2 = context.Define(symbolText, procedure);
                        continuation(context2, null);
                        break;
                    }
                }
            });
            
            RegisterSpecialForm("quote", (ast, context, continuation) =>
            {
                continuation(context, ast);
            });
            
            RegisterSpecialForm("set!", (ast, context, continuation) =>
            {
                var (left, right) = ((SchemePair) ast).Pair;
                var (expr, _) = ((SchemePair) right).Pair;
                Evaluate(expr, context, (context2, value) =>
                {
                    context2.Set(((SchemeSymbol) left).Value, value);
                    continuation(context2, null);
                });
            });

            RegisterSpecialForm("let*", (ast, context, continuation) =>
            {
                var (localExprList, statementList) = ast.Pair();
                LetStarHelper(localExprList, statementList, new SchemeContext(context), (context2, value) =>
                {
                    continuation(context2.Parent, value);
                });
            });
            
            RegisterSpecialForm("let", (ast, context, continuation) =>
            {
                var (localExprList, statementList) = ast.Pair();
                LetHelper(localExprList, Scheme.Null(), statementList, context, continuation);
            });
        }
    }
}