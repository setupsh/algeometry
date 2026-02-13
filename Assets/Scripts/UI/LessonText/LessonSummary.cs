using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonSummary : MonoBehaviour {
    [SerializeField] private RectTransform _content;
    [SerializeField] private Animator _animator;
    public void AddSummary(RectTransform summary) {
        _animator.SetTrigger(AnimatorKeys.Open);
        summary.SetParent(_content);
    }

    public void Clear() {
        _animator.SetTrigger(AnimatorKeys.Close);
        foreach (Transform summary in _content.GetComponentInChildren<Transform>()) {
            Destroy(summary.gameObject);
        }
    }
}
