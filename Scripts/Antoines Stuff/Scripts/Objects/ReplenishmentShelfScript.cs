using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used for shelf pick ups which replenish food and drink of player
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class ReplenishmentShelfScript : MonoBehaviour, IInteractable {

    [SerializeField] private float respawnTime = 30;
    [SerializeField] private GameObject replenishmentObject;
    private float timer;

    // Use this for initialization
    void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0 && replenishmentObject != null)
            replenishmentObject.SetActive(true);
	}

    // This gives the player hunger and thirst satisfaction by a random amount between 0 and 15
    public void Interact(GameObject player)
    {
        if (timer <= 0)
        {
            player.GetComponent<FPSController>().Thirst += UnityEngine.Random.value * 15;
            player.GetComponent<FPSController>().Hunger += UnityEngine.Random.value * 15;
            timer = respawnTime;

            // This resets the replenishment game objects in shelves
            if (replenishmentObject != null)
                replenishmentObject.SetActive(false);

            // Add in UI text to show how much player gained
        }
    }

    // This is used to tell the player what to do when they look at the shelves to pick up food and drink
    public void InteractText(Text interactionText)
    {
        if (timer <= 0)
            interactionText.text = "Press 'E' to interact";
    }

}
