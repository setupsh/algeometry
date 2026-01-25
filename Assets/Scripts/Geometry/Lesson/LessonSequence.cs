using UnityEngine;

namespace Geometry {
    public class LessonSequence : MonoBehaviour {
        [SerializeField] private LessonAction[] _actions;
        private int currentIndex;

        private void Start() {
            StartAction(0);
        }

        private void StartAction(int index) {
            currentIndex = index;
            _actions[currentIndex].Launch(this);
        }

        public void GoToNextAction() {
            currentIndex++;
            if (currentIndex >= _actions.Length)
                return;
            _actions[currentIndex].Launch(this);
        }
    }
}