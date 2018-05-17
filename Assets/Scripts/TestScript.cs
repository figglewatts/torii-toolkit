using System.Collections;
using System.Collections.Generic;
using Torii.Resource;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    Sprite s = ResourceManager.Load<Sprite>(Application.streamingAssetsPath + "/ui/sprite/test.json");

        Image i = GameObject.FindWithTag("Respawn").GetComponent<Image>();
	    i.sprite = s;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
