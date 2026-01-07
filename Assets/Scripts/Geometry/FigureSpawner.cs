using Geometry;
using UnityEngine;
using Geometry.Realisations;
public enum Figures {Triangle, RightTriangle, EquilateralTriangle}
public class FigureSpawner : MonoBehaviour {
    [SerializeField] private Triangle _trianglePrefab;
    public void SpawnFigure(Figures figure) {
        switch (figure) {
            case Figures.Triangle: SnapToGrid(Instantiate(_trianglePrefab, (Vector2) FieldCamera.Instance.GetCameraBounds().center, Quaternion.identity, Board.Instance.transform)); break; 
        }
    }

    private void SnapToGrid(Figure figure) {
        foreach (GeometryPoint point in figure.Points) {
            point.Move(Board.Grid.GetNearestGridPoint(point.Position));
        }
    }
}
