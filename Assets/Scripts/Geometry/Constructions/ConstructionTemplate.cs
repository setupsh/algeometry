using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Geometry;
using TMPro;
using Color = UnityEngine.Color;

public abstract class ConstructionTemplate : ScriptableObject {
    public List<GeometryParameter> parameters;
    public abstract Construction Generate(List<IGeometryValue> arguments);

    protected T Get<T>(List<IGeometryValue> arguments, int index) {
        if (arguments[index] is IGeometryValue<T> argument) {
            return argument.Value;
        }
        throw new System.Exception($"YOU ARE MOTHERFUCKING PIECE OF SHIT, FIX CRAP IN YOUR TEMPLATES {this.name} {index} {arguments[index].GetType()}");
    }
}

[CreateAssetMenu(fileName = "Angle Construction", menuName = "Geometry/Constructions/Construction Template/Angle Construction")]
public class AngleConstructionTemplate : ConstructionTemplate {
    public override Construction Generate(List<IGeometryValue> arguments) {
        Angle construction = new GameObject("Angle").AddComponent<Angle>();
        construction.Init(
            Get<Figure>(arguments, 0),
            Get<GeometryPoint>(arguments, 1),
            Get<GeometryPoint>(arguments, 2),
            Get<GeometryPoint>(arguments, 3),
            Get<Color>(arguments, 4),
            Get<bool>(arguments, 5));
        return construction;
    }
}
[System.Serializable]
public class GeometryParameter {
    public GeometryParameterUI _parameterUIprefab;
    public string _parameterCaption;
    
    public GeometryParameterUI InstantiateUI() {
        GeometryParameterUI parameterUI = GameObject.Instantiate(_parameterUIprefab);
        parameterUI.SetCaption(_parameterCaption);
        return parameterUI;
    }
}

public abstract class GeometryParameterUI : MonoBehaviour {
    public abstract void SetCaption(string caption);
    public abstract IGeometryValue GetValue();
}

public class GeometryPointParameterUI : GeometryParameterUI {
    [SerializeField] private TextMeshProUGUI _caption;
    public GeometryPoint point;
    public override void SetCaption(string caption) {
        _caption.text = caption;
    }

    public override IGeometryValue GetValue() {
        return new GeometryPointValue(point);
    }
}

public interface IGeometryValue {}
public interface IGeometryValue<T> : IGeometryValue {
    public T Value { get; }
}

public class GeometryPointValue : IGeometryValue<GeometryPoint> {
    public GeometryPoint Value { get; }
    public GeometryPointValue(GeometryPoint value) {
        Value = value;
    }
}

public class FigureValue : IGeometryValue<Figure> {
    public Figure Value { get; }

    public FigureValue(Figure value) {
        Value = value;
    }
}

public class ColorValue : IGeometryValue<Color> {
    public Color Value { get; }

    public ColorValue(Color value) {
        Value = value;
    }
}

public class BoolValue : IGeometryValue<bool> {
    public bool Value { get; }

    public BoolValue(bool value) {
        Value = value;
    }
}







