using UnityEngine;
using UI;
using Geometry.Realisations;
namespace Geometry {
    public class Resources : MonoBehaviour {
        [Header("Geometry")]
        [Header("Points")]
        [SerializeField] private FreeGeometryPoint _freeGeometryPointPrefab;
        [SerializeField] private StaticGeometryPoint _staticGeometryPointPrefab;
        [Header("Figures")]
        [SerializeField] private Triangle _trianglePrefab;
        [SerializeField] private Circle _circlePrefab;
        [Header("UI")]
        [SerializeField] private Description _descriptionPrefab;
        
        public static FreeGeometryPoint FreeGeometryPointPrefab => Instance._freeGeometryPointPrefab;
        public static StaticGeometryPoint StaticGeometryPointPrefab => Instance._staticGeometryPointPrefab;
        public static Triangle TrianglePrefab => Instance._trianglePrefab;
        public static Circle CirclePrefab => Instance._circlePrefab;
        public static Description DescriptionPrefab => Instance._descriptionPrefab;
        
        public static Resources Instance { get; private set; }
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