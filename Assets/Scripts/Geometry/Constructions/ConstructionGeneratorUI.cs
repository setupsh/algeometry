using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ConstructionGeneratorUI : MonoBehaviour {
    [SerializeField] private ConstructionGeneratorWindow _windowPrefab;
    private ConstructionGeneratorWindow currentWindow;
    
    public void GenerateWindow(ConstructionTemplate template) {
        if (currentWindow) {
            Destroy(currentWindow.gameObject);
            return;
        }
        currentWindow = Instantiate(_windowPrefab, transform);
        currentWindow.SetConstructionTemplate(template);
        foreach (GeometryParameter parameter in template.parameters) {
            currentWindow.Add(parameter.InstantiateUI());
        }
    }
}
