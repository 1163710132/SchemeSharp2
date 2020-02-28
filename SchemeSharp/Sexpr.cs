using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchemeSharp
{
    public static class Sexpr
    {
        public static SexprAtom Atom(SexprAtom.KindCode kind, string value)
        {
            return new SexprAtom(kind, value);
        }

        public static SexprPair Pair(ISexpr left, ISexpr right)
        {
            return new SexprPair(left, right);
        }

        public static SexprAtom Null()
        {
            return Atom(SexprAtom.KindCode.Null, "'()");
        }

        public static ISexpr List(IEnumerable<ISexpr> content)
        {
            return content.Reverse().Aggregate((ISexpr)Null(), (right, left)=>Pair(left, right));
        }

        public static SexprAtom Symbol(string value)
        {
            return Atom(SexprAtom.KindCode.Symbol, value);
        }

        public static SexprAtom String(string value)
        {
            return Atom(SexprAtom.KindCode.String, value);
        }

        public static SexprAtom Number(string value)
        {
            return Atom(SexprAtom.KindCode.Number, value);
        }

        public static bool IsList(ISexpr sexpr)
        {
            return sexpr.IsAtom && sexpr.AtomKind == SexprAtom.KindCode.Null ||
                   sexpr.IsPair && IsList(sexpr.Right);
        }
    }
    public interface ISexpr
    {
        public bool IsAtom { get; }
        public bool IsPair { get; }
        public SexprAtom.KindCode AtomKind { get; }
        public string AtomValue { get; }
        public ISexpr Left { get; }
        public ISexpr Right { get; }
        public (ISexpr, ISexpr) Pair => (Left, Right);
    }

    public class SexprAtom: ISexpr
    {
        public bool IsAtom => true;
        public bool IsPair => false;
        public KindCode AtomKind { get; }
        public string AtomValue { get; }
        public ISexpr Left => throw new Exception();
        public ISexpr Right => throw new Exception();

        public SexprAtom(KindCode kind, string value)
        {
            AtomValue = value;
            AtomKind = kind;
        }

        public enum KindCode
        {
            String, Number, Symbol, Null
        }

        public override string ToString()
        {
            return AtomValue;
        }
    }

    public class SexprPair : ISexpr
    {
        public bool IsAtom => false;
        public bool IsPair => true;
        public SexprAtom.KindCode AtomKind => throw new Exception();
        public string AtomValue => throw new Exception();
        public ISexpr Left { get; }
        public ISexpr Right { get; }
        public SexprPair(ISexpr left, ISexpr right)
        {
            Left = left;
            Right = right;
        }

        public override string ToString()
        {
            if (Sexpr.IsList(this))
            {
                ISexpr items = this;
                var builder = new StringBuilder();
                builder.Append("( ");
                while (items.IsPair)
                {
                    var item = items.Left;
                    items = items.Right;
                    builder.Append(item).Append(" ");
                }
                builder.Append(")");
                return builder.ToString();
            }
            else
            {
                return $"({Left} . {Right})";
            }
        }
    }
}