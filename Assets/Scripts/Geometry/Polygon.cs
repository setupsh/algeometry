using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PolygonBuilder : MonoBehaviour {

    public void BuildPolygon(List<Vector2> points)
    {
        if (points.Count < 3)
        {
            Debug.LogError("Нужно минимум 3 точки!");
            return;
        }

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        Vector3[] vertices = new Vector3[points.Count];
        for (int i = 0; i < points.Count; i++)
            vertices[i] = new Vector3(points[i].x, points[i].y, 0);

        int[] triangles = Triangulate(points);
        
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
            uvs[i] = new Vector2(vertices[i].x, vertices[i].y);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
    
    private int[] Triangulate(List<Vector2> pts)
    {
        List<int> indices = new List<int>();
        List<int> remaining = new List<int>();

        for (int i = 0; i < pts.Count; i++)
            remaining.Add(i);

        int maxIterations = pts.Count * pts.Count;
        int iteration = 0;

        while (remaining.Count > 3 && iteration < maxIterations)
        {
            iteration++;
            bool earFound = false;

            for (int i = 0; i < remaining.Count; i++)
            {
                int prev = remaining[(i - 1 + remaining.Count) % remaining.Count];
                int curr = remaining[i];
                int next = remaining[(i + 1) % remaining.Count];

                if (IsEar(pts, prev, curr, next, remaining))
                {
                    indices.Add(prev);
                    indices.Add(curr);
                    indices.Add(next);
                    remaining.RemoveAt(i);
                    earFound = true;
                    break;
                }
            }

            if (!earFound) break;
        }
        
        if (remaining.Count == 3)
        {
            indices.Add(remaining[0]);
            indices.Add(remaining[1]);
            indices.Add(remaining[2]);
        }

        return indices.ToArray();
    }

    private bool IsEar(List<Vector2> pts, int prev, int curr, int next, List<int> remaining) {
        Vector2 a = pts[prev];
        Vector2 b = pts[curr];
        Vector2 c = pts[next];
        
        if (Cross(a, b, c) <= 0) return false;
        
        foreach (int idx in remaining)
        {
            if (idx == prev || idx == curr || idx == next) continue;
            if (IsPointInTriangle(pts[idx], a, b, c)) return false;
        }

        return true;
    }

    private float Cross(Vector2 a, Vector2 b, Vector2 c) {
        return (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);
    }

    private bool IsPointInTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c) {
        float d1 = Cross(p, a, b);
        float d2 = Cross(p, b, c);
        float d3 = Cross(p, c, a);

        bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(hasNeg && hasPos);
    }
}