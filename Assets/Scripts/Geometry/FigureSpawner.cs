using Geometry;
using UnityEngine;
using Geometry.Realisations;
using System.Collections;
using System.Collections.Generic;
using Resources = Geometry.Resources;

public enum Figures {Triangle, RightTriangle, EquilateralTriangle, Circle}
public class FigureSpawner : MonoBehaviour {
    public Dictionary<Figures, Figure> Prefabs { get; private set; } = new Dictionary<Figures, Figure>();

    private void Awake() {
        Prefabs = new Dictionary<Figures, Figure>() {
            { Figures.Triangle, Resources.TrianglePrefab},
            { Figures.Circle, Resources.CirclePrefab},
        };
    }
    public void SpawnFigure(Figures figure) {
        Figure instance = Instantiate(Prefabs[figure], (Vector2) FieldCamera.Instance.GetCameraBounds().center, Quaternion.identity, Board.Instance.transform);
        SnapToGrid(instance);
    }

    private void SnapToGrid(Figure figure) {
        foreach (GeometryPoint point in figure.Points) {
            point.Move(Board.Grid.GetNearestGridPoint(point.Position));
        }
    }
}
