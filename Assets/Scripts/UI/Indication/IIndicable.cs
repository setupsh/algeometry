using System.Collections.Generic;

namespace UI {
    public interface IIndicable
    {
        public string GetBoardMenuCaption();
        List<IndicatorInfo> GetIndicatorInfos();
        List<IIndicable> GetChildrenIndicators();
    }
}