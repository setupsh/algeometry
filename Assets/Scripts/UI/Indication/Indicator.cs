using Geometry;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Algebra;

namespace UI {
    public abstract class IndicatorInfo {
        public string BoardMenuCaption { get; protected set; }
        public string UICaption { get; protected set; }

        public IndicatorInfo(string boardMenuCaption, string uiCaption) {
            BoardMenuCaption = boardMenuCaption;
            UICaption = uiCaption;
        }
        public abstract AlgebraExpression GetExpression();
    }

    public class AlgebraExpressionInfo : IndicatorInfo {
        private AlgebraExpression expression;
        public AlgebraExpressionInfo(AlgebraExpression expression, string boardMenuCaption, string uiCaption) : base(boardMenuCaption, uiCaption) {
            this.expression = expression;
        }
        public override AlgebraExpression GetExpression() {
            return expression;
        }
    }
    
    [RequireComponent(typeof(RectTransform))]
    public class Indicator : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _caption;
        [SerializeField] private RectTransform _root;
        private AlgebraExpressionView expressionView;
        private AlgebraExpression currentExpression;
        private IndicatorInfo indicatorInfo;
        public void SetInfo(IndicatorInfo info) {
            _caption.text = info.UICaption;
            currentExpression = info.GetExpression();
            expressionView = Board.AlgebraExpressionViewGenerator.Generate(currentExpression, _root);
            indicatorInfo = info;
            UpdateInfo();
        }
        public void UpdateInfo() {
            AlgebraExpression newExpression = indicatorInfo.GetExpression();
            if (currentExpression.SameType(newExpression)) {
                expressionView.UpdateValue();
            }
            else {
                expressionView.Dispose();
                currentExpression = newExpression;
                expressionView = Board.AlgebraExpressionViewGenerator.Generate(currentExpression, _root);
            }
        }
        public void Delete() {
            Destroy(gameObject);
        }
    }
}
