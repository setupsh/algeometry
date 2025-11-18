using UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Geometry {
    public class Board : MonoBehaviour {
        [SerializeField] private GameObject _board;
        public static event System.Action OnUpdate;
        public static Board Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                foreach (GeometryPoint point in _board.GetComponentsInChildren<GeometryPoint>()) {
                    point.OnPositionChanged += () => OnUpdate?.Invoke();
                }
            }
            else {
                Destroy(this);
            }
        }

        public List<IIndicable> CollectIndicables() {
            List<IIndicable> except =  new List<IIndicable>();
            List<IIndicable> result = new List<IIndicable>();
            foreach (IIndicable indicable in gameObject.GetComponentsInChildren<IIndicable>(false)) {
                Debug.Log(indicable);
                if (indicable.GetChildrenIndicators() != null) {
                    except.AddRange(indicable.GetChildrenIndicators());
                }
                if (except.Contains(indicable)) {
                    continue;
                }
                result.Add(indicable);
            }
            return result;
        }
    }
}