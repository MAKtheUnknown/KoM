using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CreditsButtonClick : MonoBehaviour {

		public void Click()
		{
			SceneManager.LoadScene("Credits");
		}
	}
