using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using PicoTween;
using Unity.VisualScripting;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
    [SerializeField] private bool _fade;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private Image _overlayImage;

    private static SceneLoader instance;

    private void Awake() {
        if (instance) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public static void LoadScene(int buildIndex) {
        var sceneLoadingOperation = SceneManager.LoadSceneAsync(buildIndex);
        instance.StartCoroutine(instance.Loading(sceneLoadingOperation));
    }
    public static void LoadScene(string sceneName) {
        var sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.StartCoroutine(instance.Loading(sceneLoadingOperation));
    }

    private IEnumerator Loading(AsyncOperation loadingOperation) {
        InputListener.InputActions.Disable();
        loadingOperation.allowSceneActivation = false;
        _overlayImage.Tween(new Color(0,0,0,1), _fadeDuration, null,() => {
            loadingOperation.allowSceneActivation = true;
        });
        while (!loadingOperation.isDone) {
            yield return null;
        }
        yield return new WaitUntil(() => loadingOperation.allowSceneActivation);
        _overlayImage.Tween(new Color(0, 0, 0, 0), _fadeDuration, null, null);
        InputListener.InputActions.Enable();
    }
}