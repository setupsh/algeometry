using System;
using UnityEngine;
using System.Collections.Generic;
using UI;
using Object = UnityEngine.Object;

namespace Geometry {
    public abstract class Figure : MonoBehaviour, IIndicable {
        [SerializeField] protected GeometricalLineRenderer _lineRenderer;
        public GeometryPoint[] Points {get; private set;}
        public Side[] Sides { get; private set;}
        public List<Construction> Constructions { get; private set; } = new List<Construction>();
        protected Transform constructionFolder;
        protected Transform sidesFolder;
        protected Transform pointsFolder;

        private void OnEnable() {
            InitPoints();
            foreach (GeometryPoint point in Points) {
                point.OnPositionChanged += UpdateFigure;
            }
        }

        private void OnDisable() {
            foreach (GeometryPoint point in Points) {
                Board.Instance.FreeCaption(point.Label);
                point.OnPositionChanged -= UpdateFigure;
            }
        }

        protected void Start() {
            InitConstruction();
            InitSides();
            InitRules();
            UpdateFigure();
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
        }

        private void InitConstruction() {
            constructionFolder = new GameObject("Constructions").transform;
            constructionFolder.parent = transform;
        }

        private void InitSides() {
            Sides = new Side[PointsAmount()];
            sidesFolder = new GameObject("Sides").transform;
            sidesFolder.SetParent(transform);
            sidesFolder.position = transform.position;
            
            for (int i = 0; i < PointsAmount(); i++) {
                Side side = new GameObject().AddComponent<Side>();
                side.Init(this, Points[i], Points[i + 1 > PointsAmount() - 1 ? 0 : i + 1 ]);
                side.transform.SetParent(sidesFolder);
                side.transform.position = side.GetMiddle();
                Sides[i] = side;
            }
        }

        private void InitPoints() {
            Points = new GeometryPoint[PointsAmount()];
            pointsFolder = new GameObject("Points").transform;
            pointsFolder.SetParent(transform);
            for (int i = 0; i < PointsAmount(); i++) {
                string caption = Board.Instance.GetFreeCaption();
                GeometryPoint point = Instantiate(Board.Instance.FreeGeometryPointPrefab, pointsFolder).GetComponent<GeometryPoint>();
                point.Label = caption;
                point.Move((Vector2)transform.position + DefaultPositions()[i]);
                Points[i] = point;
            }
        }

        public void AddConstruction(Construction construction) {
            Constructions.Add(construction);
            construction.transform.parent = constructionFolder;
        }
        
        public abstract Vector2 GetCenter();

        protected abstract int PointsAmount();
        
        protected abstract void InitRules();
        
        protected abstract Vector2[] DefaultPositions();

        public string GetCaption() {
            return $"{this.GetType().Name} {Points[0].Label}{Points[1].Label}{Points[2].Label}";
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