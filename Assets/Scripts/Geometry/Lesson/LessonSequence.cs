using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
public class LessonSequence : MonoBehaviour {
    [SerializeField] private bool _autoStart;
    public bool AutoStart => _autoStart;
    private List<LessonAction> actions = new List<LessonAction>();
    private int currentIndex;
    public event System.Action OnEnd;

    private void Start() {
        if (_autoStart)
            Launch();
    }

    public void Launch() {
        if (actions.Count > 0) throw new SystemException("Already launched");
        foreach (Transform child in transform) {
            if (child.gameObject.activeSelf)
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
        if (currentIndex >= actions.Count) {
            OnEnd?.Invoke();
            return;
        }
        actions[currentIndex].Launch(this);
    }
}