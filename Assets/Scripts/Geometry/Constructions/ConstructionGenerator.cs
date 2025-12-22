using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ConstructionGenerator : MonoBehaviour {
    [SerializeField] private ParametersWindow _windowPrefab;
    private ParametersWindow currentWindow = null;
    
    public void GenerateUI(ConstructionTemplate template) {
        if (currentWindow != null) {
            Destroy(currentWindow);
            return;
        }
        currentWindow = Instantiate(_windowPrefab, transform);
        currentWindow.SetConstructionTemplate(template);
        foreach (GeometryParameter parameter in template.parameters) {
            currentWindow.Add(parameter.InstantiateUI());
        }
    }
}
