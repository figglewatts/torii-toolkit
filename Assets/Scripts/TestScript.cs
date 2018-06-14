using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        
	    ViewTest testView = new ViewTest(69);
	    ModelTest testModel = gameObject.AddComponent<ModelTest>();

        Debug.Log(testModel.GUID);

        BindBrokerTest testBroker = new BindBrokerTest(testModel, testView);

	    Debug.Log("binder: " + testModel.AFloat);
	    Debug.Log("bindee: " + testView.AnotherOne);

        testBroker.Bind(() => testModel.AFloat, () => testView.AnotherOne, AbstractBindBroker.BindingType.TwoWay);
        Debug.Log("Bound");

	    //testModel.AFloat = 3.4f;
	    testView.AnotherOne = 666;

        Debug.Log("binder: " + testModel.AFloat);
        Debug.Log("bindee: " + testView.AnotherOne);
	}

    // Update is called once per frame
	void Update () {
		
	}
}
