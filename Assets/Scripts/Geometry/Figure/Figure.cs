using System;
using UnityEngine;
using System.Collections.Generic;
using UI;
using Object = UnityEngine.Object;

namespace Geometry {
    public abstract class Figure : MonoBehaviour, IIndicable {
        [SerializeField] protected GeometricalLineRenderer _lineRenderer;
        public event Action OnFigureChanged;
        public GeometryPoint[] Points {get; private set;}
        public List<Side> Sides { get; private set; } = new List<Side>();
        public List<Construction> Constructions { get; private set; } = new List<Construction>();
        protected Transform constructionFolder;
        protected Transform sidesFolder;
        protected Transform pointsFolder;

        private void OnEnable() {
            InitPoints();
            InitConstruction();
            foreach (GeometryPoint point in Points) {
                point.OnPositionChanged += UpdateFigure;
            }
        }

        private void OnDisable() {
            foreach (GeometryPoint point in Points) {
                Board.CaptionSystem.FreeCaption(point.Label);
                point.OnPositionChanged -= UpdateFigure;
            }
        }

        protected void Start() {
            transform.name = GetBoardMenuCaption();
            InitSides();
            InitRules();
            UpdateFigure();
            FieldCamera.Instance.ForceCameraUpdate();
        }

        private void UpdateFigure() {
            for (int i = 0; i < PointsAmount(); i++) {
                _lineRenderer.SetPosition(i, Points[i].transform.position);
            }
            
            foreach (Side side in Sides) {
                side.UpdatePosition();
                side.UpdateLabel();
            }

            foreach (Construction construction in Constructions) {
                construction.UpdateConstruction();
            }
            PostUpdate();
            OnFigureChanged?.Invoke();
        }

        private void InitConstruction() {
            constructionFolder = new GameObject("Constructions").transform;
            constructionFolder.parent = transform;
        }

        private void InitSides() {
            sidesFolder = new GameObject("Sides").transform;
            sidesFolder.SetParent(transform);
            sidesFolder.position = transform.position;
            
            for (int i = 0; i < PointsAmount(); i++) {
                Side side = new GameObject().AddComponent<Side>();
                side.Init(this, Points[i], Points[i + 1 > PointsAmount() - 1 ? 0 : i + 1 ]);
                side.transform.SetParent(sidesFolder);
                side.transform.position = side.GetMiddle();
                Sides.Add(side);
            }
        }

        private void InitPoints() {
            Points = new GeometryPoint[PointsAmount()];
            pointsFolder = new GameObject("Points").transform;
            pointsFolder.SetParent(transform);
            for (int i = 0; i < PointsAmount(); i++) {
                string caption = Board.CaptionSystem.GetFreeCaption(CaptionSystem.UpperLatinLetters);
                GeometryPoint point = Instantiate(Resources.FreeGeometryPointPrefab, pointsFolder).GetComponent<GeometryPoint>();
                point.Label = caption;
                point.name = $"Point {caption}";
                point.Position = (Vector2)transform.position + DefaultPositions()[i] * FieldCamera.Instance.CeilSize();
                Points[i] = point;
            }
        }

        public void AddConstruction(Construction construction) {
            Constructions.Add(construction);
            Debug.Log(constructionFolder);
            construction.transform.parent = constructionFolder;
        }

        protected abstract void PostUpdate();
        
        public abstract Vector2 GetCenter();

        protected abstract int PointsAmount();
        
        protected abstract void InitRules();
        
        protected abstract Vector2[] DefaultPositions();

        public string GetBoardMenuCaption() {
            string letters = string.Empty;
            foreach (GeometryPoint point in Points) {
                letters += point.Label;
            }
            return $"{this.GetType().Name} {letters}";
        }

        public List<IndicatorInfo> GetIndicatorInfos() {
            return new List<IndicatorInfo>();
            List<IndicatorInfo> indicators = new List<IndicatorInfo>();
            foreach (Side side in Sides) {
                foreach (IndicatorInfo info in side.GetIndicatorInfos()) {
                    indicators.Add(info);
                }
            }
            return indicators;
        }

        public List<IIndicable> GetChildrenIndicators() {
            List<IIndicable> result = new List<IIndicable>();
            foreach (Side side in Sides) {
                result.Add(side);
            }
            return result;
        }
    }
}