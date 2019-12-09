using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class GUIHandler : MonoBehaviour
{
    public GameObject panelObject;
    public GameObject sporesEffect;
    public string sceneName;
    private Animator transitionAnim;
    bool notEnded = true;

    private void Start() {
        transitionAnim = panelObject.GetComponent<Animator>();
    }

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
        panelObject.SetActive(true);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");

        //TODO: Instead of instantly setting the spores effect
        //      when the Start button is pressed, it would be
        //      better if the particles fade away alongside
        //      the screen. For now, this works for the transition.
        sporesEffect.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    // Quits the application for the user
    // For this to work must be playing with execution file
    public void exit()
    {
        Application.Quit();
    }
}

