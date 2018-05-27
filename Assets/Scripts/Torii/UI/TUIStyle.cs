using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;
using Torii.Resource;
using Torii.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Torii.UI
{
    public class TUIStyle
    {
        protected string BackgroundGraphicPath { get; set; }
        protected bool UseStreamingAssets { get; set; }
        protected WidgetLayoutType LayoutType { get; set; }
        protected WidgetBackgroundType BackgroundType { get; set; }
        protected LayoutElementData LayoutElement { get; set; }
        protected Vector2 Pivot { get; set; }
        protected Color Color { get; set; }
        protected AnchorType Anchor { get; set; }
        protected RectOffset Padding { get; set; }

        // TODO: layout spacing

        public TUIStyle(JSONNode json)
        {
            string backgroundType =
                json.GetValueOrDefault<JSONString>("backgroundType", WidgetBackgroundType.Sprite.ToString());
            BackgroundType = EnumUtil.Parse<WidgetBackgroundType>(backgroundType);

            BackgroundGraphicPath = json["backgroundGraphic"];

            UseStreamingAssets = json.GetValueOrDefault<JSONBool>("streamingAssets", true);

            string layoutType = json.GetValueOrDefault<JSONString>("layoutType", WidgetLayoutType.None.ToString());
            LayoutType = EnumUtil.Parse<WidgetLayoutType>(layoutType);

            JSONNode layoutElement = json["layoutElement"];
            LayoutElement = layoutElement == null ? null : new LayoutElementData(layoutElement);

            Pivot = json.GetValueOrDefault<JSONNode>("pivot", new Vector2(0.5f, 0.5f));

            Color = json.GetValueOrDefault<JSONNode>("color", UnityEngine.Color.white);

            string anchorType = json.GetValueOrDefault<JSONString>("anchorType", AnchorType.TopLeft.ToString());
            Anchor = EnumUtil.Parse<AnchorType>(anchorType);

            Padding = json.GetValueOrDefault<JSONNode>("padding", new RectOffset(0, 0, 0, 0));
        }

        public TUIStyle(string backgroundGraphicPath,
            bool useStreamingAssets,
            WidgetLayoutType layoutType,
            WidgetBackgroundType backgroundType,
            LayoutElementData layoutElement,
            Vector2 pivot,
            Color color,
            AnchorType anchor,
            RectOffset padding)
        {
            BackgroundGraphicPath = backgroundGraphicPath;
            UseStreamingAssets = useStreamingAssets;
            LayoutType = layoutType;
            BackgroundType = backgroundType;
            LayoutElement = layoutElement;
            Pivot = pivot;
            Color = color;
            Anchor = anchor;
            Padding = padding;
        }

        public static readonly TUIStyle DefaultFallback = new TUIStyle("", true, WidgetLayoutType.None,
            WidgetBackgroundType.Sprite, null, new Vector2(0.5f, 0.5f), UnityEngine.Color.white,
            AnchorType.TopLeft, new RectOffset(0, 0, 0, 0));

        public TUIWidget CreateWidget()
        {
            TUIWidget widget = null;

            // if there is no background graphic
            if (string.IsNullOrEmpty(BackgroundGraphicPath))
            {
                widget = TUIWidget.Create(LayoutType, LayoutElement);
            }
            else
            {
                widget = TUIWidget.Create(LayoutType, BackgroundType, BackgroundGraphicPath, UseStreamingAssets, LayoutElement);
            }

            widget.Color = Color;
            widget.Pivot = Pivot;
            widget.Anchor = Anchor;
            widget.Padding = Padding;

            return widget;
        }
    }
}
