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
        public static float DefaultLineWidth => Instance._defaultLineWidth;
        
        [Header("Text")]
        [SerializeField] private float _defaultLabelSize;
        public static float DefaultLabelSize => Instance._defaultLabelSize;
        
        [Header("Sorting Orders")]
        [SerializeField] private int _gridSortingOrder;
        public static int GridSortingOrder => Instance._gridSortingOrder;
        
        [SerializeField] private int _defaultSortingOrder;
        public static int DefaultSortingOrder => Instance._defaultSortingOrder;
        
        [SerializeField] private int _labelSortingOrder;

        public static int LabelSortingOrder => Instance._labelSortingOrder;
        [Header("Constructions")]
        [Header("Angle")]
        [SerializeField] private float _angleWidth;
        public static float AngleWidth => Instance._angleWidth;   
        
        [SerializeField] private float _angleRadius;
        public static float AngleRadius => Instance._angleRadius;
        
        [SerializeField] private float _offsetTextFromAngleMultiplier;
        public static float OffsetTextFromAngleMultiplier => Instance._offsetTextFromAngleMultiplier;
        
        [Header("Height")]
        [SerializeField] private float _heightLineWidth;
        public static float HeightLineWidth => Instance._heightLineWidth;
        
        [SerializeField] private float _heightLineDashHeight;
        public static float HeightLineDashHeight => Instance._heightLineDashHeight;
    }
}