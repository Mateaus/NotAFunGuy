using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterMovement playerMovement = other.GetComponent<CharacterMovement>();
            playerMovement.isInTransition = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            CharacterMovement playerMovement = other.GetComponent<CharacterMovement>();
            playerMovement.isInTransition = false;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
