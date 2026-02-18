using System.Collections.Generic;

namespace UI {
    public interface IIndicable {
        public string GetBoardMenuCaption();
        List<IndicatorInfo> GetIndicatorInfos();
        List<IIndicable> GetChildrenIndicators();
    }
    public interface IMenuEntry {
        string GetTitle();
        void OnClick(BoardMenuContainer container, IndicatorsList getter);
    }
    
    public class SubMenuEntry : IMenuEntry {
        private string _title;
        private List<IMenuEntry> _subEntries;

        public SubMenuEntry(string title, List<IMenuEntry> subEntries) {
            _title = title;
            _subEntries = subEntries;
        }

        public string GetTitle() => _title + " >";

        public void OnClick(BoardMenuContainer container, IndicatorsList getter) {
            container.GenerateMenu(_subEntries, getter);
        }
    }
    
    public class ActionEntry : IMenuEntry {
        private IndicatorInfo _info;

        public ActionEntry(IndicatorInfo info) {
            _info = info;
        }

        public string GetTitle() => _info.BoardMenuCaption;

        public void OnClick(BoardMenuContainer container, IndicatorsList getter) {
            getter.AddIndicator(_info);
            container.DestroyBoardMenu();
        }
    }
}