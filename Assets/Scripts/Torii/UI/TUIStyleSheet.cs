using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;
using UnityEngine;

namespace Torii.UI
{
    public class TUIStyleSheet
    {
        /*private readonly Dictionary<string, TUIStyle> _classes;

        public TUIStyleSheet(JSONNode json)
        {
            _classes = new Dictionary<string, TUIStyle>();
            _classes["*"] = TUIStyle.DefaultFallback;;

            foreach (string key in json.Keys)
            {
                _classes[key] = new TUIStyle(json[key], this, key);
            }
        }

        public TUIWidget CreateWidget(string widgetClass = "*")
        {
            TUIStyle style = null;

            // if the class doesn't exist use the default
            if (!_classes.ContainsKey(widgetClass))
            {
                Debug.LogWarning("TUIStyleSheet: TUIWidget style class '" + widgetClass + "' not found! Using default instead.");
                style = TUIStyle.DefaultFallback;;
            }
            else
            {
                style = _classes[widgetClass];
            }

            TUIWidget widget = style.CreateWidget();
            widget.StyleSheet = this;
            return widget;
        }*/
    }
}
