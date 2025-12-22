using UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Geometry {
    public class Board : MonoBehaviour {
        [SerializeField] private Grid _grid;
        [SerializeField] private FigureSpawner _figureSpawner;
        [SerializeField] private GameObject _board;
        [SerializeField] private FreeGeometryPoint _freeGeometryPointPrefab;
        [SerializeField] private StaticGeometryPoint _staticGeometryPointPrefab;
        public static Board Instance { get; private set; }
        public FreeGeometryPoint FreeGeometryPointPrefab => _freeGeometryPointPrefab;
        public StaticGeometryPoint StaticGeometryPointPrefab => _staticGeometryPointPrefab;
        public Grid Grid => _grid;
        public FigureSpawner FigureSpawner => _figureSpawner;
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
        }

        public void FreeCaption(string caption) {
            captions[caption] = false;
        }

        public T Instantiate<T>() where T : MonoBehaviour {
            T instance = new GameObject(typeof(T).Name).AddComponent<T>();
            return instance;
        }

        public List<T> Collect<T>() where T : MonoBehaviour {
            List<T> result = new List<T>();
            foreach (T child in gameObject.GetComponentsInChildren<T>()) {
                result.Add(child);
            }
            return result;
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