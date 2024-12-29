using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private bool hasPlayedAnim = false;
    private UIManager uiManager;

    private void Awake() 
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            if (hasPlayedAnim) return;
            hasPlayedAnim = true;
            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<Animator>().SetTrigger("Appear");
            GetComponent<PlayerMovement>().movable = false;
            uiManager.ShowVictoryUI();
            Victory.instance.PlaySound();
        }
    }
}
