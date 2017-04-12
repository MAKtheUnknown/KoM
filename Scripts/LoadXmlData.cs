using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class LoadXmlData : MonoBehaviour 
{
	string npcName;

	int maxData;
	int npcTalk;
	int npcNum;
	int showData;
	string [] data;
	string [] NpcName;
	//simple gui to show read data
	public void OnGUI()
	{
		// ensures I don't try to show data I don't have
		if(showData<maxData)
		{
			GUI.Label(new Rect(0,0,200,20), NpcName[showData]);
			GUI.Label(new Rect(0,20,200,100), data[showData]);
			if(GUI.Button(new Rect(0,120,200,20),"Next"))
			{
				Debug.Log(showData);
				Debug.Log(data[showData]);
				Debug.Log(NpcName[showData]);
				// wrap
				if(showData == maxData - 1)
					showData=0;
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
XmlReader reader = XmlReader.Create("chat.xml");
		//while there is data read it
		while(reader.Read())
		{
			if (reader.IsStartElement("npcs"))
			{
			maxData = Int32.Parse(reader.GetAttribute("totalentries"));
			//Debug.Log(maxData);
			//allocate string pointer array
			data = new string[maxData];
			NpcName = new string[maxData];
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
	NpcName[0] = "";
	NpcName[1] = "Lucius";
	NpcName[2] = "MC";
	NpcName[3] = "Lucius";
						showData=0;
	}
}