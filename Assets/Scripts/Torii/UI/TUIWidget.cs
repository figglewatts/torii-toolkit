using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Torii.Resource;
using Torii.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Torii.UI
{
    public class TUIWidget : MonoBehaviour
    {
        public LayoutElement Element { get; private set; }

        public WidgetLayoutType LayoutType { get; private set; }
        public WidgetBackgroundType BackgroundType { get; private set; }

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
                        return null;
                }
            }
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
            get { return Layout.padding; }
            set { Layout.padding = value; }
        }

        public TUIWidget[] Children
        {
            get { return this.GetComponentsInChildrenNonRecursive<TUIWidget>(); }
        }

        public static TUIWidget Create(WidgetLayoutType layout, LayoutElement element = null)
        {
            return createBaseWidget(layout, WidgetBackgroundType.Sprite, element == null);
        }

        public static TUIWidget Create(WidgetLayoutType layout, Color background, LayoutElement element = null)
        {
            TUIWidget widget = createBaseWidget(layout, WidgetBackgroundType.Sprite, element == null);
            widget.GetComponent<TUIWidget>().Color = background;
            return widget;
        }

        public static TUIWidget Create(WidgetLayoutType layout, Sprite background, LayoutElement element = null)
        {
            TUIWidget widget = createBaseWidget(layout, WidgetBackgroundType.Sprite, element == null);
            Image uiImage = widget.GetComponent<Image>();
            uiImage.sprite = background;
            if (uiImage.sprite.HasBorder())
            {
                uiImage.type = Image.Type.Tiled;
            }

            return widget;
        }

        public static TUIWidget Create(WidgetLayoutType layout, Texture2D background, LayoutElement element = null)
        {
            TUIWidget widget = createBaseWidget(layout, WidgetBackgroundType.Texture, element == null);
            widget.GetComponent<RawImage>().texture = background;
            return widget;
        }

        public static TUIWidget Create(WidgetLayoutType layout, WidgetBackgroundType background, string path, LayoutElement element = null)
        {
            switch (background)
            {
                case WidgetBackgroundType.Sprite:
                {
                    Sprite backgroundSprite = ResourceManager.Load<Sprite>(path);
                    return Create(layout, backgroundSprite, element);
                }
                case WidgetBackgroundType.Texture:
                {
                    Texture2D backgroundTex = ResourceManager.Load<Texture2D>(path);
                    return Create(layout, backgroundTex, element);
                }
                default:
                    throw new ArgumentOutOfRangeException("background", background, "Background type not found!");
            }
        }

        private static TUIWidget createBaseWidget(WidgetLayoutType layout, WidgetBackgroundType background, bool sizeControlledByLayout)
        {
            GameObject obj = new GameObject("TUIWidget");
            obj.layer |= LayerMask.NameToLayer("UI");

            switch (layout)
            {
                case WidgetLayoutType.Grid:
                    obj.AddComponent<GridLayoutGroup>();
                    break;
                case WidgetLayoutType.Horizontal:
                    obj.AddComponent<VerticalLayoutGroup>();
                    break;
                case WidgetLayoutType.Vertical:
                    obj.AddComponent<HorizontalLayoutGroup>();
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

            if (!sizeControlledByLayout)
            {
                obj.AddComponent<LayoutElement>();
            }

            TUIWidget widget = obj.AddComponent<TUIWidget>();
            widget.LayoutType = layout;
            widget.BackgroundType = background;
            widget.Anchor = AnchorType.TopLeft;
            widget.Position = Vector2.zero;
            widget.Pivot = new Vector2(0, 1);

            return widget;
        }
    }

    public enum WidgetLayoutType
    {
        Grid,
        Horizontal,
        Vertical
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
