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

        public Color Color
        {
            set { Background.color = value; }
        }

        public RectOffset Padding
        {
            set { Layout.padding = value; }
        }

        public TUIWidget[] Children
        {
            get { return GetComponentsInChildren<TUIWidget>(true); }
        }

        public static GameObject Create(WidgetLayoutType layout, LayoutElement element = null)
        {
            return createBaseWidget(layout, WidgetBackgroundType.Sprite, element == null);
        }

        public static GameObject Create(WidgetLayoutType layout, Color background, LayoutElement element = null)
        {
            GameObject widget = createBaseWidget(layout, WidgetBackgroundType.Sprite, element == null);
            widget.GetComponent<TUIWidget>().Color = background;
            return widget;
        }

        public static GameObject Create(WidgetLayoutType layout, Sprite background, LayoutElement element = null)
        {
            GameObject widget = createBaseWidget(layout, WidgetBackgroundType.Sprite, element == null);
            Image uiImage = widget.GetComponent<Image>();
            uiImage.sprite = background;
            if (uiImage.sprite.HasBorder())
            {
                uiImage.type = Image.Type.Tiled;
            }

            return widget;
        }

        public static GameObject Create(WidgetLayoutType layout, Texture2D background, LayoutElement element = null)
        {
            GameObject widget = createBaseWidget(layout, WidgetBackgroundType.Texture, element == null);
            widget.GetComponent<RawImage>().texture = background;
            return widget;
        }

        public static GameObject Create(WidgetLayoutType layout, WidgetBackgroundType background, string path, LayoutElement element = null)
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

        private static GameObject createBaseWidget(WidgetLayoutType layout, WidgetBackgroundType background, bool sizeControlledByLayout)
        {
            GameObject obj = new GameObject("TUIWidget");
            TUIWidget widget = obj.AddComponent<TUIWidget>();
            widget.LayoutType = layout;
            widget.BackgroundType = background;

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

            return obj;
        }
    }

    [Serializable]
    public enum WidgetLayoutType
    {
        Grid,
        Horizontal,
        Vertical
    }

    [Serializable]
    public enum WidgetBackgroundType
    {
        Sprite,
        Texture
    }
}
