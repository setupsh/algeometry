using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace PicoTween {
    public class PicoRunner : MonoBehaviour {
        private static PicoRunner _instance;
        public static PicoRunner Instance {
            get {
                if (!_instance) {
                    _instance = new GameObject("PicoTween").AddComponent<PicoRunner>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }
    }
    public static class Tweener {
        private static AnimationCurve defualtCurve = AnimationCurves.InOut;
        private static IEnumerator Tween<T>(Func<T> getter, Action<T> setter, T endValue, float duration, Func<T, T, float, T> lerper, AnimationCurve curve, Action onComplete) {
            T startValue = getter();
            for (float t = 0f; t < 1f; t += Time.deltaTime / duration) {
                if (getter == null) yield break;
                setter(lerper(startValue, endValue, curve.Evaluate(t)));
                yield return null;
            }
            setter(endValue);
            onComplete?.Invoke();
        }
        public static void Tween(this float self, Func<float> getter, Action<float> setter, float endValue, float duration, AnimationCurve curve = null, Action onComplete = null) {
            PicoRunner.Instance.StartCoroutine(Tween(getter, setter, endValue, duration, Mathf.Lerp, curve ?? defualtCurve, onComplete));
        }
        
        public static void Tween(this Vector2 self, Vector2 endValue, float duration, AnimationCurve curve, Action onComplete) {
            PicoRunner.Instance.StartCoroutine(Tween(() => self, value => self = value, endValue, duration, Vector2.Lerp, curve ?? defualtCurve, onComplete));
        }
        public static void Tween(this Vector3 self, Vector3 endValue, float duration, AnimationCurve curve, Action onComplete) {
            PicoRunner.Instance.StartCoroutine(Tween(() => self, value => self = value, endValue, duration, Vector3.Lerp, curve ?? defualtCurve, onComplete));
        }
        public static void Tween(this Image self, Color endValue, float duration, AnimationCurve curve, Action onComplete) {
            PicoRunner.Instance.StartCoroutine(Tween(() => self.color, value => self.color = value, endValue, duration, Color.Lerp, curve ?? defualtCurve, onComplete));
        }
        public static void Tween(this Quaternion self, Quaternion endValue, float duration, AnimationCurve curve, Action onComplete) {
            PicoRunner.Instance.StartCoroutine(Tween(() => self, value => self = value, endValue, duration, Quaternion.Lerp, curve ?? defualtCurve, onComplete));
        }
    }

    public static class AnimationCurves {
        public static AnimationCurve Linear => AnimationCurve.Linear(0, 0, 1, 1);
        
        public static AnimationCurve In = new AnimationCurve(
            new Keyframe(0, 0, 0, 0), 
            new Keyframe(1, 1, 2, 0)
        );
        
        public static AnimationCurve Out = new AnimationCurve(
            new Keyframe(0, 0, 0, 2), 
            new Keyframe(1, 1, 0, 0)
        );
        
        public static AnimationCurve InOut = AnimationCurve.EaseInOut(0, 0, 1, 1);
        
        public static AnimationCurve BackIn = new AnimationCurve(
            new Keyframe(0, 0, 0, -1f), 
            new Keyframe(1, 1, 2, 0)
        );
        
        public static AnimationCurve Bounce = new AnimationCurve(
            new Keyframe(0, 0),
            new Keyframe(0.3f, 1),
            new Keyframe(0.5f, 0.7f),
            new Keyframe(0.7f, 1),
            new Keyframe(0.85f, 0.9f),
            new Keyframe(1, 1)
        );
    }
}