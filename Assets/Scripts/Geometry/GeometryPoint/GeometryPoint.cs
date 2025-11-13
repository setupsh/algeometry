using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Geometry {
    public abstract class GeometryPoint : MonoBehaviour {
        [SerializeField] protected GeometryPointCollider _collider;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        protected List<Rule> Rules = new List<Rule>();
        protected List<Link> Links = new List<Link>();
        
        public Vector2 Position => GetPosition();
        public event System.Action OnPositionChanged;

        private void OnEnable() {
            _collider.OnDrag += OnDrag;
            _collider.OnDragEnd += OnDragEnd;
        }
        private void OnDisable() {
            _collider.OnDrag -= OnDrag;
            _collider.OnDragEnd -= OnDragEnd;
        }
        protected abstract void OnDrag();
        protected abstract void OnDragEnd();
        
        protected abstract Vector2 GetPosition();
        protected void InvokePositionChanging() {
            OnPositionChanged?.Invoke();
        }

        public abstract void Move(Vector2 position);

        public void AddRule(Rule rule) {
            Rules.Add(rule);
        }

        public void AddLink(Link link) {
            Links.Add(link);
        }

        public bool MatchRules(Vector2 position) {
            foreach (var rule in Rules.OrderBy(rule => rule.Priority)) {
                if (!rule.Match(position)) {
                    return false;
                }
            }
            return true;
        }

        protected void MoveLinked(Vector2 delta) {
            foreach (var link in Links) {
                link.Move(delta);
            }
        }
    }
}