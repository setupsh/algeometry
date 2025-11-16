using UnityEngine;
using PrimeTween;
namespace Geometry {
    public class Parameters : MonoBehaviour {
        public static Parameters Instance { get; private set; }
        private void Awake() {
            if (Instance) {
                Destroy(this);
            }
            else {
                Instance = this;
            }
        }
        [Header("UI")]
        [SerializeField] private float _defaultIndicatorSizeY;
        public float DefaultIndicatorSizeY => _defaultIndicatorSizeY;
        [Header("LineRenderer")]
        [SerializeField] private float _defaultLineWidth;
        public float DefaultLineWidth => _defaultLineWidth;
        
        [Header("Text")]
        [SerializeField] private int _defaultLabelSize;
        public int DefaultLabelSize => _defaultLabelSize;
        
        [Header("Sorting Orders")]
        [SerializeField] private int _gridSortingOrder;
        public int GridSortingOrder => _gridSortingOrder;
        
        [SerializeField] private int _defaultSortingOrder;
        public int DefaultSortingOrder => _defaultSortingOrder;
        [Header("Constructions")]
        [Header("Angle")]
        [SerializeField] private float _angleWidth;
        public float AngleWidth => _angleWidth;   
        
        [SerializeField] private float _angleRadius;
        public float AngleRadius => _angleRadius;

        [SerializeField] private int _defaultTextSize;
        public int DefaultTextSize => _defaultTextSize;
        
        [SerializeField] private float _offsetTextFromAngleMultiplier;
        public float OffsetTextFromAngleMultiplier => _offsetTextFromAngleMultiplier;
        
        [Header("Height")]
        [SerializeField] private float _heightLineWidth;
        public float HeightLineWidth => _heightLineWidth;
        
        [SerializeField] private float _heightLineDashHeight;
        public float HeightLineDashHeight => _heightLineDashHeight;
        
        
        [Header("Tween")]
        [SerializeField] private TweenSettings _gridPointTween;
        public TweenSettings GridPointTween => _gridPointTween;
    }
}