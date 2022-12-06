using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    void Awake ()
    {
        //using GameObject.Find() is typically not advisable. 
        //It's slow, and prone to breaking if you change the name of the object.
        //However, for something like a main menu that never changes, we can search for a button by name.
        //In other cases, consider using tags (GameObject.FindGameObjectWithTag())
        Button button = GameObject.Find("PlayButton").GetComponent<Button>();
        
        //Button inherits from Selectable, which describes a UI component that can be selected with arrow keys or
        //a gamepad. Select the play button when we start so that the user can push enter or Xbox A / PS X / NS B
        //to play.
        button.Select();
        
    }

    //Notice that both of these functions are public. They 
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
        //This tells Unity it's time to close. 
        Application.Quit();
    }
}
