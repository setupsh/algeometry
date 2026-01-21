using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExiter : MonoBehaviour {
    [SerializeField] private SceneInfo _mainMenuScene;
    public void Show() {
        gameObject.SetActive(true);
    }

    public void Exit() {
        if (SceneManager.GetActiveScene().buildIndex == _mainMenuScene.BuildIndex) {
            Application.Quit();
        }
        else {
            SceneLoader.LoadScene(_mainMenuScene.name);
            gameObject.SetActive(false);
        }
    }
    
    public void Hide() {
        gameObject.SetActive(false);
    }
}
