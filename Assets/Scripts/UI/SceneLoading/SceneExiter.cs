using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExiter : MonoBehaviour {
    [SerializeField] private SceneInfo _mainMenuScene;
    [SerializeField] private GameObject _sceneExiter;

    private void Awake() {
        InputListener.Escape.performed += (ctx) => Show();
    }
    
    private void Show() {
        if (!_sceneExiter.activeInHierarchy) {
            _sceneExiter.SetActive(true);
        }
        else {
            Hide();
        }
    }

    public void Exit() {
        if (SceneManager.GetActiveScene().buildIndex == _mainMenuScene.BuildIndex) {
            Application.Quit();
        }
        else {
            SceneLoader.LoadScene(_mainMenuScene.name);
            _sceneExiter.SetActive(false);
        }
    }
    
    public void Hide() {
        _sceneExiter.SetActive(false);
    }
}
