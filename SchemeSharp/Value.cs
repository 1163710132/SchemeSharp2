using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SchemeSharp
{
    public interface ISchemeValue
    {
        public enum KindCode
        {
            Boolean,
            Number,
            String,
            Pair,
            Vector,
            Procedure, 
            Symbol,
            Port, 
            Char, 
            Null
        }

        public KindCode Kind { get; }
    }

    public class SchemeBoolean : ISchemeValue
    {
        public ISchemeValue.KindCode Kind => ISchemeValue.KindCode.Boolean;
        
        public bool Value { get; }

        public SchemeBoolean(bool value)
        {
            Value = value;
        }
        
        public static SchemeBoolean True { get; }
        public static SchemeBoolean False { get; }

        static SchemeBoolean()
        {
            True = new SchemeBoolean(true);
            False = new SchemeBoolean(false);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class SchemeRational
    {
        public BigInteger Nominator { get; }
        public BigInteger Denominator { get; }

        public bool IsZero => Nominator == 0;

        public SchemeRational(BigInteger nominator, BigInteger denominator)
        {
            var gcd = BigInteger.GreatestCommonDivisor(nominator, denominator);
            Nominator = nominator / gcd;
            Denominator = denominator / gcd;
        }

        public SchemeRational Minus()
        {
            return new SchemeRational(-Nominator, Denominator);
        }

        public SchemeRational AttachSign(bool sign)
        {
            return !sign ? new SchemeRational(-Nominator, Denominator) : this;
        }
        
        public static SchemeRational operator+(SchemeRational left, SchemeRational right)
        {
            var leftNomi = left.Nominator * right.Denominator;
            var rightNomi = right.Nominator * left.Denominator;
            var denominator = left.Denominator * right.Denominator;
            return new SchemeRational(leftNomi + rightNomi, denominator);
        }
        
        public static SchemeRational operator-(SchemeRational left, SchemeRational right)
        {
            var leftNomi = left.Nominator * right.Denominator;
            var rightNomi = right.Nominator * left.Denominator;
            var denominator = left.Denominator * right.Denominator;
            return new SchemeRational(leftNomi - rightNomi, denominator);
        }
        
        public static SchemeRational operator*(SchemeRational left, SchemeRational right)
        {
            var nominator = left.Nominator * right.Nominator;
            var denominator = left.Denominator * right.Denominator;
            return new SchemeRational(nominator, denominator);
        }

        public static SchemeRational operator -(SchemeRational rational)
        {
            return rational.Minus();
        }
        
        public static SchemeRational Zero { get; }

        static SchemeRational()
        {
            Zero = new SchemeRational(0, 1);
        }

        public static SchemeRational Parse(string text)
        {
            static bool StartsWithSign(string text)
            {
                return text.StartsWith('+') || text.StartsWith('-');
            }

            var sign = true;

            if (StartsWithSign(text))
            {
                sign = StartsWithSign(text);
                text = text.Substring(1);
            }

            if (text.Contains('/'))
            {
                var split = text.Split('/');
                return new SchemeRational(
                    BigInteger.Parse(split[0]), 
                    BigInteger.Parse(split[1])
                    ).AttachSign(sign);
            }
            
            if (text.Contains('.'))
            {
                var split = text.Split('/');
                var integral = BigInteger.Parse(split[0]);
                var fractional = BigInteger.Parse(split[1]);
                var pow = BigInteger.Pow(10, split[1].Length);
                return new SchemeRational(
                    integral * pow + fractional, 
                    pow 
                    ).AttachSign(sign);
            }

            return new SchemeRational(BigInteger.Parse(text), 1).AttachSign(sign);
        }

        public override string ToString()
        {
            if (Nominator == 0)
            {
                return "0";
            }
            else if (Denominator == 1)
            {
                return Nominator.ToString();
            }
            else
            {
                return $"{Nominator}/{Denominator}";
            }
        }
    }

    public class SchemeComplex
    {
        public SchemeRational Real { get; }
        public SchemeRational Imaginary { get; }

        public SchemeComplex(SchemeRational real, SchemeRational imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }
        
        public static SchemeComplex operator+(SchemeComplex left, SchemeComplex right)
        {
            return new SchemeComplex(left.Real + right.Real, left.Imaginary + right.Imaginary);
        }
        
        public static SchemeComplex operator-(SchemeComplex left, SchemeComplex right)
        {
            return new SchemeComplex(left.Real - right.Real, left.Imaginary - right.Imaginary);
        }
        
        public static SchemeComplex operator*(SchemeComplex left, SchemeComplex right)
        {
            var prodRr = left.Real * right.Real;
            var prodRi = left.Real * right.Imaginary;
            var prodIr = left.Imaginary * right.Real;
            var prodIi = left.Imaginary * right.Imaginary;
            return new SchemeComplex(prodRr - prodIi, prodRi + prodIr);
        }

        public override string ToString()
        {
            if (Imaginary.Nominator == 0)
            {
                return Real.ToString();
            }
            else if (Real.Nominator == 0)
            {
                return $"{Imaginary}i";
            }
            else if(Imaginary.Nominator > 0)
            {
                return $"{Real}+{Imaginary}i";
            }
            else
            {
                return $"{Real}{Imaginary}i";
            }
        }
    }

    public class SchemeNumber : ISchemeValue
    {
        public ISchemeValue.KindCode Kind => ISchemeValue.KindCode.Number;
        
        public SchemeComplex Value { get; }

        public SchemeNumber(SchemeComplex value)
        {
            Value = value;
        }
        
        public static SchemeNumber operator+(SchemeNumber left, SchemeNumber right)
        {
            return new SchemeNumber(left.Value + right.Value);
        }
        
        public static SchemeNumber operator-(SchemeNumber left, SchemeNumber right)
        {
            return new SchemeNumber(left.Value - right.Value);
        }
        
        public static SchemeNumber operator*(SchemeNumber left, SchemeNumber right)
        {
            return new SchemeNumber(left.Value * right.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class SchemeString : ISchemeValue
    {
        public ISchemeValue.KindCode Kind => ISchemeValue.KindCode.String;
        
        public string Value { get; }

        public SchemeString(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public class SchemePair : ISchemeValue
    {
        public ISchemeValue.KindCode Kind => ISchemeValue.KindCode.Pair;
        
        public ISchemeValue Left { get; }
        public ISchemeValue Right { get; }
        public (ISchemeValue, ISchemeValue) Pair => (Left, Right);

        public SchemePair(ISchemeValue left, ISchemeValue right)
        {
            Left = left;
            Right = right;
        }

        public override string ToString()
        {
            if (Scheme.IsList(this))
            {
                var builder = new StringBuilder();
                builder.Append("( ");
                Scheme.ForEachInList(this, item =>
                {
                    builder.Append(item).Append(" ");
                });
                builder.Append(")");
                return builder.ToString();
            }
            return $"({Left} . {Right})";
        }
    }

    public class SchemeVector : ISchemeValue
    {
        public ISchemeValue.KindCode Kind => ISchemeValue.KindCode.Vector;

        public ISchemeValue[] Values { get; }

        public int Size => Values.Length;
        
        public ISchemeValue this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }

        public SchemeVector(IEnumerable<ISchemeValue> values)
        {
            Values = values.ToArray();
        }

        public SchemeVector(int size)
        {
            Values = new ISchemeValue[size];
        }
    }

    public class SchemeProcedure : ISchemeValue
    {
        public ISchemeValue.KindCode Kind => ISchemeValue.KindCode.Procedure;

        public Action<ISchemeValue, SchemeContext, Continuation> Value { get; }

        public SchemeProcedure(Action<ISchemeValue, SchemeContext, Continuation> value)
        {
            Value = value;
        }

        public void Invoke(ISchemeValue args, SchemeContext context, Continuation continuation)
        {
            Value.Invoke(args, context, continuation);
        }
    }

    public class SchemeSymbol : ISchemeValue
    {
        public ISchemeValue.KindCode Kind => ISchemeValue.KindCode.Symbol;
        
        public string Value { get; }

        public SchemeSymbol(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public class SchemeNull : ISchemeValue
    {
        public ISchemeValue.KindCode Kind => ISchemeValue.KindCode.Null;
        
        private SchemeNull(){}

        public static SchemeNull Instance { get; }

        static SchemeNull()
        {
            Instance = new SchemeNull();
        }

        public override string ToString()
        {
            return "'()";
        }
    }

}