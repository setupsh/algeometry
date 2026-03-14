using System;
using PicoTween;
using UnityEngine;

namespace MiniGames {
    public class AngleMiniGamePoint : MonoBehaviour {
        [SerializeField] private AngleMiniGameController _controller;
        [SerializeField] private SpriteRenderer _insideSprite;
        [SerializeField] private int _answer;
        private Color _baseColor;
        private void Start() {
            _baseColor = _insideSprite.color;
        }

        private void OnMouseDown() {
            _controller.CompareWithAnswer(_answer);
            PicoTween.Tweener.Tween(_insideSprite, _baseColor, 0.075f, AnimationCurves.InOut, () => {
                PicoTween.Tweener.Tween(_insideSprite, Color.green, 0.075f, AnimationCurves.InOut, () => {
                    if (Vector2.Distance(Camera.main.ScreenToWorldPoint(InputListener.MousePosition), transform.position) > 0.5f) {
                        _insideSprite.color = _baseColor;
                    }
                });
            });

        }

        private void OnMouseEnter() {
            PicoTween.Tweener.Tween(_insideSprite, Color.green, 0.05f, AnimationCurves.InOut, () => {});
        }

        private void OnMouseExit() {
            PicoTween.Tweener.Tween(_insideSprite, _baseColor, 0.05f, AnimationCurves.InOut, () => {});
        }
    }
}