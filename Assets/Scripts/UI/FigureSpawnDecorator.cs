using Geometry;
using UnityEngine;

namespace UI {
    public class FigureSpawnAdapter : MonoBehaviour {
        [SerializeField] private Figures _figures;
        public void SpawnFigure() {
            Board.Instance.FigureSpawner.SpawnFigure(_figures);
        }
    }
}