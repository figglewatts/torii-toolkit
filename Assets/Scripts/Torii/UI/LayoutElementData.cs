using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;
using Torii.Util;
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

        public LayoutElementData(JSONNode json)
        {
            IgnoreLayout = json.GetValueOrDefault<JSONBool>("ignoreLayout", false);
            MinWidth = json.GetValueOrDefault<JSONNumber>("minWidth", -1);
            MinHeight = json.GetValueOrDefault<JSONNumber>("minHeight", -1);
            PreferredWidth = json.GetValueOrDefault<JSONNumber>("preferredWidth", -1);
            PreferredHeight = json.GetValueOrDefault<JSONNumber>("preferredHeight", -1);
            FlexibleWidth = json.GetValueOrDefault<JSONNumber>("flexibleWidth", -1);
            FlexibleHeight = json.GetValueOrDefault<JSONNumber>("flexibleHeight", -1);
            LayoutPriority = json.GetValueOrDefault<JSONNumber>("layoutPriority", 1);
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
