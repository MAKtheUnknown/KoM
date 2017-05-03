using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MultiplayerButtonClick : MonoBehaviour {

    public void Click()
    {
		String[] levels ={"Map 1", "Multiplayer Map 2"};
		int chance = UnityEngine.Random.Range(0,levels.Length-1);
        SceneManager.LoadScene(levels[chance]);
    }
}
