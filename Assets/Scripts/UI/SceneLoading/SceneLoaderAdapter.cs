using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderAdapter : MonoBehaviour {
    [SerializeField] private SceneInfo _scene;

    public void Load() {
        SceneLoader.LoadScene(_scene.BuildIndex);
    }
}
