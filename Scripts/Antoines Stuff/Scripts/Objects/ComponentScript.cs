using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is used as a sort of inventory for the pick ups for the communication system for end game
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class ComponentScript : MonoBehaviour, IInteractable
{
    // This is called when the player interacts with the pickup
    public void Interact(GameObject player)
    {
        player.GetComponent<FPSController>().PickUpRadioPiece();
        Destroy(this.gameObject);
    }

    // This tells the player what they need to do to pick the component up
    public void InteractText(Text interactionText)
    {
        interactionText.text = "Press 'E' to pick up";
    }

}
