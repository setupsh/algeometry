using UnityEngine;
using TMPro;
namespace UI {
    public class FunctionExpressionUI : AlgebraExpressionUI {
        [SerializeField] private TextMeshProUGUI _functionName;
        public TextMeshProUGUI FunctionName => _functionName;
    }
}