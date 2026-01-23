using System.Collections;
using System.Collections.Generic;
using Geometry;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI {
    public class Description : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string text) {
            _text.text = text;
        }
    }
}
