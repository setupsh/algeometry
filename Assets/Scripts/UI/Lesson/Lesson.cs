using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lesson : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _lessonName;
    [SerializeField] private TextMeshProUGUI _lessonDescription;
    [SerializeField] private Image _lessonPreview;
    private string sceneName;
    
    public void Init(SceneInfo lessonInfo) {
        _lessonName.SetText(lessonInfo.SceneDisplayName);
        _lessonDescription.SetText(lessonInfo.SceneDescription);
        _lessonPreview.sprite = lessonInfo.ScenePreview;
        sceneName = lessonInfo.SceneName;
    }

    public void LoadIntoScene() {
        SceneLoader.LoadScene(sceneName);    
    }
}
