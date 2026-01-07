using UnityEngine;
using UI;
namespace Geometry {
    public class Resources : MonoBehaviour {
        [SerializeField] private FreeGeometryPoint _freeGeometryPointPrefab;
        [SerializeField] private StaticGeometryPoint _staticGeometryPointPrefab;
        [SerializeField] private Description _descriptionPrefab;
        
        public static FreeGeometryPoint FreeGeometryPointPrefab => Instance._freeGeometryPointPrefab;
        public static StaticGeometryPoint StaticGeometryPointPrefab => Instance._staticGeometryPointPrefab;
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