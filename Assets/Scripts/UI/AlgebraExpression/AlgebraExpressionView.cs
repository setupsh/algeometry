using System;
using UnityEngine;
using UnityEngine.UI;
using Algebra;
using Geometry;
using TMPro;
using Object = UnityEngine.Object;

namespace UI {
    public abstract class AlgebraExpressionView {
        public abstract RectTransform Root { get; }
        public abstract void UpdateValue();

        public virtual void Dispose() {
            Object.Destroy(Root.gameObject);
        }
        public virtual float GetTotalWidth() {
            return LayoutUtility.GetPreferredWidth(Root);
        }
        
        public virtual float GetTotalHeight() {
            return LayoutUtility.GetPreferredHeight(Root);
        }
        public virtual float GetBaselineHeight() {
            return GetTotalHeight();
        }
    }

    public sealed class VariableExpressionView : AlgebraExpressionView {
        private VariableExpression expression;
        private TextMeshProUGUI text;

        public VariableExpressionView(VariableExpression expression, Transform parent, TextMeshProUGUI prefab) {
            this.expression = expression;
            text = Object.Instantiate(prefab, parent);
            UpdateValue();
        }

        public override RectTransform Root => text.rectTransform;

        public override void UpdateValue() {
            text.text = expression.Name;
        }
    }
    public sealed class NumberExpressionView : AlgebraExpressionView {
        private NumberExpression expression;
        private TextMeshProUGUI text;

        public NumberExpressionView(NumberExpression expression, Transform parent, TextMeshProUGUI prefab) {
            this.expression = expression;
            text = Object.Instantiate(prefab, parent);
            UpdateValue();
        }

        public override RectTransform Root => text.rectTransform;

        public override void UpdateValue() {
            double value = expression.Evaluate();
            text.text = expression.Evaluate().ToString(value % 1 == 0 ? "N0" : "F3");
        }
    }

    public sealed class FunctionExpressionView : AlgebraExpressionView {
        private readonly FunctionExpressionUI root;
        private readonly AlgebraExpressionView argument;

        public FunctionExpressionView(FunctionExpression expression, Transform parent, AlgebraExpressionViewGenerator generator, FunctionExpressionUI functionPrefab) {
            root = Object.Instantiate(functionPrefab, parent);
            root.FunctionName.text = expression.Name;
            argument = generator.Generate(expression.Argument, Root);
            argument.Root.SetSiblingIndex(2);
        }

        public override RectTransform Root => root.Root;
        public override void UpdateValue() {
            argument.UpdateValue();
        }
    }

    public sealed class VectorExpressionView : AlgebraExpressionView {
        private readonly AlgebraExpressionUI root;
        private readonly AlgebraExpressionView x;
        private readonly AlgebraExpressionView y;

        public VectorExpressionView(VectorExpression expression, Transform parent, AlgebraExpressionViewGenerator generator, AlgebraExpressionUI vectorPrefab) {
            root = Object.Instantiate(vectorPrefab, parent);
            x = generator.Generate(expression.X, root.Root);
            x.Root.SetSiblingIndex(1);
            y = generator.Generate(expression.Y, root.Root);
            y.Root.SetSiblingIndex(3);
        }

        public override RectTransform Root => root.Root;

        public override void UpdateValue() {
            x.UpdateValue();
            y.UpdateValue();
        }

        public override void Dispose() {
            x.Dispose();
            y.Dispose();
            base.Dispose();
        }
    }

    public sealed class FractionExpressionView : AlgebraExpressionView {
        private readonly AlgebraExpressionUI root;
        private readonly AlgebraExpressionView numerator;
        private readonly AlgebraExpressionView denominator;

        public FractionExpressionView(FractionExpression expression, Transform parent, AlgebraExpressionViewGenerator generator, AlgebraExpressionUI fractionPrefab) {
            root = Object.Instantiate(fractionPrefab, parent);
            numerator = generator.Generate(expression.Numerator, root.Root);
            numerator.Root.SetAsFirstSibling();
            denominator = generator.Generate(expression.Denominator, root.Root);
            denominator.Root.SetAsLastSibling();
        }

        public override RectTransform Root => root.Root;

        public override void UpdateValue() {
            numerator.UpdateValue();
            denominator.UpdateValue();
        }

        public override void Dispose() {
            numerator.Dispose();
            denominator.Dispose();
            base.Dispose();
        }
    }
    public sealed class MulExpressionView : AlgebraExpressionView {
        private AlgebraExpressionUI root;
        private AlgebraExpressionView left;
        private AlgebraExpressionView right;

        public MulExpressionView(MulExpression expression, Transform parent, AlgebraExpressionViewGenerator generator, AlgebraExpressionUI mulPrefab) {
            root = Object.Instantiate(mulPrefab, parent);
            left = generator.Generate(expression.Left, root.Root);
            left.Root.SetAsFirstSibling();
            right = generator.Generate(expression.Right, root.Root);
            right.Root.SetAsLastSibling();
        }

        public override RectTransform Root => root.Root;

        public override void UpdateValue() {
            left.UpdateValue();
            right.UpdateValue();
        }

        public override void Dispose() {
            left.Dispose();
            right.Dispose();
            base.Dispose();
        }
    }
    public sealed class SumExpressionView : AlgebraExpressionView {
        private AlgebraExpressionUI root;
        private AlgebraExpressionView left;
        private AlgebraExpressionView right;

        public SumExpressionView(SumExpression expression, Transform parent, AlgebraExpressionViewGenerator generator, AlgebraExpressionUI sumPrefab) {
            root = Object.Instantiate(sumPrefab, parent);
            left = generator.Generate(expression.Left, root.Root);
            left.Root.SetAsFirstSibling();
            right = generator.Generate(expression.Right, root.Root);
            right.Root.SetAsLastSibling();
        }

        public override RectTransform Root => root.Root;

        public override void UpdateValue() {
            left.UpdateValue();
            right.UpdateValue();
        }

        public override void Dispose() {
            left.Dispose();
            right.Dispose();
            base.Dispose();
        }
    }
    public sealed class SubtractExpressionView : AlgebraExpressionView {
        private AlgebraExpressionUI root;
        private AlgebraExpressionView left;
        private AlgebraExpressionView right;

        public SubtractExpressionView(SubtractExpression expression, Transform parent, AlgebraExpressionViewGenerator generator, AlgebraExpressionUI subtractPrefab) {
            root = Object.Instantiate(subtractPrefab, parent);
            if (!(expression.Left is NumberExpression numberExpression && numberExpression.Evaluate() == 0)) {
                left = generator.Generate(expression.Left, root.Root);
                left.Root.SetAsFirstSibling();
            }
            right = generator.Generate(expression.Right, root.Root);
            right.Root.SetAsLastSibling();
        }

        public override RectTransform Root => root.Root;

        public override void UpdateValue() {
            left?.UpdateValue();
            right.UpdateValue();
        }

        public override void Dispose() {
            left?.Dispose();
            right.Dispose();
            base.Dispose();
        }
    }
    public sealed class SqrtExpressionView : AlgebraExpressionView {
        private AlgebraExpressionUI root;
        private AlgebraExpressionView inner;

        public SqrtExpressionView(SqrtExpression expression, Transform parent, AlgebraExpressionViewGenerator generator, AlgebraExpressionUI fractionPrefab) {
            root = Object.Instantiate(fractionPrefab, parent);
            inner = generator.Generate(expression.Inner, root.Root);
            inner.Root.SetAsLastSibling();
        }

        public override RectTransform Root => root.GetComponent<RectTransform>();

        public override void UpdateValue() {
            inner.UpdateValue();
        }

        public override void Dispose() {
            inner.Dispose();
            base.Dispose();
        }
    }
    public sealed class PowExpressionView : AlgebraExpressionView {
        private PowExpressionUI root;
        private AlgebraExpressionView _base;
        private AlgebraExpressionView exponent;

        public PowExpressionView(PowExpression expression, Transform parent, AlgebraExpressionViewGenerator generator, PowExpressionUI powPrefab) {
            root = Object.Instantiate(powPrefab, parent);
            _base = generator.Generate(expression.Base, root.Base);
            exponent = generator.Generate(expression.Exponent, root.Exponent);
            //exponent.Root.pivot = Vector2.zero;
            exponent.Root.SetAsFirstSibling();
            _base.Root.SetAsFirstSibling();
            UpdateValue();
            LayoutRebuilder.ForceRebuildLayoutImmediate(Root);
        }

        public override float GetBaselineHeight() {
            return _base.GetBaselineHeight();
        }

        public override RectTransform Root => root.Root;

        public override void UpdateValue() {
            _base.UpdateValue();
            exponent.UpdateValue();
            float baseHeight = _base.GetTotalHeight();
            float exponentHeight = exponent.GetBaselineHeight();
            if (exponentHeight > baseHeight) {
                root.Spacer.SetHeight(exponentHeight);
            }
            else {
                root.Spacer.SetHeight(baseHeight);
            }
        }

        public override void Dispose() {
            _base.Dispose();
            exponent.Dispose();
            base.Dispose();
        }
    }
}