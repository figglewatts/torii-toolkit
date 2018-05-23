using System.Collections;
using System.Collections.Generic;
using Torii;
using Torii.Resource;
using Torii.UI;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {

    void Awake()
    {
        ToriiToolkit.Initialize();
    }

	void Start ()
	{
	    TUIWidget widget = TUIWidget.Create(WidgetLayoutType.Vertical, WidgetBackgroundType.Sprite,
	        Application.streamingAssetsPath + "/ui/sprite/test.json");
        TUICanvas.Instance.AddWidget(widget);
	    widget.Anchor = AnchorType.Stretch;
        Debug.Log(widget.Size);
        Vector2 canvasSize = new Vector2(TUICanvas.Instance.Canvas.pixelRect.width, TUICanvas.Instance.Canvas.pixelRect.height);
        Debug.Log(Vector2.Scale(widget.RectTransform.anchorMax - widget.RectTransform.anchorMin, canvasSize));
        Debug.Log(widget.RectTransform.rect.width);
	    widget.Size = new Vector2(100, 100);
        Debug.Log(widget.Size);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
