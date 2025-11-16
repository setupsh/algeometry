using System.Collections.Generic;

namespace UI {
    public interface IIndicable {
        public abstract IndicatorInfo GetIndicatorInfo(); 
    }

    public interface IIndicables {
        public abstract List<IndicatorInfo> GetIndicatorsInfo();
    }
}