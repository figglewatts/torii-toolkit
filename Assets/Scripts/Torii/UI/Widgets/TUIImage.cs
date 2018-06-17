using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torii.Resource;
using Torii.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Torii.UI.Widgets
{
    public enum ImageType
    {
        Texture,
        Sprite
    }
    
    public class TUIImage : TUIGraphic
    {
        public static TUIImage Create(Color color, LayoutElementData element = null)
        {
            TUIImage image = createBaseWidget<TUIImage>(element);

            Image uiImage = image.gameObject.AddComponent<Image>();
            uiImage.color = color;
            image.Graphic = uiImage;

            return image;
        }

        public static TUIImage Create(Sprite sprite, LayoutElementData element = null)
        {
            TUIImage image = createBaseWidget<TUIImage>(element);

            Image uiImage = image.gameObject.AddComponent<Image>();
            uiImage.sprite = sprite;
            if (uiImage.sprite.HasBorder())
            {
                uiImage.type = Image.Type.Tiled;
            }

            image.Graphic = uiImage;

            return image;
        }

        public static TUIImage Create(Texture2D texture, LayoutElementData element = null)
        {
            TUIImage image = createBaseWidget<TUIImage>(element);

            RawImage uiImage = image.gameObject.AddComponent<RawImage>();
            uiImage.texture = texture;
            image.Graphic = uiImage;

            return image;
        }

        public static TUIImage Create(string path, ImageType type, bool streamingAssets = true, LayoutElementData element = null)
        {
            path = PathUtil.Combine(streamingAssets ? TUI.UIUserDataDirectory : TUI.UIDataDirectory, path);

            switch (type)
            {
                case ImageType.Sprite:
                {
                    var sprite = streamingAssets
                        ? ResourceManager.Load<Sprite>(path)
                        : ResourceManager.UnityLoad<Sprite>(path);
                    return Create(sprite, element);
                }
                case ImageType.Texture:
                {
                    var texture = streamingAssets
                        ? ResourceManager.Load<Texture2D>(path)
                        : ResourceManager.UnityLoad<Texture2D>(path);
                    return Create(texture, element);
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Image type not found!");
                }
            }
        }
    }
}
