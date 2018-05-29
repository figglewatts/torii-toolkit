using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;
using Torii.Exceptions;
using Torii.UI;
using Torii.Util;
using UnityEngine;

namespace Torii.Resource.Handlers
{
    class SpriteHandler : IResourceHandler
    {
        public Type HandlerType
        {
            get { return typeof(Sprite); }
        }

        public void Load(string path, int span)
        {
            JSONNode sprite = JSONUtil.ReadJSONFromDisk(path);
            string type = sprite["type"].Value;
            if (!sprite["type"].Value.Equals("sprite"))
            {
                throw new ToriiResourceLoadException(
                    "Could not load resource: JSON type '" + sprite["type"] + "' did not match 'sprite'",
                    typeof(Sprite));
            }

            bool streamingAssets = sprite.GetValueOrDefault<JSONBool>("streamingAssets", true);

            string textureValue = sprite["texture"];
            if (string.IsNullOrEmpty(textureValue))
            {
                throw new ArgumentException(
                    "SpriteHandler: \"texture\" JSON key was null or empty! Sprite must have a texture.", "path");
            }

            float ppuValue = sprite.GetValueOrDefault<JSONNumber>("ppu", 100);

            Texture2D texture = streamingAssets
                ? ResourceManager.Load<Texture2D>(PathUtil.Combine(TUI.UIUserDataDirectory, textureValue))
                : ResourceManager.UnityLoad<Texture2D>(PathUtil.Combine(TUI.UIDataDirectory, textureValue));

            Rect rect = sprite.GetValueOrDefault<JSONNode>("rect", new Rect(0, 0, texture.width, texture.height));

            Vector2 pivot =
                sprite.GetValueOrDefault<JSONNode>("pivot", new Vector2(texture.width / 2f, texture.height / 2f));

            Vector4 border = sprite.GetValueOrDefault<JSONNode>("border", Vector4.zero);

            Sprite s = Sprite.Create(texture, rect, pivot, ppuValue, 0, SpriteMeshType.FullRect, border);
            Resource<Sprite> resource = new Resource<Sprite>(s, span);
            ResourceManager.RegisterResource(path, resource);
        }
    }
}
