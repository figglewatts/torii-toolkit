using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace Torii.UI
{
    public class LayoutElementData
    {
        public float FlexibleHeight;
        public float FlexibleWidth;
        public bool IgnoreLayout;
        public int LayoutPriority;
        public float MinHeight;
        public float MinWidth;
        public float PreferredHeight;
        public float PreferredWidth;

        public LayoutElementData(bool ignoreLayout = false, float minWidth = -1, float minHeight = -1,
            float preferredWidth = -1, float preferredHeight = -1, float flexibleWidth = -1, float flexibleHeight = -1,
            int layoutPriority = 1)
        {
            IgnoreLayout = ignoreLayout;
            MinWidth = minWidth;
            MinHeight = minHeight;
            PreferredWidth = preferredWidth;
            PreferredHeight = preferredHeight;
            FlexibleWidth = flexibleWidth;
            FlexibleHeight = flexibleHeight;
            LayoutPriority = layoutPriority;
        }

        public void Set(ref LayoutElement element)
        {
            element.flexibleHeight = FlexibleHeight;
            element.flexibleWidth = FlexibleWidth;
            element.ignoreLayout = IgnoreLayout;
            element.layoutPriority = LayoutPriority;
            element.minHeight = MinHeight;
            element.minWidth = MinWidth;
            element.preferredHeight = PreferredHeight;
            element.preferredWidth = PreferredWidth;
        }
    }
}
