using UnityEngine;
using System.Collections.Generic;
using Geometry;

namespace UI {
    public class TestLessonText : MonoBehaviour {
        [SerializeField] private List<LessonTextElementInstance> _elements;

        [ContextMenu("Add Elements")]
        public void AddElements() {
            Board.LessonText.Invoke(_elements);
        }
    }
}