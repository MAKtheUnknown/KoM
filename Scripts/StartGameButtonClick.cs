using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGameButtonClick : MonoBehaviour {

    public void Click()
    {
        SceneManager.LoadScene("Level 1");
    }
}
