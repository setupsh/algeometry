using Algebra;
using UnityEngine;
using Geometry;

namespace UI {
    public class AlgebraExpressionViewGenerator {
        public AlgebraExpressionView Generate(AlgebraExpression expression, Transform parent) {
            return expression switch {
                NumberExpression numberExpression => new NumberExpressionView(numberExpression, parent, Geometry.Resources.NumberExpressionPrefab),
                VariableExpression variableExpression => new VariableExpressionView(variableExpression, parent, Geometry.Resources.NumberExpressionPrefab),
                VectorExpression vectorExpression => new VectorExpressionView(vectorExpression, parent, this, Geometry.Resources.VectorExpressionPrefab),
                SumExpression sumExpression => new SumExpressionView(sumExpression, parent, this, Geometry.Resources.SumExpressionPrefab),
                SubtractExpression subtractExpression => new SubtractExpressionView(subtractExpression, parent, this, Geometry.Resources.SubtractExpressionPrefab),
                FractionExpression fractionExpression => new FractionExpressionView(fractionExpression, parent, this, Geometry.Resources.FractionExpressionPrefab),
                MulExpression mulExpression => new MulExpressionView(mulExpression, parent, this, Geometry.Resources.MulExpressionPrefab),
                SqrtExpression sqrtExpression => new SqrtExpressionView(sqrtExpression, parent, this, Geometry.Resources.SqrtExpressionPrefab),
                _ => throw new System.Exception("Unknown expression type")
            };
        }
    }
}