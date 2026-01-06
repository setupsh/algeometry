using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Geometry;
namespace UI {
    public class ParametersWindow : MonoBehaviour {
        [SerializeField] private GameObject _content;
        private List<GeometryParameterUI> parameters = new List<GeometryParameterUI>();
        private ConstructionTemplate constructionTemplate;
        
        public void Add(GeometryParameterUI parameter) {
            parameter.transform.SetParent(_content.transform);
            // I hate comments, but this shit is so fucking bad i am cant keep silence. When instance ui, while your canvas its try to компенсировать, this piece of shit steal a lot of my time, 1 free limit of chat gpt, thanks Gemini for helping
            // This place is grave of this ridicolus shit
            parameter.transform.localScale = _content.transform.localScale;
            parameters.Add(parameter);
        }

        public void SetConstructionTemplate(ConstructionTemplate constructionTemplate) {
            this.constructionTemplate = constructionTemplate;
        }

        public void GenerateConstruction() {
            List<IGeometryValue> parametersValues =  new List<IGeometryValue>();
            foreach (GeometryParameterUI parameter in parameters) {
                parametersValues.Add(parameter.GetValue());
            }
            constructionTemplate.generator.Generate(parametersValues);
        }
    }
}