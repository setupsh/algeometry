using UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Geometry {
    public class Board : MonoBehaviour {
        [SerializeField] private GameObject _board;
        public static Board Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(this);
            }
        }

        public List<IIndicable> CollectIndicables() {
            return gameObject.GetComponentsInChildren<IIndicable>(false).ToList();
        }
    }
}