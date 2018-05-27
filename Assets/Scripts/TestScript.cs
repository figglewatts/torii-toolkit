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
	    TUIStyleSheet sheet =
	        ResourceManager.UnityLoad<TUIStyleSheet>("ui/test-style");

        TUIWidget container = TUIWidget.Create(WidgetLayoutType.Vertical, Color.grey);
	    TUICanvas.Instance.AddWidget(container);
        container.Anchor = AnchorType.HStretchTop;
        container.Size = new Vector2(container.Size.x, 500);

        TUIWidget regularTest = sheet.CreateWidget();
        container.AddChild(regularTest);

	    TUIWidget redTest = sheet.CreateWidget("red");
        container.AddChild(redTest);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
