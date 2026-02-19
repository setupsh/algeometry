using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Algebra;
using UI;

namespace Geometry {
    public class GeometryPoint : MonoBehaviour, IIndicable {
        [SerializeField] private GeometryPointCollider _collider;
        [SerializeField] private GeometryPointSprite _sprite;
        [SerializeField] private string _label;
        public List<Rule> Rules = new List<Rule>();
        public List<Link> Links = new List<Link>();
        public Constrain Constrain = null;
        private bool blocked;
        public enum Indicators {Coordinates, X, Y}

        public bool Blocked {
            get => blocked;
            set { blocked = value; _sprite.SetColorByBlocked(value); }
        }

        public Vector2 Position {
            get => transform.position;
            set => SetPosition(value);
        }
        public string Label {
            get => _label;
            set => _label = value;
        }
        public event System.Action OnPositionChanged;
        private void OnEnable() {
            _collider.OnDrag += OnDrag;
            _collider.OnDragEnd += OnDragEnd;
        }
        private void OnDisable() {
            _collider.OnDrag -= OnDrag;
            _collider.OnDragEnd -= OnDragEnd;
        }
        
        private void OnDrag() {
            if (blocked) return;
            Vector2 targetPosition = FieldCamera.Instance.ToWorldPoint(InputListener.MousePosition);

            if (InputListener.ShiftPressed) {
                targetPosition = Board.Grid.GetNearestGridPoint(targetPosition);
            }
        
            if (Constrain != null) {
                targetPosition = Constrain.GetConstrainedPosition(targetPosition);
            }

            if (MatchRules(targetPosition)) {
                Move(targetPosition);
            }
        
            _collider.transform.position = targetPosition;
        }

        private void OnDragEnd() {
            _collider.transform.position = transform.position;
        }
        
        private void SetPosition(Vector2 position) {
            if (Constrain != null) {
                position = Constrain.GetConstrainedPosition(position);
            }
            transform.position = position;
            InvokePositionChanging();
        }

        public void Move(Vector2 newPosition) {
            Vector2 delta = newPosition - (Vector2) transform.position;
            MoveLinked(delta);
            SetPosition(newPosition);
        }
        private void InvokePositionChanging() {
            OnPositionChanged?.Invoke();
            Board.Instance.InvokeUpdate();
        }

        public bool MatchRules(Vector2 position) {
            foreach (var rule in Rules.OrderBy(rule => rule.Priority)) {
                if (!rule.Match(position)) {
                    return false;
                }
            }
            return true;
        }

        private void MoveLinked(Vector2 delta) {
            foreach (var link in Links) {
                link.Move(delta);
            }
        }

        public string GetBoardMenuCaption() {
            return $"Точка {_label}";
        }

        public List<IndicatorInfo> GetIndicatorInfos() {
            return new List<IndicatorInfo>() {
                new AlgebraExpressionInfo(new VectorExpression(new NumberExpression(() => Position.x), new NumberExpression(() => Position.y)), $"Координаты точки {Label}", $"{Label}"),
                new RawNumberInfo((new NumberExpression(() => Position.x)), $"Координаты точки {Label} степень", $"{Label}(X)"),
                new RawNumberInfo((new NumberExpression(() => Position.y)), $"Координаты точки {Label} степень", $"{Label}(Y)"),
            };
        }

        public List<IIndicable> GetChildrenIndicators() {
            return null;
        }
    }
}