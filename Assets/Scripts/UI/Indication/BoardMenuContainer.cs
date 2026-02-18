using UI;
using UnityEngine;
using System.Collections.Generic;

public class BoardMenuContainer : MonoBehaviour {
    [SerializeField] private BoardMenu _boardMenuPrefab;
    private BoardMenu currentMenu = null;
    
    public void GenerateMenu(List<IMenuEntry> entries, IndicatorsList getter) {
        if (currentMenu != null)
            DestroyBoardMenu();
        BoardMenu menu = Instantiate(_boardMenuPrefab, transform);
        menu.transform.position = InputListener.MousePosition;
        menu.Init(entries, getter, this);
        currentMenu = menu;
    }
    
    public void GenerateMenu(List<IIndicable> entries, IndicatorsList getter) {
        if (currentMenu != null)
            DestroyBoardMenu();
        BoardMenu menu = Instantiate(_boardMenuPrefab, transform);
        menu.transform.position = InputListener.MousePosition;
        menu.Init(ConvertToEntries(entries, getter), getter, this);
        currentMenu = menu;
    }
    
    private List<IMenuEntry> ConvertToEntries(List<IIndicable> items, IndicatorsList getter) {
        List<IMenuEntry> entries = new List<IMenuEntry>();
        foreach (var item in items) {
            List<IMenuEntry> subEntries = new List<IMenuEntry>();
            
            var infos = item.GetIndicatorInfos();
            if (infos != null) {
                foreach (var info in infos) subEntries.Add(new ActionEntry(info));
            }
            var children = item.GetChildrenIndicators();
            if (children != null) {
                subEntries.AddRange(ConvertToEntries(children, getter));
            }
            entries.Add(new SubMenuEntry(item.GetBoardMenuCaption(), subEntries));
        }
        return entries;
    }

    public void DestroyBoardMenu() {
        Destroy(currentMenu.gameObject);
    }
}        