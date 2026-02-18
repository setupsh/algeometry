using UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Geometry {
    public class Board : MonoBehaviour {
        [SerializeField] private Grid _grid;
        [SerializeField] private FigureSpawner _figureSpawner;
        [SerializeField] private GameObject _board;
        [SerializeField] private GameObject _mainCanvas;
        [SerializeField] private LessonText _lessonText;
        [SerializeField] private LessonSummary _lessonSummary;
        [SerializeField] private IndicatorsList _indicatorsList;
        private bool isUpdating = false;
        public static Board Instance { get; private set; }

        public static Grid Grid => Instance._grid;
        public static FigureSpawner FigureSpawner => Instance._figureSpawner;
        public static GameObject MainCanvas => Instance._mainCanvas;
        public static CaptionSystem CaptionSystem { get; private set; } = new CaptionSystem();
        public static AlgebraExpressionViewGenerator AlgebraExpressionViewGenerator { get; private set; } = new AlgebraExpressionViewGenerator();
        public static LessonText LessonText => Instance._lessonText;
        public static LessonSummary LessonSummary => Instance._lessonSummary;
        public static IndicatorsList IndicatorsList => Instance._indicatorsList;
        
        public static event System.Action OnUpdate;
        

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(this);
            }
        }

        public void InvokeUpdate() {
            OnUpdate?.Invoke();
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