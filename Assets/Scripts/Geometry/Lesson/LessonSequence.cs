using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
public class LessonSequence : MonoBehaviour {
    private List<LessonAction> actions = new List<LessonAction>();
    private int currentIndex;

    private void Start() {
        foreach (Transform child in transform) {
            actions.Add(child.GetComponent<LessonAction>());
        }
        StartAction(0);
    }

    private void StartAction(int index) {
        currentIndex = index;
        actions[currentIndex].Launch(this);
    }

    public void GoToNextAction() {
        currentIndex++;
        if (currentIndex >= actions.Count)
            return;
        actions[currentIndex].Launch(this);
    }
    
}