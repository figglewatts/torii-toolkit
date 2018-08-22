using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using SimpleJSON;
using Torii;
using Torii.Binding;
using Torii.Resource;
using Torii.Serialization;
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
        SerializationTest test = new SerializationTest()
        {
            Name = "Testing serialization!!",
            Number = 3.141f
        };
        test.Strings.AddRange(new [] {"test 1", "test 2", "test 3"});

        ToriiSerializer serializer = new ToriiSerializer();
        //serializer.Serialize(test, "test.dat");

        SerializationTest test2 = serializer.Deserialize<SerializationTest>("test2.dat");

        Debug.Log(test2.Name);
        Debug.Log(test2.Number);
    }

	void Start ()
	{
	    
    }

    // Update is called once per frame
	void Update () {
		
	}
}
