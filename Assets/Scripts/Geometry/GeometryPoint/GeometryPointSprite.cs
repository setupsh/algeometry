using Geometry;
using UnityEngine;

public class GeometryPointSprite : MonoBehaviour, ICameraListener {
    [SerializeField] private Color _color;
    [SerializeField] private Color _blockedColor;
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

    public void SetColorByBlocked(bool blocked) {
        _spriteRenderer.color = blocked ? _blockedColor : _color;
    }
    public void OnCameraChanged() {
        transform.localScale = _initialScale * FieldCamera.Instance.CeilSize();
    }
}
