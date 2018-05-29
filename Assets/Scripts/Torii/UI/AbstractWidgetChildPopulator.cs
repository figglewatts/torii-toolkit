using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.UI
{
    public abstract class AbstractWidgetChildPopulator
    {
        protected TUIStyleSheet _styleSheet;

        protected AbstractWidgetChildPopulator(TUIStyleSheet sheet)
        {
            _styleSheet = sheet;
        }

        public abstract TUIWidget[] CreateChildren();
    }

    public enum PopulatorPropertyType
    {
        Integer,
        Float,
        Boolean,
        String
    }
}
