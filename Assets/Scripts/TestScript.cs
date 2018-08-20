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

    [ExposeProperty]
    public int AProperty { get; set; }
    
    void Awake()
    {
    }

	void Start ()
	{
	    
    }

    // Update is called once per frame
	void Update () {
		
	}
}
