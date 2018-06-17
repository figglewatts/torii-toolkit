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
        public TUIWidget Parent { get; protected set; }

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

        public static TUIWidget Create(LayoutElementData element = null)
        {
            return createBaseWidget<TUIWidget>(element);
        }

        protected static T createBaseWidget<T>(LayoutElementData element) where T : TUIWidget
        {
            GameObject obj = new GameObject("TUIWidget");
            obj.layer |= LayerMask.NameToLayer("UI");

            if (element != null)
            {
                LayoutElement layoutElement = obj.AddComponent<LayoutElement>();
                element.Set(ref layoutElement);
            }

            obj.AddComponent<EventTrigger>();
            obj.AddComponent<RectTransform>();

            T widget = obj.AddComponent<T>();
            widget.Anchor = AnchorType.TopLeft;
            widget.Position = Vector2.zero;
            widget.Pivot = new Vector2(0, 1);

            return widget;
        }
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
