using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

[CustomEditor(typeof(LessonSequence))]
public class LessonSequenceEditor : Editor {
    private Type[] actionTypes;
    private int selectedIndex;

    private void OnEnable() {
        actionTypes = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(type => !type.IsAbstract && typeof(LessonAction).IsAssignableFrom(type))
            .ToArray();
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Lesson Actions", EditorStyles.boldLabel);

        string[] names = actionTypes.Select(type => type.Name).ToArray();
        selectedIndex = EditorGUILayout.Popup("Action Type", selectedIndex, names);

        if (GUILayout.Button("Add Action")) {
            AddAction(actionTypes[selectedIndex]);
        }
    }

    private void AddAction(Type actionType) {
        var sequenceObject = (LessonSequence) target;
        
        GameObject gameObject = new GameObject(actionType.Name);
        gameObject.transform.SetParent(sequenceObject.transform);
        gameObject.AddComponent(actionType);
        
        Undo.RegisterCreatedObjectUndo(gameObject, "Add Lesson Action");
    }
}