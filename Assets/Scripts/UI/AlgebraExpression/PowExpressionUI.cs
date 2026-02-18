using UnityEngine;

namespace UI {
    public class PowExpressionUI : AlgebraExpressionUI {
        [SerializeField] private RectTransform _base;
        [SerializeField] private RectTransform _exponent;
        [SerializeField] private PowSpacer _spacer;

        public RectTransform Base => _base;

        public RectTransform Exponent => _exponent;

        public PowSpacer Spacer => _spacer;
    }
}