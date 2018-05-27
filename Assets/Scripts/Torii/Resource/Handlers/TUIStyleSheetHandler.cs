using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;
using Torii.UI;
using Torii.Util;
using UnityEngine;

namespace Torii.Resource.Handlers
{
    public class TUIStyleSheetHandler : IResourceHandler, ITextAssetHandler
    {
        public Type HandlerType
        {
            get { return typeof(TUIStyleSheet); }
        }

        public void Load(string path, int span)
        {
            JSONNode styleSheetJson = JSONUtil.ReadJSONFromDisk(path);
            TUIStyleSheet sheet = new TUIStyleSheet(styleSheetJson);
            Resource<TUIStyleSheet> resource = new Resource<TUIStyleSheet>(sheet, span);
            ResourceManager.RegisterResource(path, resource);
        }

        public object Process(TextAsset textAsset)
        {
            JSONNode stylesheetJson = JSON.Parse(textAsset.text);
            return new TUIStyleSheet(stylesheetJson);
        }
    }
}
