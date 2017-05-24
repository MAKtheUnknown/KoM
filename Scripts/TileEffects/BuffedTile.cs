using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffedTile : MonoBehaviour {

	public Team source;
	public int turnsLeft;
	public TileAttributes subject;
	Material buffed;
	GameObject buffHighlighter;

	public void Start()
	{
		subject=GetComponentInParent<TileAttributes>();
		source=subject.map.teams.teams[0];
		turnsLeft=100;
		buffed=GetComponent<MeshRenderer>().material;
		
		buffHighlighter = Instantiate (subject.map.highlighter.possibleMovesHighlighter);
		buffHighlighter.GetComponent<Renderer>().material=buffed;
		buffHighlighter.transform.SetParent(subject.transform);
		RectTransform rt = buffHighlighter.GetComponent<RectTransform> ();
		rt.anchoredPosition = new Vector2 (0,0);
	}
	
	public void Update()
	{
		if(subject.containedCharacter!=null)
		{
			GiveBuff(subject.containedCharacter);
			GameObject.Destroy(buffHighlighter);
			GameObject.Destroy(this);
		}
	}
	
	public abstract void GiveBuff(CharacterCharacter c);	
}
