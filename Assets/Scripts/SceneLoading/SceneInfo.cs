using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Scenes/Scene Info")]
public class SceneInfo : ScriptableObject {
    public string SceneName { get; private set; }
    public int BuildIndex { get; private set; }
    public string ScenePath { get; private set; }
    public string SceneDescription;
    public Image ScenePreview;
#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneAsset;

    private void OnValidate()
    {
        if (sceneAsset == null)
            return;

        ScenePath = AssetDatabase.GetAssetPath(sceneAsset);
        SceneName = sceneAsset.name;
        BuildIndex = SceneUtility.GetBuildIndexByScenePath(ScenePath);
    }
#endif
}