using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Torii.Resource;
using Torii.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Torii.UI
{
    public class TUIWidget : MonoBehaviour
    {
        public LayoutElement Element { get; protected set; }

        public WidgetLayoutType LayoutType { get; protected set; }
        public WidgetBackgroundType BackgroundType { get; protected set; }

        public TUIWidget Parent { get; protected set; }

        public AbstractWidgetChildPopulator ChildPopulator { get; set; }

        private readonly WriteOnce<TUIStyleSheet> _styleSheet = new WriteOnce<TUIStyleSheet>();
        public TUIStyleSheet StyleSheet
        {
            get { return _styleSheet.Value; }
            set { _styleSheet.Value = value; }
        }

        public EventTrigger Events
        {
            get { return GetComponent<EventTrigger>(); }
        }

        public Graphic Background
        {
            get
            {
                switch (BackgroundType)
                {
                    case WidgetBackgroundType.Sprite:
                        return GetComponent<Image>();
                    case WidgetBackgroundType.Texture:
                        return GetComponent<RawImage>();
                    default:
                        return GetComponent<Image>(); // Image is used without a Sprite to display color
                }
            }
        }

        public LayoutGroup Layout
        {
            get
            {
                switch (LayoutType)
                {
                    case WidgetLayoutType.Grid:
                        return GetComponent<GridLayoutGroup>();
                    case WidgetLayoutType.Horizontal:
                        return GetComponent<HorizontalLayoutGroup>();
                    case WidgetLayoutType.Vertical:
                        return GetComponent<VerticalLayoutGroup>();
                    default:
                        throw new InvalidOperationException("Cannot get LayoutGroup if widget has no layout component!");
                }
            }
        }

        public GridLayoutGroup GridLayout
        {
            get
            {
                if (LayoutType == WidgetLayoutType.Grid)
                {
                    return GetComponent<GridLayoutGroup>();
                }

                throw new InvalidOperationException("Cannot get GridLayoutGroup if widget does not have one!");
            }
        }

        public HorizontalOrVerticalLayoutGroup HorizontalOrVerticalLayout
        {
            get
            {
                if (LayoutType == WidgetLayoutType.Horizontal || LayoutType == WidgetLayoutType.Vertical)
                {
                    return GetComponent<HorizontalOrVerticalLayoutGroup>();
                }

                throw new InvalidOperationException("Cannot get HorizontalOrVerticalLayoutGroup if widget does not have one!");
            }
        }

        public HorizontalLayoutGroup HorizontalLayout
        {
            get
            {
                if (LayoutType == WidgetLayoutType.Horizontal)
                {
                    return GetComponent<HorizontalLayoutGroup>();
                }

                throw new InvalidOperationException("Cannot get HorizontalLayoutGroup if widget does not have one!");
            }
        }

        public VerticalLayoutGroup VerticalLayout
        {
            get
            {
                if (LayoutType == WidgetLayoutType.Vertical)
                {
                    return GetComponent<VerticalLayoutGroup>();
                }

                throw new InvalidOperationException("Cannot get VerticalLayoutGroup if widget does not have one!");
            }
        }

        public TextAnchor ChildAlignment
        {
            get { return Layout.childAlignment; }
            set { Layout.childAlignment = value; }
        }

        public RectTransform RectTransform
        {
            get { return GetComponent<RectTransform>(); }
        }

        public Vector2 Position
        {
            get { return RectTransform.anchoredPosition; }
            set { RectTransform.anchoredPosition = value; }
        }

        public Vector2 Size
        {
            get
            {
                RectTransform rt = RectTransform;
                Rect size = RectTransformUtility.PixelAdjustRect(rt, TUICanvas.Instance.Canvas);
                return new Vector2(size.width, size.height);
            }
            set
            {
                RectTransform rt = RectTransform;
                Vector2 canvasSize = TUICanvas.Instance.Size;
                RectTransform.sizeDelta = value - Vector2.Scale(rt.anchorMax - rt.anchorMin, canvasSize);
            }
        }

        public Vector2 Pivot
        {
            get { return RectTransform.pivot; }
            set { RectTransform.pivot = value; }
        }

        public AnchorType Anchor
        {
            set
            {
                RectTransform rt = RectTransform;
                switch (value)
                {
                    case AnchorType.TopLeft:
                        rt.anchorMin = new Vector2(0, 1);
                        rt.anchorMax = new Vector2(0, 1);
                        break;
                    case AnchorType.TopMiddle:
                        rt.anchorMin = new Vector2(0.5f, 1);
                        rt.anchorMax = new Vector2(0.5f, 1);
                        break;
                    case AnchorType.TopRight:
                        rt.anchorMin = Vector2.one;
                        rt.anchorMax = Vector2.one;
                        break;
                    case AnchorType.HStretchTop:
                        rt.anchorMin = new Vector2(0, 1);
                        rt.anchorMax = Vector2.one;
                        break;
                    case AnchorType.MiddleLeft:
                        rt.anchorMin = new Vector2(0, 0.5f);
                        rt.anchorMax = new Vector2(0, 0.5f);
                        break;
                    case AnchorType.Center:
                        rt.anchorMin = new Vector2(0.5f, 0.5f);
                        rt.anchorMax = new Vector2(0.5f, 0.5f);
                        break;
                    case AnchorType.MiddleRight:
                        rt.anchorMin = new Vector2(1, 0.5f);
                        rt.anchorMax = new Vector2(1, 0.5f);
                        break;
                    case AnchorType.HStretchMiddle:
                        rt.anchorMin = new Vector2(0, 0.5f);
                        rt.anchorMax = new Vector2(1, 0.5f);
                        break;
                    case AnchorType.BottomLeft:
                        rt.anchorMin = Vector2.zero;
                        rt.anchorMax = Vector2.zero;
                        break;
                    case AnchorType.BottomMiddle:
                        rt.anchorMin = new Vector2(0.5f, 0);
                        rt.anchorMax = new Vector2(0.5f, 0);
                        break;
                    case AnchorType.BottomRight:
                        rt.anchorMin = new Vector2(1, 0);
                        rt.anchorMax = new Vector2(1, 0);
                        break;
                    case AnchorType.HStretchBottom:
                        rt.anchorMin = Vector2.zero;
                        rt.anchorMax = new Vector2(1, 0);
                        break;
                    case AnchorType.VStretchLeft:
                        rt.anchorMin = Vector2.zero;
                        rt.anchorMax = new Vector2(0, 1);
                        break;
                    case AnchorType.VStretchMiddle:
                        rt.anchorMin = new Vector2(0.5f, 0);
                        rt.anchorMax = new Vector2(0.5f, 1);
                        break;
                    case AnchorType.VStretchRight:
                        rt.anchorMin = new Vector2(1, 0);
                        rt.anchorMax = Vector2.one;
                        break;
                    case AnchorType.Stretch:
                        rt.anchorMin = Vector2.zero;
                        rt.anchorMax = Vector2.one;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value", value, "Anchor type not found!");
                }
                rt.sizeDelta = Vector2.zero;
            }
        }

        public Color Color
        {
            get { return Background.color; }
            set { Background.color = value; }
        }

        public RectOffset Padding
        {
            get { return LayoutType == WidgetLayoutType.None ? null : Layout.padding; }
            set
            {
                if (LayoutType == WidgetLayoutType.None) return;
                Layout.padding = value;
            }
        }

        public TUIWidget[] Children
        {
            get { return this.GetComponentsInChildrenNonRecursive<TUIWidget>(); }
        }

        public void AddChild(TUIWidget widget)
        {
            widget.transform.SetParent(this.transform);
            widget.Parent = this;
        }

        public void RemoveChild(TUIWidget widget)
        {
            widget.transform.SetParent(null);
            widget.Parent = null;
        }

        public void RegisterEvent(EventTriggerType type, UnityAction<BaseEventData> callback)
        {
            foreach (EventTrigger.Entry entry in Events.triggers)
            {
                if (entry.eventID == type)
                {
                    entry.callback.AddListener(callback);
                    return;
                }
            }

            EventTrigger.Entry newEntry = new EventTrigger.Entry();
            newEntry.eventID = type;
            newEntry.callback.AddListener(callback);
            Events.triggers.Add(newEntry);
        }

        public virtual void PopulateChildren()
        {
            if (ChildPopulator == null)
            {
                throw new InvalidOperationException("Cannot populate if widget does not have ChildPopulator!");
            }

            TUIWidget[] children = ChildPopulator.CreateChildren();
            foreach (TUIWidget child in children)
            {
                AddChild(child);
            }
        }

        public static TUIWidget Create(WidgetLayoutType layout, LayoutElementData element = null)
        {
            return createBaseWidget(layout, WidgetBackgroundType.Sprite, element);
        }

        public static TUIWidget Create(WidgetLayoutType layout, Color background, LayoutElementData element = null)
        {
            TUIWidget widget = createBaseWidget(layout, WidgetBackgroundType.Sprite, element);
            widget.GetComponent<TUIWidget>().Color = background;
            return widget;
        }

        public static TUIWidget Create(WidgetLayoutType layout, Sprite background, LayoutElementData element = null)
        {
            TUIWidget widget = createBaseWidget(layout, WidgetBackgroundType.Sprite, element);
            Image uiImage = widget.GetComponent<Image>();
            uiImage.sprite = background;
            if (uiImage.sprite.HasBorder())
            {
                uiImage.type = Image.Type.Tiled;
            }

            return widget;
        }

        public static TUIWidget Create(WidgetLayoutType layout, Texture2D background, LayoutElementData element = null)
        {
            TUIWidget widget = createBaseWidget(layout, WidgetBackgroundType.Texture, element);
            widget.GetComponent<RawImage>().texture = background;
            return widget;
        }

        public static TUIWidget Create(WidgetLayoutType layout, WidgetBackgroundType background, string path, bool streamingAssets = true, LayoutElementData element = null)
        {
            path = PathUtil.Combine(streamingAssets ? TUI.UIUserDataDirectory : TUI.UIDataDirectory, path);

            switch (background)
            {
                case WidgetBackgroundType.Sprite:
                {
                    var backgroundSprite = streamingAssets
                        ? ResourceManager.Load<Sprite>(path)
                        : ResourceManager.UnityLoad<Sprite>(path);

                    return Create(layout, backgroundSprite, element);
                }
                case WidgetBackgroundType.Texture:
                {
                    var backgroundTex = streamingAssets
                        ? ResourceManager.Load<Texture2D>(path)
                        : ResourceManager.UnityLoad<Texture2D>(path);
                        return Create(layout, backgroundTex, element);
                }
                default:
                    throw new ArgumentOutOfRangeException("background", background, "Background type not found!");
            }
        }

        protected static TUIWidget createBaseWidget(WidgetLayoutType layout, WidgetBackgroundType background, LayoutElementData element)
        {
            GameObject obj = new GameObject("TUIWidget");
            obj.layer |= LayerMask.NameToLayer("UI");

            switch (layout)
            {
                case WidgetLayoutType.Grid:
                    obj.AddComponent<GridLayoutGroup>();
                    break;
                case WidgetLayoutType.Horizontal:
                    obj.AddComponent<HorizontalLayoutGroup>();
                    break;
                case WidgetLayoutType.Vertical:
                    obj.AddComponent<VerticalLayoutGroup>();
                    break;
                case WidgetLayoutType.None:
                    // intentionally empty
                    break;
                default:
                    throw new ArgumentOutOfRangeException("layout", layout, "Layout type not found!");
            }
            
            switch (background)
            {
                case WidgetBackgroundType.Sprite:
                    obj.AddComponent<Image>();
                    break;
                case WidgetBackgroundType.Texture:
                    obj.AddComponent<RawImage>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("background", background, "Background type not found!");
            }

            if (element != null)
            {
                LayoutElement layoutElement = obj.AddComponent<LayoutElement>();
                element.Set(ref layoutElement);
            }

            obj.AddComponent<EventTrigger>();

            TUIWidget widget = obj.AddComponent<TUIWidget>();
            widget.LayoutType = layout;
            widget.BackgroundType = background;
            widget.Anchor = AnchorType.TopLeft;
            widget.Position = Vector2.zero;
            widget.Pivot = new Vector2(0, 1);
            widget.Color = Color.white;

            return widget;
        }
    }

    public enum WidgetLayoutType
    {
        Grid,
        Horizontal,
        Vertical,
        None
    }

    public enum WidgetBackgroundType
    {
        Sprite,
        Texture
    }

    public enum AnchorType
    {
        TopLeft,
        MiddleLeft,
        BottomLeft,
        TopMiddle,
        Center,
        BottomMiddle,
        TopRight,
        MiddleRight,
        BottomRight,
        HStretchTop,
        HStretchMiddle,
        HStretchBottom,
        VStretchLeft,
        VStretchMiddle,
        VStretchRight,
        Stretch
    }
}
