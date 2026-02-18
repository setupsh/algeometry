using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using PrimeTween;

namespace Geometry {
    public class FieldCamera : MonoBehaviour {
        public static FieldCamera Instance { get; private set;}
        [SerializeField] private Camera _camera;
        [SerializeField] private float _moveMultiplier;
        [SerializeField] private float _scrollMultiplier;
        [SerializeField] private float _maxOrthographicSize, _minOrthographicSize;
        [SerializeField] private AnimationCurve _zoomCurve;
        [SerializeField] private float _zoomDuration;
        [SerializeField] private float _defualtOrthographicSize;
        public Camera Camera => _camera;
        private bool zoomBlocked;
        private float defaultOrthograhicSize = 5f;
        public static event System.Action OnCameraChanged;
        public static event System.Action OnCameraMove;
        public static event System.Action OnCameraZoom;
        
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;
                defaultOrthograhicSize = _camera.orthographicSize;
            }
            else {
                Destroy(this);
            }
        }

        public void ForceCameraUpdate() {
            OnCameraChanged?.Invoke();
            OnCameraMove?.Invoke();
            OnCameraZoom?.Invoke();
        }

        private void LateUpdate() {
            if (InputListener.MiddleButtonPressed) {
                MoveCamera(InputListener.MouseDelta);
            }
        }

        public void MoveCamera(Vector2 delta) {
            _camera.transform.position -= (Vector3) delta * (Time.deltaTime * _moveMultiplier * CeilSize());
            OnCameraChanged?.Invoke();
            OnCameraMove?.Invoke();
        }

        public void ZoomCamera(int direction) {
            if (direction >= 1) {
                if (_camera.orthographicSize > _minOrthographicSize && !zoomBlocked) {
                    StartCoroutine(ZoomCamera(_camera.orthographicSize / 1.2f));
                }
            }
            else if (direction <= -1) {
                if (_camera.orthographicSize < _maxOrthographicSize && !zoomBlocked) {
                    StartCoroutine(ZoomCamera(_camera.orthographicSize * 1.2f));
                }
            }
        }

        private IEnumerator ZoomCamera(float targetOrthographicSize) {
            zoomBlocked = true;
            float initialOrthographicSize = _camera.orthographicSize;
            for (float t = 0; t < 1f; t += (Time.deltaTime / _zoomDuration)) {
                _camera.orthographicSize = Mathf.Lerp(initialOrthographicSize, targetOrthographicSize, _zoomCurve.Evaluate(t));
                //OnCameraZoom?.Invoke();
                //OnCameraChanged?.Invoke();
                yield return null;
            }
            _camera.orthographicSize = targetOrthographicSize;
            OnCameraChanged?.Invoke();
            OnCameraZoom?.Invoke();
            zoomBlocked = false;
        }
        
        public float ZoomLevel => defaultOrthograhicSize / _camera.orthographicSize;

        public Vector2 ToWorldPoint(Vector2 position) {
            return _camera.ScreenToWorldPoint(position);
        }

        public Vector2 ToScreenPoint(Vector2 position) {
            return _camera.WorldToScreenPoint(position);
        }
        
        public Bounds GetCameraBounds() {
            float screenAspect = Instance.Camera.aspect;
            float cameraHeight = Instance.Camera.orthographicSize * 2;
            Bounds bounds = new Bounds(
                Instance.Camera.transform.position,
                new Vector2(cameraHeight * screenAspect, cameraHeight));
            return bounds;
        }

        public float CeilSize() {
            return NiceStep(1 / ZoomLevel);
        }
        public float NiceStep(float rawStep) {
            float exponent = Mathf.Floor(Mathf.Log10(rawStep));
            float fraction = rawStep / Mathf.Pow(10f, exponent);

            float niceFraction;
            if (fraction < 1.5f)
                niceFraction = 1f;
            else if (fraction < 3f)
                niceFraction = 2f;
            else if (fraction < 7f)
                niceFraction = 5f;
            else
                niceFraction = 10f;

            return niceFraction * Mathf.Pow(10f, exponent);
        }
    }
}
