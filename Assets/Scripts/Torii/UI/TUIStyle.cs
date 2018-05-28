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
        protected TextAnchor ChildAlignment { get; set; }

        // GridLayout styling
        protected Vector2 GridCellSize { get; set; }
        protected GridLayoutGroup.Constraint GridConstraint { get; set; }
        protected int GridConstraintCount { get; set; }
        protected GridLayoutGroup.Axis GridStartAxis { get; set; }
        protected GridLayoutGroup.Corner GridStartCorner { get; set; }
        protected Vector2 GridLayoutSpacing { get; set; }

        // HorizontalOrVerticalLayout styling
        protected bool LayoutChildControlWidth { get; set; }
        protected bool LayoutChildControlHeight { get; set; }
        protected bool LayoutChildExpandWidth { get; set; }
        protected bool LayoutChildExpandHeight { get; set; }
        protected float LayoutSpacing { get; set; }

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

            string childAlignment = json.GetValueOrDefault<JSONString>("childAlignment", "UpperLeft");
            ChildAlignment = EnumUtil.Parse<TextAnchor>(childAlignment);

            if (LayoutType == WidgetLayoutType.Grid)
            {
                JSONNode gridLayoutNode = json.GetValueOrDefault<JSONNode>("gridLayout", new JSONObject());

                GridCellSize = gridLayoutNode.GetValueOrDefault<JSONNode>("cellSize", new Vector2(100, 100));

                string gridConstraint = gridLayoutNode.GetValueOrDefault<JSONString>("constraint", "Flexible");
                GridConstraint = EnumUtil.Parse<GridLayoutGroup.Constraint>(gridConstraint);

                GridConstraintCount = gridLayoutNode.GetValueOrDefault<JSONNumber>("constraintCount", 1);

                string gridStartAxis = gridLayoutNode.GetValueOrDefault<JSONString>("startAxis", "Horizontal");
                GridStartAxis = EnumUtil.Parse<GridLayoutGroup.Axis>(gridStartAxis);

                string gridStartCorner = gridLayoutNode.GetValueOrDefault<JSONString>("startCorner", "UpperLeft");
                GridStartCorner = EnumUtil.Parse<GridLayoutGroup.Corner>(gridStartCorner);

                GridLayoutSpacing = gridLayoutNode.GetValueOrDefault<JSONNode>("layoutSpacing", Vector2.zero);
            }
            else if (LayoutType == WidgetLayoutType.Horizontal || LayoutType == WidgetLayoutType.Vertical)
            {
                JSONNode layoutNode = json.GetValueOrDefault<JSONNode>("horizontalOrVerticalLayout", new JSONObject());

                LayoutChildControlWidth = layoutNode.GetValueOrDefault<JSONBool>("childControlWidth", true);
                LayoutChildControlHeight = layoutNode.GetValueOrDefault<JSONBool>("childControlHeight", true);
                LayoutChildExpandWidth = layoutNode.GetValueOrDefault<JSONBool>("childExpandWidth", true);
                LayoutChildExpandHeight = layoutNode.GetValueOrDefault<JSONBool>("childExpandHeight", true);
                LayoutSpacing = json.GetValueOrDefault<JSONNumber>("layoutSpacing", 0);
            }
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

            if (LayoutType != WidgetLayoutType.None)
            {
                widget.Layout.childAlignment = ChildAlignment;
            }

            if (LayoutType == WidgetLayoutType.Grid)
            {
                widget.GridLayout.cellSize = GridCellSize;
                widget.GridLayout.constraint = GridConstraint;
                widget.GridLayout.constraintCount = GridConstraintCount;
                widget.GridLayout.startAxis = GridStartAxis;
                widget.GridLayout.startCorner = GridStartCorner;
                widget.GridLayout.spacing = GridLayoutSpacing;
            }
            else if (LayoutType == WidgetLayoutType.Horizontal || LayoutType == WidgetLayoutType.Vertical)
            {
                widget.HorizontalOrVerticalLayout.childControlWidth = LayoutChildControlWidth;
                widget.HorizontalOrVerticalLayout.childControlHeight = LayoutChildControlHeight;
                widget.HorizontalOrVerticalLayout.childForceExpandWidth = LayoutChildExpandWidth;
                widget.HorizontalOrVerticalLayout.childForceExpandHeight = LayoutChildExpandHeight;
                widget.HorizontalOrVerticalLayout.spacing = LayoutSpacing;
            }

            return widget;
        }
    }
}
