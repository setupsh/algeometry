using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Scenes/Scene Info")]
public class SceneInfo : ScriptableObject {
    public int BuildIndex;
    
    public string SceneDisplayName;
    public string SceneDescription;
    public Sprite ScenePreview;
}