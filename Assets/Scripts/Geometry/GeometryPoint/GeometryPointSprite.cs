using Geometry;
using UnityEngine;

public class GeometryPointSprite : MonoBehaviour, ICameraListener {
    [SerializeField] private Color _color;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Vector2 _initialScale;
    [SerializeField] private int _sortingOrder;

    private void Awake() {
        FieldCamera.OnCameraChanged += OnCameraChanged;
        transform.localScale = _initialScale;
        _spriteRenderer.sortingOrder = _sortingOrder;
        _spriteRenderer.color = _color;
    }
    private void OnDisable() {
        FieldCamera.OnCameraChanged -= OnCameraChanged;
    }
    public void OnCameraChanged() {
        transform.localScale = _initialScale * 1f / FieldCamera.Instance.ZoomLevel;
    }
}
