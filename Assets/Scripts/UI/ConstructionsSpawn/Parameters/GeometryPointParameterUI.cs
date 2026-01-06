using UnityEngine;
using UnityEngine.UI;
using Geometry;
using TMPro;
using System.Collections.Generic;
namespace UI {
    public class GeometryPointParameterUI : GeometryParameterUI {
        [SerializeField] private TextMeshProUGUI _caption;
        [SerializeField] private TMP_Dropdown _dropdown;
        private Dictionary<string, GeometryPoint> options = new  Dictionary<string, GeometryPoint>();
        private GeometryPoint point;
        
        public override void SetCaption(string caption) {
            _caption.text = caption;
        }
        
        public void SetPointByDropdown() {
            point = options[_dropdown.options[_dropdown.value].text];
        }

        private void Start() {
            foreach (GeometryPoint point in Board.Instance.Collect<GeometryPoint>()) {
                options[point.Label] = point;
                _dropdown.options.Add(new TMP_Dropdown.OptionData(point.Label));
            }
        }

        public override IGeometryValue GetValue() {
            return new GeometryPointValue(point);
        }
    }
}
public class GeometryPointValue : IGeometryValue<GeometryPoint> {
    public GeometryPoint Value { get; }
    public GeometryPointValue(GeometryPoint value) {
        Value = value;
    }
}