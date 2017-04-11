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
	string npcType;

	int maxData;
	int showData;
	string [] data;

	//simple gui to show read data
	public void OnGUI()
	{
		// ensures I don't try to show data I don't have
		if(showData<maxData)
		{
			GUI.Label(new Rect(0,0,200,20), npcType+":"+npcName);
			GUI.Label(new Rect(0,20,200,100), data[showData]);
			if(Input.GetKeyUp("space"))
			{
				// goto next
				showData++;
				// wrap
				if(showData>=maxData)
					showData=0;
			}
		}
	}

	public void Start()
	{
		// initialise data
		maxData = 0;
		showData = 0;
		npcName = "unset";
		npcName = "unset";
		data = null;

		//readxml from chat.xml in project folder (Same folder where Assets and Library are in the Editor)
		XmlReader reader = XmlReader.Create("chat.xml");
		//while there is data read it
		while(reader.Read())
		{
			//when you find a npc tag do this
			if(reader.IsStartElement("npc"))
			{
				// get attributes from npc tag
				npcName=reader.GetAttribute("name");
				npcType = reader.GetAttribute("npcType");
				maxData = Int32.Parse(reader.GetAttribute("entries"));

				//allocate string pointer array
				data = new string[maxData];

				//read speach elements (showdata is used instead of having a new int I reset it later)
				for(showData = 0;showData<maxData;showData++)
				{
					reader.Read();
					if(reader.IsStartElement("speach"))
					{
						//fill strings
						data[showData] = reader.ReadString();
					}
				}
				//reset showData index
				showData=0;
			}
		}
}
}