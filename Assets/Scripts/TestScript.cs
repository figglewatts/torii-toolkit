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
	    TUILabel label = TUILabel.Create("Test text", new TUILabelSettings
	    {
            FontSize = 48
	    });
	    TUICanvas.Instance.AddWidget(label);
        label.Size = new Vector2(600, 200);
    }

    // Update is called once per frame
	void Update () {
		
	}
}
