using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Algebra;
using Geometry;
using UI;
using Random = UnityEngine.Random;

namespace MiniGames { 
    public class AngleMiniGameController : MonoBehaviour {
        [SerializeField] private GameObject _questionDisplay;
        [SerializeField] private GameObject _correct;
        [SerializeField] private GameObject _wrong;
        public Dictionary<string, int> QuestionPool = new Dictionary<string, int>() {
            { "0", 0 }, { "90", 4 }, { "180", 8 }, { "270", 12 }, { "360", 0 },
            
            { "pi / 6", 1 }, { "pi / 4", 2 }, { "pi / 3", 3 }, { "pi / 2", 4 },
            { "pi", 8 }, { "(3 * pi) / 2", 12 }, { "2 * pi", 0 },
            
            { "(3 * pi) / 4", 6 }, { "(5 * pi) / 6", 7 }, { "(7 * pi) / 4", 14 },
            
            { "-90", 12 }, 
            { "-pi / 2", 12 }, 
            { "-pi", 8 },
            { "-30", 15 }, 
            
            { "450", 4 },
            { "10 * pi", 0 },
            { "(13 * pi) / 6", 1 }, 
            { "720", 0 }
        };
        private int _currentAnswer;
        private AlgebraExpressionView view;

        public void CompareWithAnswer(int answer) {
            if (answer == _currentAnswer)
                StartCoroutine(CorrectAnswer());
            else
                StartCoroutine(WrongAnswer());
        }

        private void Start() {
            DisplayQuestion();
        }

        private void DisplayQuestion() {
            if (view != null)
                view.Dispose();
            List<string> keys = new List<string>(QuestionPool.Keys);
            string randomQuestion = keys[Random.Range(0, keys.Count)];
            _currentAnswer = QuestionPool[randomQuestion];
            AlgebraExpression questionExpression = new AlgebraExpressionParser().Parse(Lexer.Tokenize(randomQuestion));
            view = Board.AlgebraExpressionViewGenerator.Generate(questionExpression, _questionDisplay.transform);
        }
        

        private IEnumerator CorrectAnswer() {
            _correct.SetActive(true);
            yield return new WaitForSeconds(1);
            _correct.SetActive(false);
            DisplayQuestion();

        }
        
        private IEnumerator WrongAnswer() {
            _wrong.SetActive(true);
            yield return new WaitForSeconds(1);
            _wrong.SetActive(false); 
            DisplayQuestion();

        } 
    }
}