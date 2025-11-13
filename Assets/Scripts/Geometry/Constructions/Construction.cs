using UnityEngine;

namespace Geometry {
    public abstract class Construction : MonoBehaviour {
        protected Figure Parent;
        
        public void Init(Figure parent) {
            Parent = parent;
            name = $"{Parent.name} {GetType().Name}";
            CreateConstruction();
        }
        public abstract void UpdateConstruction();
        protected abstract void CreateConstruction();
    }
}