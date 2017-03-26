using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidebar : MonoBehaviour {

	TileArrangement map;

	// Use this for initialization
	void Start () 
	{
		map = GameObject.FindGameObjectWithTag ("Map").GetComponentInChildren<TileArrangement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver()
	{
		map.highlighter.gameObject.SetActive (false);
		map.highlighter.mouseIsOverSidebar = true;
	}

	void OnMouseExit()
	{
		map.highlighter.mouseIsOverSidebar = false;
	}
}
