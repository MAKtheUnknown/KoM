using UnityEngine;
using System.Collections;

public class Zooming : MonoBehaviour 
{
	Camera camera;


	// Use this for initialization
	void Start () 
	{
		this.camera = this.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if (Input.GetAxis ("Mouse ScrollWheel")>0 || Input.GetKey(KeyCode.E)) 
		{
			this.camera.orthographicSize+=.05f;
		}
		if (Input.GetAxis ("Mouse ScrollWheel")<0 || Input.GetKey(KeyCode.Q)) 
		{
			this.camera.orthographicSize-=.05f;
		}
		if (Input.GetKey (KeyCode.W)) 
		{
			this.camera.transform.Translate (new Vector2 (0, .1f));
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			this.camera.transform.Translate (new Vector2 (-.1f, 0));
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			this.camera.transform.Translate (new Vector2 (0, -.1f));
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			this.camera.transform.Translate (new Vector2 (.1f, 0));
		}
	}
}
