using UnityEngine;
[CreateAssetMenu(menuName = "Scenes/Scene Info")]
public class SceneInfo : ScriptableObject {
    public int BuildIndex;
    
    public string SceneDisplayName;
    public string SceneDescription;
    public Sprite ScenePreview;
}