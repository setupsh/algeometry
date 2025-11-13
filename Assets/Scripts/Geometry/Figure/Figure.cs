using System;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Geometry {
    public abstract class Figure : MonoBehaviour {
        [SerializeField] protected GeometricalLineRenderer _lineRenderer;
        [SerializeField] protected GeometryPoint[] _points;
        protected Side[] sides;
        protected List<Construction> constructions = new List<Construction>();
        protected Transform constructionFolder;
        protected Transform sidesFolder;
        private void OnValidate() {
            if (PointsAmount() != _points.Length) {
                Array.Resize(ref _points, PointsAmount());
            }
        }
        private void OnEnable() {
            foreach (GeometryPoint point in _points) {
                point.OnPositionChanged += UpdateFigure;
            }
        }

        private void OnDisable() {
            foreach (GeometryPoint point in _points) {
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
                _lineRenderer.SetPosition(i, _points[i].transform.position);
            }
            
            foreach (Side side in sides) {
                side.UpdatePosition();
                side.UpdateLabel();
            }

            foreach (Construction construction in constructions) {
                construction.UpdateConstruction();
            }
        }

        private void InitConstruction() {
            constructionFolder = new GameObject("Constructions").transform;
            constructionFolder.parent = transform;
        }

        private void InitSides() {
            sides = new Side[PointsAmount()];
            sidesFolder = new GameObject("Sides").transform;
            sidesFolder.SetParent(transform);
            sidesFolder.position = transform.position;
            
            for (int i = 0; i < PointsAmount(); i++) {
                Side side = new GameObject().AddComponent<Side>();
                side.Init(this, _points[i], _points[i + 1 > PointsAmount() - 1 ? 0 : i + 1 ]);
                side.transform.SetParent(sidesFolder);
                side.transform.position = side.GetMiddle();
                sides[i] = side;
            }
        }

        public void AddConstruction(Construction construction) {
            constructions.Add(construction);
            construction.transform.parent = constructionFolder;
        }
        
        public abstract Vector2 GetCenter();

        protected abstract int PointsAmount();
        
        protected abstract void InitRules();

    }
}