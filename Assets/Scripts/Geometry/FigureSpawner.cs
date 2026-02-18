using Geometry;
using UnityEngine;
using Geometry.Realisations;
using System.Collections;
using System.Collections.Generic;
using Resources = Geometry.Resources;

public enum Figures {Triangle, IsoscelesTriangle, RectangularTriangle, Circle}
public class FigureSpawner : MonoBehaviour {
    public Dictionary<Figures, Figure> Prefabs { get; private set; } = new Dictionary<Figures, Figure>();

    private void Awake() {
        Prefabs = new Dictionary<Figures, Figure>() {
            { Figures.Triangle, Resources.TrianglePrefab},
            { Figures.Circle, Resources.CirclePrefab},
            { Figures.IsoscelesTriangle, Resources.IsoscelesTrianglePrefab},
            { Figures.RectangularTriangle, Resources.RectangularTrianglePrefab}
        };
    }
    public Figure SpawnFigure(Figures figure) {
        Figure instance = Instantiate(Prefabs[figure], (Vector2) FieldCamera.Instance.GetCameraBounds().center, Quaternion.identity, Board.Instance.transform);
        SnapToGrid(instance);
        return instance;
    }

    public Figure SpawnFigure(Figures figure, Vector2 center) {
        Figure instance = Instantiate(Prefabs[figure], center, Quaternion.identity, Board.Instance.transform);
        SnapToGrid(instance);
        return instance;
    }

    private void SnapToGrid(Figure figure) {
        foreach (GeometryPoint point in figure.Points) {
            point.Position = Board.Grid.GetNearestGridPoint(point.Position);
        }
    }
}
