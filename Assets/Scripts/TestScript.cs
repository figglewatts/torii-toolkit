using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using Torii;
using Torii.Resource;
using Torii.UI;
using Torii.Util;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {

    void Awake()
    {
        ToriiToolkit.Initialize();
    }

	void Start ()
	{
	    Sprite s = ResourceManager.UnityLoad<Sprite>("test-9s");
	    Sprite s2 = ResourceManager.UnityLoad<Sprite>("test-9s");

	    TUIWidget widget = TUIWidget.Create(WidgetLayoutType.Vertical, s);
	    TUICanvas.Instance.AddWidget(widget);
	    widget.Anchor = AnchorType.HStretchTop;
	    widget.Size = new Vector2(widget.Size.x, 300);

	    TUIWidget widget2 = TUIWidget.Create(WidgetLayoutType.Vertical, s2);
	    TUICanvas.Instance.AddWidget(widget2);
	    widget2.Anchor = AnchorType.HStretchBottom;
	    widget2.Size = new Vector2(widget.Size.x, 200);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
