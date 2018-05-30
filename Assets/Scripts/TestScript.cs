using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using Torii;
using Torii.Binding;
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
	        ResourceManager.Load<TUIStyleSheet>(Application.streamingAssetsPath + "/ui/styles/test-style.json");

	    TUIWidget container = sheet.CreateWidget("container");
	    TUICanvas.Instance.AddWidget(container);
        container.Anchor = AnchorType.HStretchTop;
        container.Size = new Vector2(container.Size.x, 500);
	    container.Position = Vector2.zero;
	    container.VerticalLayout.spacing = 20;

        container.PopulateChildren();

	    float test = 0;
	    ModelTest testModel = gameObject.AddComponent<ModelTest>();
        BindBrokerTest testBroker = new BindBrokerTest(testModel);
        testBroker.Bind("AFloat", () => test);

	    testModel.AFloat = 3.4f;

        Debug.Log(test);
	}

    // Update is called once per frame
	void Update () {
		
	}
}
