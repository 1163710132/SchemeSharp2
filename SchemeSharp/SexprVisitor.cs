using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Antlr4.Runtime.Tree;
using AntlrGen;

namespace SchemeSharp
{
    public class SexprVisitor: SexprBaseVisitor<ISexpr>
    {
        public override ISexpr VisitSexpr(SexprParser.SexprContext context)
        {
            return Sexpr.List(context.item()
                .Select(VisitItem));
        }

        public override ISexpr VisitItem(SexprParser.ItemContext context)
        {
            if (context.atom() != null)
            {
                return VisitAtom(context.atom());
            }
            else if (context.list() != null)
            {
                return VisitList(context.list());
            }
            else if (context.pair() != null)
            {
                return VisitPair(context.pair());
            }
            else if (context.quote() != null)
            {
                return VisitQuote(context.quote());
            }
            else throw new Exception();
        }

        public override ISexpr VisitPair(SexprParser.PairContext context)
        {
            return Sexpr.Pair(
                VisitItem(context.item(0)), 
                VisitItem(context.item(1)));
        }

        public override ISexpr VisitAtom(SexprParser.AtomContext context)
        {
            var text = context.GetText();
            if (context.SYMBOL() != null)
            {
                return Sexpr.Symbol(text);
            }else if (context.STRING() != null)
            {
                return Sexpr.String(text);
            }else if (context.NUMBER() != null)
            {
                return Sexpr.Number(text);
            }
            else throw new Exception();
        }

        public override ISexpr VisitList(SexprParser.ListContext context)
        {
            return Sexpr.List(context.item()
                .Select(VisitItem));
        }

        public override ISexpr VisitQuote(SexprParser.QuoteContext context)
        {
            return Sexpr.List(new[] { Sexpr.Symbol("quote"), VisitItem(context.item()) });
        }
    }
}