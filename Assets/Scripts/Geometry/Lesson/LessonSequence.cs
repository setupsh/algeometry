using UnityEngine;

public class LessonSequence : MonoBehaviour {
    private LessonAction[] actions;
    private int currentIndex;

    private void Start() {
        actions = GetComponentsInChildren<LessonAction>();
        StartAction(0);
    }

    private void StartAction(int index) {
        currentIndex = index;
        actions[currentIndex].Launch(this);
    }

    public void GoToNextAction() {
        currentIndex++;
        if (currentIndex >= actions.Length)
            return;
        actions[currentIndex].Launch(this);
    }
    
}