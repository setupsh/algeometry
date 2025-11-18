using UI;
using UnityEngine;
using System.Collections.Generic;
public class BoardMenuContainer : MonoBehaviour {
    [SerializeField] private BoardMenu _boardMenuPrefab;
    private BoardMenu boardMenu = null;
    public void InvokeMenu(List<IIndicable> indicables, IndicatorsList indicatorsList) {
        if (boardMenu != null) {
            Destroy(boardMenu.gameObject);
            boardMenu = null;
        }
        boardMenu = Instantiate(_boardMenuPrefab, transform);
        boardMenu.transform.position = InputListener.MousePosition;
        boardMenu.Setup(indicables, indicatorsList);
    }

    public void Destroy() {
        Destroy(boardMenu.gameObject);
    }
}
