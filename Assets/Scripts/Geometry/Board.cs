using UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Geometry {
    public class Board : MonoBehaviour {
        [SerializeField] private Grid _grid;
        [SerializeField] private GameObject _board;
        [SerializeField] private FreeGeometryPoint _freeGeometryPointPrefab;
        [SerializeField] private GridGeometryPoint _gridGeometryPointPrefab;
        public static Board Instance { get; private set; }
        public FreeGeometryPoint FreeGeometryPointPrefab => _freeGeometryPointPrefab;
        public GridGeometryPoint GridGeometryPointPrefab => _gridGeometryPointPrefab;
        public Grid Grid => _grid;
        public static event System.Action OnUpdate;
        private Dictionary<string, bool> captions =  new Dictionary<string, bool>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                InitializeCaptions();
            }
            else {
                Destroy(this);
            }
        }

        public void InvokeUpdate() {
            OnUpdate?.Invoke();
        }

        private void InitializeCaptions() {
            foreach (char caption in Utilities.Captions) {
                captions[caption.ToString()] = true;
            }
        }

        public string GetFreeCaption() {
            foreach (char caption in Utilities.Captions) {
                if (captions[caption.ToString()]) {
                    captions[caption.ToString()] = false;
                    return caption.ToString();
                }
            }
            throw new System.Exception("No captions found");
            return "";
        }

        public void FreeCaption(string caption) {
            captions[caption] = false;
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