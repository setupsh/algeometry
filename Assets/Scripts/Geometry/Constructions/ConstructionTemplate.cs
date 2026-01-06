using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Geometry;
using TMPro;
using Color = UnityEngine.Color;

[CreateAssetMenu(fileName = "Construction Template", menuName = "Construction Template")]
public class ConstructionTemplate : ScriptableObject {
    [SerializeReference, SubclassSelector] public ConstructionGenerator generator = null;
    public List<GeometryParameter> parameters;
}

public abstract class ConstructionGenerator {
    protected T Get<T>(List<IGeometryValue> arguments, int index) {
        if (arguments[index] is IGeometryValue<T> argument) {
            return argument.Value;
        }
        throw new System.Exception($"YOU ARE MOTHERFUCKING PIECE OF SHIT, FIX CRAP IN YOUR TEMPLATES {index} {arguments[index].GetType()}");
    }
    public abstract Construction Generate(List<IGeometryValue> arguments);
}

[System.Serializable]
public class GeometryParameter {
    public GeometryParameterUI parameterUIprefab;
    public string parameterCaption;
    
    public GeometryParameterUI InstantiateUI() {
        GeometryParameterUI parameterUI = Object.Instantiate(parameterUIprefab);
        parameterUI.SetCaption(parameterCaption);
        return parameterUI;
    }
}

public abstract class GeometryParameterUI : MonoBehaviour {
    public abstract void SetCaption(string caption);
    public abstract IGeometryValue GetValue();
}

public interface IGeometryValue {}
public interface IGeometryValue<T> : IGeometryValue {
    public T Value { get; }
}



public class BoolValue : IGeometryValue<bool> {
    public bool Value { get; }

    public BoolValue(bool value) {
        Value = value;
    }
}







