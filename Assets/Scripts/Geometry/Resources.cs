using Algebra;
using UnityEngine;
using UI;
using Geometry.Realisations;
using TMPro;

namespace Geometry {
    public class Resources : MonoBehaviour {
        [Header("Geometry")]
        
        [Header("Points")]
        [SerializeField] private GeometryPoint _geometryPointPrefab;
        
        [Header("Figures")]
        [SerializeField] private Triangle _trianglePrefab;
        [SerializeField] private Circle _circlePrefab;
        [SerializeField] private IsoscelesTriangle _isoscelesTrianglePrefab;
        
        [Header("UI")]
        [SerializeField] private Description _descriptionPrefab;
        [SerializeField] private TextMeshProUGUI _numberExpressionPrefab;
        [SerializeField] private AlgebraExpressionUI _vectorExpressionPrefab;
        [SerializeField] private AlgebraExpressionUI _fractionExpressionPrefab;
        [SerializeField] private AlgebraExpressionUI _sumExpressionPrefab;
        [SerializeField] private AlgebraExpressionUI _subtractExpressionPrefab;
        [SerializeField] private AlgebraExpressionUI _sqrtExpressionPrefab;
        [SerializeField] private AlgebraExpressionUI _mulExpressionPrefab;
        
        public static GeometryPoint FreeGeometryPointPrefab => Instance._geometryPointPrefab;
        
        public static Triangle TrianglePrefab => Instance._trianglePrefab;
        
        public static Circle CirclePrefab => Instance._circlePrefab;
        
        public static IsoscelesTriangle IsoscelesTrianglePrefab => Instance._isoscelesTrianglePrefab; 
        
        public static Description DescriptionPrefab => Instance._descriptionPrefab;
        
        public static TextMeshProUGUI NumberExpressionPrefab => Instance._numberExpressionPrefab;

        public static AlgebraExpressionUI VectorExpressionPrefab => Instance._vectorExpressionPrefab;
        
        public static AlgebraExpressionUI FractionExpressionPrefab => Instance._fractionExpressionPrefab;
        
        public static AlgebraExpressionUI SumExpressionPrefab => Instance._sumExpressionPrefab;
        
        public static AlgebraExpressionUI SubtractExpressionPrefab => Instance._subtractExpressionPrefab;
        
        public static AlgebraExpressionUI SqrtExpressionPrefab => Instance._sqrtExpressionPrefab;
        
        public static AlgebraExpressionUI MulExpressionPrefab => Instance._mulExpressionPrefab;
        
        private static Resources Instance { get; set; }
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(this);
            }
        }
    }
}