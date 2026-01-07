using UnityEngine;

namespace UI {
    public class BoolParameterUI : GeometryParameterUI {
        public override void SetCaption(string caption) {
            throw new System.NotImplementedException();
        }

        public override IGeometryValue GetValue() {
            throw new System.NotImplementedException();
        }
    }
    
    public class BoolValue : IGeometryValue<bool> {
        public bool Value { get; }

        public BoolValue(bool value) {
            Value = value;
        }
    }


}