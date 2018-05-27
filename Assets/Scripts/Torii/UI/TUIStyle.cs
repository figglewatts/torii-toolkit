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
        public string BackgroundGraphicPath { get; protected set; }
        public WidgetLayoutType LayoutType { get; protected set; }
        public WidgetBackgroundType BackgroundType { get; protected set; }
        public LayoutElementData LayoutElement { get; protected set; }
        public Vector2? Pivot { get; protected set; }
        public Color? Color { get; protected set; }
        public AnchorType Anchor { get; protected set; }
        public RectOffset Padding { get; protected set; }

        public TUIStyle(JSONNode json)
        {
            string backgroundType =
                json.GetValueOrDefault<JSONString>("backgroundType", WidgetBackgroundType.Sprite.ToString());
            BackgroundType = EnumUtil.Parse<WidgetBackgroundType>(backgroundType);

            BackgroundGraphicPath = json["backgroundGraphic"];

            string layoutType = json.GetValueOrDefault<JSONString>("layoutType", WidgetLayoutType.None.ToString());
            LayoutType = EnumUtil.Parse<WidgetLayoutType>(layoutType);

            JSONNode layoutElement = json["layoutElement"];
            LayoutElement = layoutElement == null ? null : new LayoutElementData(layoutElement);

            Pivot = json["pivot"];

            Color = json["color"];

            string anchorType = json.GetValueOrDefault<JSONString>("anchorType", AnchorType.TopLeft.ToString());
            Anchor = EnumUtil.Parse<AnchorType>(anchorType);

            Padding = json.GetValueOrDefault<JSONNode>("padding", new RectOffset(0, 0, 0, 0));
        }

        public TUIStyle(string backgroundGraphicPath,
            WidgetLayoutType layoutType,
            WidgetBackgroundType backgroundType,
            LayoutElementData layoutElement,
            Vector2? pivot,
            Color color,
            AnchorType anchor,
            RectOffset padding)
        {
            BackgroundGraphicPath = backgroundGraphicPath;
            LayoutType = layoutType;
            BackgroundType = backgroundType;
            LayoutElement = layoutElement;
            Pivot = pivot;
            Color = color;
            Anchor = anchor;
            Padding = padding;
        }

        public static readonly TUIStyle DefaultFallback = new TUIStyle("", WidgetLayoutType.None,
            WidgetBackgroundType.Sprite, null, null, UnityEngine.Color.white,
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
                widget = TUIWidget.Create(LayoutType, BackgroundType, BackgroundGraphicPath, LayoutElement);
            }

            widget.Color = Color.GetValueOrDefault(UnityEngine.Color.white);
            widget.Pivot = Pivot.GetValueOrDefault(new Vector2(0.5f, 0.5f));
            widget.Anchor = Anchor;
            widget.Padding = Padding;

            return widget;
        }
    }
}
