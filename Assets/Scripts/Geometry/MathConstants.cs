using System.Collections.Generic;
using Algebra;
using System;
using Geometry;

public class MathConstants {
    private static readonly Dictionary<double, Func<AlgebraExpression>> Constants = 
        new Dictionary<double, Func<AlgebraExpression>> {
            { Math.PI, () => new VariableExpression(() => Math.PI, "π") },
            { Math.E, () => new VariableExpression(() => Math.E, "e") },
            { Math.Sqrt(2), () => new SqrtExpression(new NumberExpression(2)) },
            { Math.Sqrt(3), () => new SqrtExpression(new NumberExpression(3)) },
            { Math.Sqrt(5), () => new SqrtExpression(new NumberExpression(5)) },
            { Math.Sqrt(8), () => new SqrtExpression(new NumberExpression(8)) },
            
            { 0.5, () => new FractionExpression(new NumberExpression(1), new NumberExpression(2)) },
            { Math.Sqrt(2)/2.0, () => new FractionExpression(new SqrtExpression(new NumberExpression(2)), new NumberExpression(2)) },
            { Math.Sqrt(3)/2.0, () => new FractionExpression(new SqrtExpression(new NumberExpression(3)), new NumberExpression(2)) },
            
            { Math.PI / 2.0, () => new FractionExpression(new VariableExpression(() => Math.PI, "π"), new NumberExpression(2)) },
            { Math.PI / 3.0, () => new FractionExpression(new VariableExpression(() => Math.PI, "π"), new NumberExpression(3)) },
            { Math.PI / 4.0, () => new FractionExpression(new VariableExpression(() => Math.PI, "π"), new NumberExpression(4)) },
            { Math.PI / 6.0, () => new FractionExpression(new VariableExpression(() => Math.PI, "π"), new NumberExpression(6)) },
            { Math.PI * 3.0 / 4.0, () => new FractionExpression(new MulExpression(new NumberExpression(3.0),new VariableExpression(() => Math.PI, "π")), new NumberExpression(4)) },
            { Math.PI * 7.0 / 4.0, () => new FractionExpression(new MulExpression(new NumberExpression(7.0),new VariableExpression(() => Math.PI, "π")), new NumberExpression(4)) },

        };
    public static bool TryToConstant(NumberExpression expression, out AlgebraExpression resultExpression) {
        double absValue = Math.Abs(expression.Evaluate());
        
        foreach (var kvp in Constants) {
            if (Math.Abs(absValue - kvp.Key) < Utilities.Epsilon) {
                resultExpression = expression.Evaluate() < 0 ? new SubtractExpression(new NumberExpression(() => 0), kvp.Value()) : kvp.Value();
                return true;
            }
        }
        resultExpression = null;
        return false;
    }
}
