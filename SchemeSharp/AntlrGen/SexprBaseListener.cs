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

using Antlr4.Runtime.Misc;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="ISexprListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public partial class SexprBaseListener : ISexprListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="SexprParser.sexpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSexpr([NotNull] SexprParser.SexprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="SexprParser.sexpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSexpr([NotNull] SexprParser.SexprContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="SexprParser.item"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterItem([NotNull] SexprParser.ItemContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="SexprParser.item"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitItem([NotNull] SexprParser.ItemContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="SexprParser.pair"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPair([NotNull] SexprParser.PairContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="SexprParser.pair"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPair([NotNull] SexprParser.PairContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="SexprParser.list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterList([NotNull] SexprParser.ListContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="SexprParser.list"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitList([NotNull] SexprParser.ListContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="SexprParser.atom"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAtom([NotNull] SexprParser.AtomContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="SexprParser.atom"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAtom([NotNull] SexprParser.AtomContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="SexprParser.quote"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterQuote([NotNull] SexprParser.QuoteContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="SexprParser.quote"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitQuote([NotNull] SexprParser.QuoteContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
} // namespace AntlrGen
