using UnityEngine;

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

public abstract class ReturnLessonAction<T> : LessonAction {
    public abstract T Value { get; protected set; }
}