using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonbehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartMenu()
    {
        SceneManager.LoadScene("Open Screen");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
        
    }
}