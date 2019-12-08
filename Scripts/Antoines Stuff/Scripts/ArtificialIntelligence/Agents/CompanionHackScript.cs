using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class is used when hacking the companion
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class CompanionHackScript : MonoBehaviour, IInteractable {

    private bool m_isHacked = false;
    private float m_timer = 0;
    private CompanionAgent myCompanion;

    // Serialised field so that designers can change their values easily
    [SerializeField] private Texture enemyTexture;
    [SerializeField] private float hackTime = 5;

    // This functions initialises the companion state
    void Start()
    {
        myCompanion = GetComponent<CompanionAgent>();
        myCompanion.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Renderer>().material.SetTexture("_EmissionMap", enemyTexture);
    }

    // This updates the companion when the player interacts with it
    public void Interact(GameObject player)
    {
        if (!m_isHacked && myCompanion != null)
        {
            if (player.GetComponent<FPSController>().Interacting == false)
                m_timer = 0;
            else
                m_timer += Time.deltaTime;

            if (m_timer >= hackTime)
            {
                m_isHacked = true;
                myCompanion.enabled = true;
            }
        }
    }

    // This shows the text telling the player what to do to hack the companion
    public void InteractText(Text interactionText)
    {
        if (!m_isHacked)
        {
            interactionText.text = "Hold 'E' to hack";
        }
        else
        {
            interactionText.text = "";
        }
    }


}
