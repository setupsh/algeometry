using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {
    [SerializeField] private SpriteRenderer _sprite;
    void Update() {
        _sprite.color = new Color(1, 1, 1,Mathf.Clamp(Mathf.Abs(Mathf.Sin(Time.time)), 0.5f, 1f));
    }
}
