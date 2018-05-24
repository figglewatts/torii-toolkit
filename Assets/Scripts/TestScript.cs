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
	    TUIWidget widget = TUIWidget.Create(WidgetLayoutType.Vertical, new Color(1, 1, 1, 0));
	    TUICanvas.Instance.AddWidget(widget);
	    widget.Anchor = AnchorType.HStretchTop;
	    widget.Size = new Vector2(widget.Size.x, 300);
	    VerticalLayoutGroup lg = widget.Layout as VerticalLayoutGroup;
	    lg.spacing = 20;


        TUIWidget subWidget1 = TUIWidget.Create(WidgetLayoutType.Horizontal, Color.red);
        widget.AddChild(subWidget1);
        subWidget1.RegisterEvent(EventTriggerType.PointerClick, eventData =>
        {
            PointerEventData ped = eventData as PointerEventData;
            Debug.Log("Clicked! " + ped.position + " " + 1);
        });

	    TUIWidget subWidget2 = TUIWidget.Create(WidgetLayoutType.Horizontal, Color.red);
	    widget.AddChild(subWidget2);
	    subWidget2.RegisterEvent(EventTriggerType.PointerClick, eventData =>
	    {
	        PointerEventData ped = eventData as PointerEventData;
	        Debug.Log("Clicked! " + ped.position + " " + 2);
	    });

	    TUIWidget subWidget3 = TUIWidget.Create(WidgetLayoutType.Horizontal, Color.red);
	    widget.AddChild(subWidget3);
	    subWidget3.RegisterEvent(EventTriggerType.PointerClick, eventData =>
	    {
	        PointerEventData ped = eventData as PointerEventData;
	        Debug.Log("Clicked! " + ped.position + " " + 3);
	    });

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
