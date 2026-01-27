using System;
using EasyTextEffects;
using Geometry;
using PrimeTween;
using TMPEffects.Components;
using TMPEffects.Parameters;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using PicoTween;

public class LessonTextElement : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private TMPAnimator _tmpAnimator;
    [SerializeField] private TMPWriter _tmpWriter;
    public event Action OnFinish;
    private bool evented = false;
    private const string ending = "<wave> ... </wave>";
    public void Init(string text) {
        _textMeshPro.text = text + ending;
    }
    public void Skip() {
        if (_tmpWriter.IsWriting) {
            _tmpWriter.SkipWriter();
        }
        else if (!evented) {
            evented = true;
            _tmpWriter.enabled = false;
            FadeOut();
        }
    }

    private void FadeOut() {
        _textMeshPro.alpha.Tween(() => _textMeshPro.alpha, value => _textMeshPro.alpha = value, 0.25f, 0.25f, onComplete: () => {
            _textMeshPro.SetText(_textMeshPro.text.Remove(_textMeshPro.text.Length - ending.Length, ending.Length));
            OnFinish?.Invoke();
        });
    }
}

