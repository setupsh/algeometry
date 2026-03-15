using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonSummary : MonoBehaviour {
    [SerializeField] private RectTransform _content;
    [SerializeField] private Animator _animator;

    private Queue<RectTransform> _pending = new Queue<RectTransform>();
    private bool _isOpen = false;
    
    public void OnOpenAnimationComplete() {
        _isOpen = true;
        FlushPending();
    }
    
    public void OnCloseAnimationComplete() {
        _isOpen = false;
        ClearContent();
    }

    public void AddSummary(RectTransform summary) {
        summary.SetParent(_content);
        _pending.Enqueue(summary);

        if (!_isOpen) {
            _animator.SetTrigger(AnimatorKeys.Open);
        } else {
            FlushPending();
        }
    }

    public void Close() {
        _animator.SetTrigger(AnimatorKeys.Close);
    }

    private void FlushPending() {
        while (_pending.Count > 0) {
            var summary = _pending.Dequeue();
            summary.gameObject.SetActive(true);
        }
    }

    private void ClearContent() {
        foreach (Transform child in _content) {
            Destroy(child.gameObject);
        }
    }
}
[System.Serializable]
public class LessonSummaryInstance {
    public string Content;
    public bool PlainText;
}
