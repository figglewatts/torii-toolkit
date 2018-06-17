using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using SimpleJSON;
using Torii;
using Torii.Binding;
using Torii.Resource;
using Torii.UI;
using Torii.UI.Widgets;
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
	    TUIContainer container = TUIContainer.Create(ContainerLayoutType.Horizontal);
	    container.HorizontalLayout.spacing = 5f;
	    container.Anchor = AnchorType.Stretch;
	    container.HorizontalOrVerticalLayout.childControlWidth = false;
	    container.HorizontalOrVerticalLayout.childControlHeight = false;
        TUICanvas.Instance.AddWidget(container);
	    
	    TUILabel label = TUILabel.Create("Test text 1", new TUILabelSettings
	    {
            FontSize = 48
	    });
        label.Size = new Vector2(300, 200);
        container.AddChild(label);

	    TUILabel label2 = TUILabel.Create("Test text 2", new TUILabelSettings
	    {
	        FontSize = 48
	    });
	    label2.Size = new Vector2(300, 200);
	    container.AddChild(label2);

	    TUILabel label3 = TUILabel.Create("Test text 3", new TUILabelSettings
	    {
	        FontSize = 48
	    });
	    label3.Size = new Vector2(300, 200);
	    container.AddChild(label3);
    }

    // Update is called once per frame
	void Update () {
		
	}
}
