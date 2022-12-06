using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //Notice that both of the functions in this class are public. They 
    //need to be in order for UI Buttons to call them. 

    //To be called when the user hits "Play" on the main menu
    public void StartGame()
    {
        //NOTICE: using UnityEngine.SceneManagement; on line 4
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    //To be called when the user hits "Quit" on the main menu
    public void QuitGame()
    {
        Application.Quit();
    }
}
