//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /home/chenjinsong/RiderProjects/SchemeSharp/SchemeSharp/Sexpr.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace AntlrGen {
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public partial class SexprParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, NUMBER=2, STRING=3, WHITESPACE=4, SYMBOL=5, LPAREN=6, RPAREN=7, 
		DOT=8, PLUS=9, MINUS=10;
	public const int
		RULE_sexpr = 0, RULE_item = 1, RULE_pair = 2, RULE_list = 3, RULE_atom = 4, 
		RULE_quote = 5;
	public static readonly string[] ruleNames = {
		"sexpr", "item", "pair", "list", "atom", "quote"
	};

	private static readonly string[] _LiteralNames = {
		null, "'''", null, null, null, null, "'('", "')'", "'.'", "'+'", "'-'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, "NUMBER", "STRING", "WHITESPACE", "SYMBOL", "LPAREN", "RPAREN", 
		"DOT", "PLUS", "MINUS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Sexpr.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static SexprParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public SexprParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public SexprParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class SexprContext : ParserRuleContext {
		public ITerminalNode Eof() { return GetToken(SexprParser.Eof, 0); }
		public ItemContext[] item() {
			return GetRuleContexts<ItemContext>();
		}
		public ItemContext item(int i) {
			return GetRuleContext<ItemContext>(i);
		}
		public SexprContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_sexpr; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.EnterSexpr(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.ExitSexpr(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ISexprVisitor<TResult> typedVisitor = visitor as ISexprVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSexpr(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public SexprContext sexpr() {
		SexprContext _localctx = new SexprContext(Context, State);
		EnterRule(_localctx, 0, RULE_sexpr);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 15;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << NUMBER) | (1L << STRING) | (1L << SYMBOL) | (1L << LPAREN))) != 0)) {
				{
				{
				State = 12; item();
				}
				}
				State = 17;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 18; Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ItemContext : ParserRuleContext {
		public AtomContext atom() {
			return GetRuleContext<AtomContext>(0);
		}
		public ListContext list() {
			return GetRuleContext<ListContext>(0);
		}
		public PairContext pair() {
			return GetRuleContext<PairContext>(0);
		}
		public QuoteContext quote() {
			return GetRuleContext<QuoteContext>(0);
		}
		public ItemContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_item; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.EnterItem(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.ExitItem(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ISexprVisitor<TResult> typedVisitor = visitor as ISexprVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitItem(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ItemContext item() {
		ItemContext _localctx = new ItemContext(Context, State);
		EnterRule(_localctx, 2, RULE_item);
		try {
			State = 24;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,1,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 20; atom();
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 21; list();
				}
				break;
			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 22; pair();
				}
				break;
			case 4:
				EnterOuterAlt(_localctx, 4);
				{
				State = 23; quote();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class PairContext : ParserRuleContext {
		public ITerminalNode LPAREN() { return GetToken(SexprParser.LPAREN, 0); }
		public ITerminalNode DOT() { return GetToken(SexprParser.DOT, 0); }
		public ITerminalNode RPAREN() { return GetToken(SexprParser.RPAREN, 0); }
		public ItemContext[] item() {
			return GetRuleContexts<ItemContext>();
		}
		public ItemContext item(int i) {
			return GetRuleContext<ItemContext>(i);
		}
		public PairContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_pair; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.EnterPair(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.ExitPair(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ISexprVisitor<TResult> typedVisitor = visitor as ISexprVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitPair(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public PairContext pair() {
		PairContext _localctx = new PairContext(Context, State);
		EnterRule(_localctx, 4, RULE_pair);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 26; Match(LPAREN);
			State = 28;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 27; item();
				}
				}
				State = 30;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << NUMBER) | (1L << STRING) | (1L << SYMBOL) | (1L << LPAREN))) != 0) );
			State = 32; Match(DOT);
			State = 33; Match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ListContext : ParserRuleContext {
		public ITerminalNode LPAREN() { return GetToken(SexprParser.LPAREN, 0); }
		public ITerminalNode RPAREN() { return GetToken(SexprParser.RPAREN, 0); }
		public ItemContext[] item() {
			return GetRuleContexts<ItemContext>();
		}
		public ItemContext item(int i) {
			return GetRuleContext<ItemContext>(i);
		}
		public ListContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_list; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.EnterList(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.ExitList(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ISexprVisitor<TResult> typedVisitor = visitor as ISexprVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitList(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ListContext list() {
		ListContext _localctx = new ListContext(Context, State);
		EnterRule(_localctx, 6, RULE_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 35; Match(LPAREN);
			State = 39;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << NUMBER) | (1L << STRING) | (1L << SYMBOL) | (1L << LPAREN))) != 0)) {
				{
				{
				State = 36; item();
				}
				}
				State = 41;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 42; Match(RPAREN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AtomContext : ParserRuleContext {
		public ITerminalNode STRING() { return GetToken(SexprParser.STRING, 0); }
		public ITerminalNode SYMBOL() { return GetToken(SexprParser.SYMBOL, 0); }
		public ITerminalNode NUMBER() { return GetToken(SexprParser.NUMBER, 0); }
		public AtomContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_atom; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.EnterAtom(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.ExitAtom(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ISexprVisitor<TResult> typedVisitor = visitor as ISexprVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAtom(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AtomContext atom() {
		AtomContext _localctx = new AtomContext(Context, State);
		EnterRule(_localctx, 8, RULE_atom);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 44;
			_la = TokenStream.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << NUMBER) | (1L << STRING) | (1L << SYMBOL))) != 0)) ) {
			ErrorHandler.RecoverInline(this);
			}
			else {
				ErrorHandler.ReportMatch(this);
			    Consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class QuoteContext : ParserRuleContext {
		public ItemContext item() {
			return GetRuleContext<ItemContext>(0);
		}
		public QuoteContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_quote; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.EnterQuote(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISexprListener typedListener = listener as ISexprListener;
			if (typedListener != null) typedListener.ExitQuote(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ISexprVisitor<TResult> typedVisitor = visitor as ISexprVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitQuote(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public QuoteContext quote() {
		QuoteContext _localctx = new QuoteContext(Context, State);
		EnterRule(_localctx, 10, RULE_quote);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 46; Match(T__0);
			State = 47; item();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x3', '\f', '\x34', '\x4', '\x2', '\t', '\x2', '\x4', '\x3', 
		'\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', '\x5', '\x4', 
		'\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x3', '\x2', '\a', '\x2', 
		'\x10', '\n', '\x2', '\f', '\x2', '\xE', '\x2', '\x13', '\v', '\x2', '\x3', 
		'\x2', '\x3', '\x2', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x5', '\x3', '\x1B', '\n', '\x3', '\x3', '\x4', '\x3', '\x4', 
		'\x6', '\x4', '\x1F', '\n', '\x4', '\r', '\x4', '\xE', '\x4', ' ', '\x3', 
		'\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x5', '\x3', '\x5', '\a', '\x5', 
		'(', '\n', '\x5', '\f', '\x5', '\xE', '\x5', '+', '\v', '\x5', '\x3', 
		'\x5', '\x3', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\a', '\x3', '\a', 
		'\x3', '\a', '\x3', '\a', '\x2', '\x2', '\b', '\x2', '\x4', '\x6', '\b', 
		'\n', '\f', '\x2', '\x3', '\x4', '\x2', '\x4', '\x5', '\a', '\a', '\x2', 
		'\x33', '\x2', '\x11', '\x3', '\x2', '\x2', '\x2', '\x4', '\x1A', '\x3', 
		'\x2', '\x2', '\x2', '\x6', '\x1C', '\x3', '\x2', '\x2', '\x2', '\b', 
		'%', '\x3', '\x2', '\x2', '\x2', '\n', '.', '\x3', '\x2', '\x2', '\x2', 
		'\f', '\x30', '\x3', '\x2', '\x2', '\x2', '\xE', '\x10', '\x5', '\x4', 
		'\x3', '\x2', '\xF', '\xE', '\x3', '\x2', '\x2', '\x2', '\x10', '\x13', 
		'\x3', '\x2', '\x2', '\x2', '\x11', '\xF', '\x3', '\x2', '\x2', '\x2', 
		'\x11', '\x12', '\x3', '\x2', '\x2', '\x2', '\x12', '\x14', '\x3', '\x2', 
		'\x2', '\x2', '\x13', '\x11', '\x3', '\x2', '\x2', '\x2', '\x14', '\x15', 
		'\a', '\x2', '\x2', '\x3', '\x15', '\x3', '\x3', '\x2', '\x2', '\x2', 
		'\x16', '\x1B', '\x5', '\n', '\x6', '\x2', '\x17', '\x1B', '\x5', '\b', 
		'\x5', '\x2', '\x18', '\x1B', '\x5', '\x6', '\x4', '\x2', '\x19', '\x1B', 
		'\x5', '\f', '\a', '\x2', '\x1A', '\x16', '\x3', '\x2', '\x2', '\x2', 
		'\x1A', '\x17', '\x3', '\x2', '\x2', '\x2', '\x1A', '\x18', '\x3', '\x2', 
		'\x2', '\x2', '\x1A', '\x19', '\x3', '\x2', '\x2', '\x2', '\x1B', '\x5', 
		'\x3', '\x2', '\x2', '\x2', '\x1C', '\x1E', '\a', '\b', '\x2', '\x2', 
		'\x1D', '\x1F', '\x5', '\x4', '\x3', '\x2', '\x1E', '\x1D', '\x3', '\x2', 
		'\x2', '\x2', '\x1F', ' ', '\x3', '\x2', '\x2', '\x2', ' ', '\x1E', '\x3', 
		'\x2', '\x2', '\x2', ' ', '!', '\x3', '\x2', '\x2', '\x2', '!', '\"', 
		'\x3', '\x2', '\x2', '\x2', '\"', '#', '\a', '\n', '\x2', '\x2', '#', 
		'$', '\a', '\t', '\x2', '\x2', '$', '\a', '\x3', '\x2', '\x2', '\x2', 
		'%', ')', '\a', '\b', '\x2', '\x2', '&', '(', '\x5', '\x4', '\x3', '\x2', 
		'\'', '&', '\x3', '\x2', '\x2', '\x2', '(', '+', '\x3', '\x2', '\x2', 
		'\x2', ')', '\'', '\x3', '\x2', '\x2', '\x2', ')', '*', '\x3', '\x2', 
		'\x2', '\x2', '*', ',', '\x3', '\x2', '\x2', '\x2', '+', ')', '\x3', '\x2', 
		'\x2', '\x2', ',', '-', '\a', '\t', '\x2', '\x2', '-', '\t', '\x3', '\x2', 
		'\x2', '\x2', '.', '/', '\t', '\x2', '\x2', '\x2', '/', '\v', '\x3', '\x2', 
		'\x2', '\x2', '\x30', '\x31', '\a', '\x3', '\x2', '\x2', '\x31', '\x32', 
		'\x5', '\x4', '\x3', '\x2', '\x32', '\r', '\x3', '\x2', '\x2', '\x2', 
		'\x6', '\x11', '\x1A', ' ', ')',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace AntlrGen
