using System;
using System.Collections.Generic;
using UnityEngine;

public class LessonText : MonoBehaviour {
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _content;
    
    private List<LessonTextElement> elements = new List<LessonTextElement>();
    private int _currentIndex = 0;
    
    public event Action OnAllTextFinished;

    public void Invoke(List<LessonTextElementInstance> dataList) {
        Clear();
        
        foreach (var data in dataList) {
            LessonTextElement instance = Instantiate(data.Prefab, _content);
            instance.Init(data.Text);
            elements.Add(instance);
        }

        _animator.SetTrigger(AnimatorKeys.Open);
        ShowText(0);
    }

    private void ShowText(int index) {
        elements[index].gameObject.SetActive(true);
        elements[index].OnFinish += () => {
            if (index < elements.Count - 1) {
                ShowText(index + 1);
            }
            else {
                FinishSequence();
                Clear();
            }
        };
    }

    private void FinishSequence() {
        _animator.SetTrigger(AnimatorKeys.Close);
        OnAllTextFinished?.Invoke();
    }
    

    private void Clear() {
        foreach (var element in elements) {
            if (element != null) Destroy(element.gameObject);
        }
        elements.Clear();
    }
}
[System.Serializable]

public class LessonTextElementInstance {

    [TextArea] public string Text;

    public LessonTextElement Prefab;

} 