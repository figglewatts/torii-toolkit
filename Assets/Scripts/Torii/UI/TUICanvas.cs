using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Torii.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Torii.UI
{
    public class TUICanvas : MonoSingleton<TUICanvas>
    {
        public Canvas Canvas
        {
            get { return GetComponent<Canvas>(); }
        }

        public TUIWidget[] Children
        {
            get { return this.GetComponentsInChildrenNonRecursive<TUIWidget>(); }
        }

        public void AddWidget(TUIWidget widget)
        {
            widget.transform.SetParent(this.transform);
            widget.Position = Vector2.zero;
        }

        public Vector2 Size
        {
            get { return new Vector2(Canvas.pixelRect.width, Canvas.pixelRect.height); }
        }

        public override void Init()
        {
            Canvas canvas = gameObject.AddComponent<Canvas>();
            canvas.worldCamera = Camera.main;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            gameObject.AddComponent<GraphicRaycaster>();
            gameObject.AddComponent<CanvasScaler>();
            gameObject.layer |= LayerMask.NameToLayer("UI");

            RectTransform rt = GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(Screen.width, Screen.height);
        }
    }
}
