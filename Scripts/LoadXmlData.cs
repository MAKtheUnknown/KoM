using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text;
using System.Xml;
using System.IO;

public class LoadXmlData : MonoBehaviour 
{
	public GUIStyle style;
	public GUIStyle nameStyle;
	public string nextScene;
	string npcName;

	int maxData;
	int npcTalk;
	int npcNum;
	int showData;
	string [] data;
	public string [] NpcName;
	public string Source;
	//simple gui to show read data
	public void OnGUI()
	{
		style.wordWrap = true;
		style.fontSize = 14;
		style.normal.textColor = Color.black;
		style.normal.background = new Texture2D(Screen.width, Screen.height-80);
		nameStyle.fontSize = 16;
		nameStyle.normal.textColor = Color.yellow;
		// ensures I don't try to show data I don't have
		if(showData<maxData)
		{
			GUI.Label(new Rect(100,300,500,20), NpcName[showData],nameStyle);
			GUI.Box(new Rect(100,400,550,80), data[showData],style);

			if(GUI.Button(new Rect(100 ,500,100,20),"Next"))
			{
				//Debug.Log(showData);
				//Debug.Log(data[showData]);
				//Debug.Log(NpcName[showData]);
				// wrap
				if (showData == maxData - 1) {
					SceneManager.LoadScene(nextScene);
				}
					else 
				showData = (showData +1);
				
			    
			}
		}
	}
	public void Start()
	{
		// initialise data
		maxData = 0;
		npcTalk = 0;
		showData = 0;
		npcName = "unset";
		data = null;

		//readxml from chat.xml in project folder (Same folder where Assets and Library are in the Editor)
XmlReader reader = XmlReader.Create(Source);
		//while there is data read it
		while(reader.Read())
		{
			if (reader.IsStartElement("npcs"))
			{
			maxData = Int32.Parse(reader.GetAttribute("totalentries"));
			//Debug.Log(maxData);
			//allocate string pointer array
			data = new string[maxData];
			//NpcName = new string[maxData];
			}
			//when you find a npc tag do this
			if(reader.IsStartElement("npc"))
			{
				// get attributes from npc tag
				//NpcName[npcNum]=reader.GetAttribute("name");
				//npcNum++;
				//Debug.Log(reader.GetAttribute("name"));
				npcTalk=Int32.Parse(reader.GetAttribute("entries"));
				
				//read speach elements (showdata is used instead of having a new int I reset it later)
				for(int x = 0;x<maxData;x++)
				{
					reader.Read();
					if(reader.IsStartElement("speach"))
					{
						//fill strings
						data[showData] = reader.ReadString();
						if (showData < maxData -1)
						showData++;
						//Debug.Log(data[showData]);
					}
				}
				//reset showData index

			}
		  
		}

						showData=0;
	}
}