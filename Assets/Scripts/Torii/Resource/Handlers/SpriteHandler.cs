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
        public Type GetResourceType()
        {
            return typeof(Sprite);
        }

        public void Load(string path, ResourceLifespan span)
        {
            JSONNode sprite = JSONUtil.ReadJSONFromDisk(path);
            string type = sprite["type"].Value;
            if (!sprite["type"].Value.Equals("sprite"))
            {
                throw new ResourceLoadException(
                    "Could not load resource: JSON type '" + sprite["type"] + "' did not match 'sprite'",
                    typeof(Sprite));
            }
            string textureValue = sprite["texture"];
            JSONObject rectValue = sprite["rect"].AsObject;
            JSONObject pivotValue = sprite["pivot"].AsObject;
            float ppuValue = sprite["ppu"];
            JSONObject borderValue = sprite["border"].AsObject;

            Texture2D texture = ResourceManager.Load<Texture2D>(TUI.UIDirectory + "/" + textureValue, span);
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            if (rectValue.Count != 0)
            {
                rect = rectValue;
            }
            Vector2 pivot = new Vector2(texture.width / 2f, texture.height / 2f);
            if (pivotValue.Count != 0)
            {
                pivot = pivotValue;
            }
            Vector4 border = Vector4.zero;
            if (borderValue.Count != 0)
            {
                border = borderValue;
            }

            Sprite s = Sprite.Create(texture, rect, pivot, ppuValue, 0, SpriteMeshType.FullRect, border);
            Resource<Sprite> resource = new Resource<Sprite>(s, span);
            ResourceManager.RegisterResource(path, resource);
        }
    }
}
