using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class GUIHandler : MonoBehaviour
{
    bool notEnded = true;

    /* for when we want the game to end we can load
     * game over scene with button that leads back to
     * main menu comment out for now
    public void over()
    {
        if (notEnded == true)
        {
            SceneManager.LoadScene("GameOver");
            notEnded = false;
        }

    }

    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }*/

    // Redirects user to game scene to play
    public void start()
    {
        SceneManager.LoadScene("Zone1");
    }

    // Quits the application for the user
    // For this to work must be playing with execution file
    public void exit()
    {
        Application.Quit();
    }
}

