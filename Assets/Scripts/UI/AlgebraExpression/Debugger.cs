using System;
using Algebra;
using Geometry;
using UI;
using UnityEngine;

namespace Con {
    public class Debugger : MonoBehaviour {
        [SerializeField] private string source;

        [ContextMenu("Parse")]
        public void Parse() {
            AlgebraExpressionParser parser = new();
            Debug.Log(parser.Parse(Lexer.Tokenize(source)).Evaluate());
        }
        [ContextMenu("Indicator")]
        public void Indicator() {
            AlgebraExpressionParser parser = new();
            Debug.Log(parser.Parse(Lexer.Tokenize(source)));
            Debug.Log((new AlgebraExpressionInfo(parser.Parse(Lexer.Tokenize(source)), " ", " ")).GetExpression());
        }
    }
}