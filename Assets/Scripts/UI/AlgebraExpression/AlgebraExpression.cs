using UnityEngine;
using System;
using Geometry;

namespace Algebra {
    public abstract class AlgebraExpression {
        public abstract double Evaluate();
        
        public abstract bool SameType(AlgebraExpression other);
    }

    public class VariableExpression : AlgebraExpression {
        private Func<double> getter;
        public string Name { get; private set; }
        
        public VariableExpression(Func<double> function, string name) {
            getter = function;
            Name = name;
        }
        public VariableExpression(double constant, string name) {
            getter = () => constant;
            Name = name;
        }
        public override double Evaluate() => getter();

        public override bool SameType(AlgebraExpression other) {
            return other is VariableExpression variableExpression && getter == variableExpression.getter;
        }
    }

    public class FunctionExpression : AlgebraExpression {
        private Func<double, double> function;
        public AlgebraExpression Argument;
        public string Name { get; private set; }

        public FunctionExpression(AlgebraExpression argument, Func<double, double> function, string displayName) {
            Argument = argument;
            Name = displayName;
            this.function = function;
        }
        public override double Evaluate() {
            return function(Argument.Evaluate());
        }

        public override bool SameType(AlgebraExpression other) {
            return other is FunctionExpression functionExpression && Argument.SameType(functionExpression.Argument) &&
                   Name == functionExpression.Name;
        }
    }

    public class NumberExpression : AlgebraExpression {
        private Func<double> getter;
        
        public NumberExpression(Func<double> function) {
            getter = function;
        }
        public NumberExpression(double constant) {
            getter = () => constant;
        }
        public override double Evaluate() => getter();

        public override bool SameType(AlgebraExpression other) {
            return other is NumberExpression;
        }
    }

    public class VectorExpression : AlgebraExpression {
        public AlgebraExpression X { get; }
        public AlgebraExpression Y { get; }

        public VectorExpression(AlgebraExpression x, AlgebraExpression y) {
            X = x;
            Y = y;
        }
        public override double Evaluate() {
            return Math.Sqrt(X.Evaluate() * X.Evaluate() + Y.Evaluate() * Y.Evaluate());
        }

        public override bool SameType(AlgebraExpression other) {
            return other is VectorExpression vectorExpression && vectorExpression.X.SameType(X) && vectorExpression.Y.SameType(Y);
        }
    }
    public class SumExpression : AlgebraExpression {
        public AlgebraExpression Left { get; }
        public AlgebraExpression Right { get; }

        public SumExpression(AlgebraExpression left, AlgebraExpression right) {
            Left = left;
            Right = right;
        }
        
        public override double Evaluate() => Left.Evaluate() + Right.Evaluate();
        public override bool SameType(AlgebraExpression other) {
            if (other is SumExpression otherSum) {
                return Left.SameType(otherSum.Left) && Right.SameType(otherSum.Right);
            }
            return false;
        }
    }
    public class SubtractExpression : AlgebraExpression {
        public AlgebraExpression Left { get; }
        public AlgebraExpression Right { get; }

        public SubtractExpression(AlgebraExpression left, AlgebraExpression right) {
            Left = left;
            Right = right;
        }
        public override double Evaluate() => Left.Evaluate() - Right.Evaluate();
        public override bool SameType(AlgebraExpression other) {
            if (other is SubtractExpression otherSubtract) {
                return Left.SameType(otherSubtract.Left) && Right.SameType(otherSubtract.Right);
            }
            return false;
        }
    }
    public class FractionExpression : AlgebraExpression {
        public AlgebraExpression Numerator { get; }
        public AlgebraExpression Denominator { get; }

        public FractionExpression(AlgebraExpression numerator, AlgebraExpression denominator) {
            Numerator = numerator;
            Denominator = denominator;
        }

        public override double Evaluate() {
            double d = Denominator.Evaluate();
            if (Math.Abs(d) < 1e-9)
                return double.NaN;
            return Numerator.Evaluate() / d;
        }
        public override bool SameType(AlgebraExpression other) {
            if (other is FractionExpression otherFraction) {
                return Numerator.SameType(otherFraction.Numerator) && Denominator.SameType(otherFraction.Denominator);
            }
            return false;
        }
    }

    public class MulExpression : AlgebraExpression {
        public AlgebraExpression Left { get; }
        public AlgebraExpression Right { get; }

        public MulExpression(AlgebraExpression left, AlgebraExpression right) {
            Left = left;
            Right = right;
        }
        public override double Evaluate() {
            return Left.Evaluate() * Right.Evaluate();
        }

        public override bool SameType(AlgebraExpression other) {
            if (other is MulExpression otherMul) {
                return Left.SameType(otherMul.Left) && Right.SameType(otherMul.Right);
            }
            return false;
        }
    }
    public sealed class SqrtExpression : AlgebraExpression {
        public AlgebraExpression Inner { get; }

        public SqrtExpression(AlgebraExpression inner) {
            Inner = inner;
        }

        public override double Evaluate() {
            double v = Inner.Evaluate();
            if (v < 0)
                return double.NaN;

            return Math.Sqrt(v);
        }

        public override bool SameType(AlgebraExpression other) {
            if (other is SqrtExpression otherSqrt) {
                return Inner.SameType(otherSqrt.Inner);
            }
            return false;
        }
    }

    public class PowExpression : AlgebraExpression {
        public AlgebraExpression Base { get; }
        public AlgebraExpression Exponent { get; }

        public PowExpression(AlgebraExpression _base, AlgebraExpression exponent) {
            Base = _base;
            Exponent = exponent;
        }
        
        public override double Evaluate() {
            return Math.Pow(Base.Evaluate(), Exponent.Evaluate());
        }

        public override bool SameType(AlgebraExpression other) {
            if (other is PowExpression powExpression) {
                return Base.SameType(powExpression.Base) && Exponent.SameType(powExpression.Exponent);
            }
            return false;
        }
    }
}