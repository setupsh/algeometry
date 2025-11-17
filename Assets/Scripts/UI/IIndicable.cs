using System.Collections.Generic;

namespace UI {
    public interface IIndicable
    {
        public string GetCaption();
        List<IndicatorInfo> GetIndicatorInfos();
        List<IIndicable> GetChildrenIndicators();
    }
}