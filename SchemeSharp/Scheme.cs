using System;
using System.Collections.Generic;
using System.Text;

namespace SchemeSharp
{
    public delegate void Continuation(SchemeContext context, ISchemeValue value);
    
    public static class Scheme
    {
        public static SchemePair Cons(ISchemeValue left, ISchemeValue right)
        {
            return new SchemePair(left, right);
        }

        public static SchemeVector MakeVector(IEnumerable<ISchemeValue> values)
        {
            return new SchemeVector(values);
        }

        public static SchemeVector MakeVectorSize(int size)
        {
            return new SchemeVector(size);
        }

        public static SchemeSymbol Symbol(string text)
        {
            return new SchemeSymbol(text);
        }

        public static SchemeString String(string text)
        {
            return new SchemeString(text);
        }

        public static SchemeNull Null()
        {
            return SchemeNull.Instance;
        }

        public static SchemeNumber Number(string text)
        {
            static bool IsDigitLike(char ch)
            {
                return (ch >= '0' && ch <= '9') || ch == '/' || ch == '.';
            }

            var pos = 0;
            
            var real = SchemeRational.Zero;
            var imaginary = SchemeRational.Zero;

            var builder = new StringBuilder();
            
            while (pos < text.Length)
            {
                if (text[pos] == '+' || text[pos] == '-')
                {
                    if (builder.Length > 0)
                    {
                        real = SchemeRational.Parse(builder.ToString());
                        builder.Clear();
                    }

                    builder.Append(text[pos]);
                }
                else if (IsDigitLike(text[pos]))
                {
                    builder.Append(text[pos]);
                }else if (text[pos] == 'i')
                {
                    imaginary = SchemeRational.Parse(builder.ToString());
                }
                else throw new Exception();

                pos++;
            }

            if (builder.Length > 0)
            {
                real = SchemeRational.Parse(builder.ToString());
                builder.Clear();
            }

            return new SchemeNumber(new SchemeComplex(real, imaginary));
        }

        public static SchemeNumber AsNumber(this ISchemeValue number)
        {
            return (SchemeNumber) number;
        }

        public static SchemeSymbol AsSymbol(this ISchemeValue symbol)
        {
            return (SchemeSymbol) symbol;
        }

        //Dangerous: Breaking continuation
        public static void ForEachInList(ISchemeValue list, Action<ISchemeValue> consumer)
        {
            switch (list.Kind)
            {
                case ISchemeValue.KindCode.Pair:
                {
                    var (left, right) = ((SchemePair) list).Pair;
                    consumer(left);
                    ForEachInList(right, consumer);
                }
                    break;
                case ISchemeValue.KindCode.Null:
                    return;
                default:
                    throw new Exception();
            }
        }

        public static ISchemeValue Tie(ISchemeValue lList, ISchemeValue rList)
        {
            switch (lList.Kind)
            {
                case ISchemeValue.KindCode.Pair:
                {
                    var (lListItem, lListItems) = ((SchemePair) lList).Pair;
                    var (rListItem, rListItems) = ((SchemePair) rList).Pair;
                    return Cons(Cons(lListItem, rListItem), Tie(lListItems, rListItems));
                }
                case ISchemeValue.KindCode.Null:
                {
                    return Null();
                }
                default:
                {
                    throw new Exception();
                }
            }
        }

        public static ISchemeValue FromSexpr(ISexpr sexpr)
        {
            if (sexpr.IsAtom)
            {
                var value = sexpr.AtomValue;
                switch (sexpr.AtomKind)
                {
                    case SexprAtom.KindCode.String:
                        return String(value);
                    case SexprAtom.KindCode.Number:
                        return Number(value);
                    case SexprAtom.KindCode.Symbol:
                        return Symbol(value);
                    case SexprAtom.KindCode.Null:
                        return Null();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }else if (sexpr.IsPair)
            {
                return Cons(FromSexpr(sexpr.Left), FromSexpr(sexpr.Right));
            }
            else
            {
                throw new Exception();
            }
        }

        public static bool IsList(this ISchemeValue sexpr)
        {
            return sexpr.Kind == ISchemeValue.KindCode.Null ||
                   sexpr.Kind == ISchemeValue.KindCode.Pair && IsList(((SchemePair)sexpr).Right);
        }

        public static bool IsNull(this ISchemeValue sexpr)
        {
            return sexpr.Kind == ISchemeValue.KindCode.Null;
        }

        public static ISchemeValue Item(this ISchemeValue list, int index)
        {
            if (index == 0)
            {
                return ((SchemePair) list).Left;
            }
            else if(index > 0)
            {
                return Item(((SchemePair) list).Right, index - 1);
            }
            else
            {
                throw new Exception();
            }
        }

        public static ISchemeValue Left(this ISchemeValue pair)
        {
            return ((SchemePair) pair).Left;
        }

        public static ISchemeValue Right(this ISchemeValue pair)
        {
            return ((SchemePair) pair).Right;
        }

        public static (ISchemeValue, ISchemeValue) Pair(this ISchemeValue pair)
        {
            return (Left(pair), Right(pair));
        }
    }
}