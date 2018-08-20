using System.Collections.Specialized;
using Torii.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Torii.UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasRenderer))]
    [ExecuteInEditMode]
    public class TUIWindow : MonoBehaviour
    {
        protected GameObject WindowTitleObject;
        protected GameObject WindowBodyObject;
        protected GameObject WindowCloseButtonObject;
        protected GameObject WindowTitleTextObject;

        protected LayoutElement WindowTitleLayoutElement;
        protected Text WindowTitleText;
        protected Button WindowCloseButton;

        protected TUIDraggable WindowDraggableScript;

        public void Awake()
        {
            // find all the dependent GameObjects
            WindowTitleObject = transform.GetChild(0).gameObject;
            WindowBodyObject = transform.GetChild(1).gameObject;
            WindowTitleTextObject = WindowTitleObject.transform.GetChild(0).gameObject;
            WindowCloseButtonObject = WindowTitleObject.transform.GetChild(1).gameObject;
            
            // find all the dependent components
            WindowTitleLayoutElement = WindowTitleObject.GetComponent<LayoutElement>();
            WindowTitleText = WindowTitleTextObject.GetComponent<Text>();
            WindowCloseButton = WindowCloseButtonObject.GetComponent<Button>();
            WindowDraggableScript = WindowTitleObject.GetComponent<TUIDraggable>();

            // set to close on click
            WindowCloseButton.onClick.AddListener(Close);

            RectTransform = GetComponent<RectTransform>();
        }

        public RectTransform RectTransform { get; protected set; }

        [ExposeProperty]
        public string WindowTitle
        {
            get { return WindowTitleText.text; }
            set
            {
                WindowTitleText.text = value;
            }
        }

        [ExposeProperty]
        public bool CanClose
        {
            get { return WindowCloseButtonObject.activeSelf; }
            set
            {
                WindowCloseButtonObject.SetActive(value);
            }
        }

        [ExposeProperty]
        public float TitleBarHeight
        {
            get { return WindowTitleLayoutElement.minHeight; }
            set { WindowTitleLayoutElement.minHeight = value; }
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        public void Show(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
