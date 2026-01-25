using UnityEngine;

public class LessonsGenerator : MonoBehaviour {
    [SerializeField] private Transform _lessonsParent;
    [SerializeField] private Lesson _lessonPrefab;
    [SerializeField] private SceneInfo[] _lessonsInfos;

    private void Awake() {
        foreach (SceneInfo lessonInfo in _lessonsInfos) {
            Instantiate<Lesson>(_lessonPrefab, _lessonsParent.transform).Init(lessonInfo);
        }
    }
}