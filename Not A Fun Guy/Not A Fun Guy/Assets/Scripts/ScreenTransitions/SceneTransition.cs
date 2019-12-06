using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterMovement playerMovement = other.GetComponent<CharacterMovement>();
            playerMovement.isInTransition = true;
            StartCoroutine(LoadScene(playerMovement));
        }
    }

    IEnumerator LoadScene(CharacterMovement playerMovement)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.0f);
        playerMovement.characterSpeed = 0.0f;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}

