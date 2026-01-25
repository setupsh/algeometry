using UnityEngine;

namespace Geometry {
    public abstract class LessonAction : MonoBehaviour {
        protected LessonSequence sequence;

        public void Launch(LessonSequence sequence) {
            this.sequence = sequence;
            Enter();
        }
        
        protected abstract void Enter();
        
        protected void Complete() {
            sequence.GoToNextAction();    
        }
    }
}