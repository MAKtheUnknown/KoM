using UnityEngine;
using System.Collections;

public class Zooming : MonoBehaviour 
{
	new Camera camera;
	Canvas canvas;

	public double scrollBorder = 10.0;

	// Use this for initialization
	void Start () 
	{
		this.camera = this.GetComponent<Camera> ();
		this.canvas = GameObject.FindGameObjectWithTag ("UI").GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if (Input.GetAxis ("Mouse ScrollWheel")>0 || Input.GetKey(KeyCode.E)) 
		{
			this.camera.orthographicSize+=.05f;
			this.canvas.transform.Translate (0f, 0f, 500f);
		}
		if (Input.GetAxis ("Mouse ScrollWheel")<0 || Input.GetKey(KeyCode.Q)) 
		{
			this.camera.orthographicSize-=.05f;
			this.canvas.transform.Translate (0f, 0f, -500f);
		}
		if (Input.GetKey (KeyCode.W) /*|| Input.mousePosition.y > this.camera.pixelHeight - this.scrollBorder*/) 
		{
			this.camera.transform.Translate (new Vector2 (0, .1f));
		}
		if (Input.GetKey (KeyCode.A) /*|| Input.mousePosition.x < this.scrollBorder*/) 
		{
			this.camera.transform.Translate (new Vector2 (-.1f, 0));
		}
		if (Input.GetKey (KeyCode.S)/* || Input.mousePosition.y < this.scrollBorder*/) 
		{
			this.camera.transform.Translate (new Vector2 (0, -.1f));
		}
		if (Input.GetKey (KeyCode.D)/* || Input.mousePosition.x > this.camera.pixelWidth - this.scrollBorder*/) 
		{
			this.camera.transform.Translate (new Vector2 (.1f, 0));
		}
	}
}
