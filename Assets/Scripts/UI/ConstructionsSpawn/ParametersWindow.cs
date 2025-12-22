using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Geometry;
namespace UI {
    public class ParametersWindow : MonoBehaviour {
        [SerializeField] private GameObject _content;
        private ConstructionTemplate constructionTemplate;
        
        public void Add(GeometryParameterUI parameter) {
            parameter.transform.SetParent(_content.transform);
        }

        public void SetConstructionTemplate(ConstructionTemplate constructionTemplate) {
            this.constructionTemplate = constructionTemplate;
        }

        public void GenerateConstruction() {
            List<IGeometryValue> parametersValues =  new List<IGeometryValue>();
            foreach (GeometryParameterUI parameter in _content.GetComponentsInChildren<GeometryParameterUI>()) {
                parametersValues.Add(parameter.GetValue());
            }
            constructionTemplate.Generate(parametersValues);
        }
    }
}