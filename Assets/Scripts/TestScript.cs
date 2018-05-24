using System.Collections;
using System.Collections.Generic;
using Torii;
using Torii.Resource;
using Torii.UI;
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

	    TUIWidget widget = TUIWidget.Create(WidgetLayoutType.Vertical, s);
	    TUICanvas.Instance.AddWidget(widget);
	    widget.Anchor = AnchorType.HStretchTop;
	    widget.Size = new Vector2(widget.Size.x, 300);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
