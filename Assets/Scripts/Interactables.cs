using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public enum InteractableTypes
    {
        Message = 0,
        Fuel = 1,
        Chronofluid = 2
    }

    [SerializeField] InteractableTypes interactableType = InteractableTypes.Message;
    [SerializeField] string messageToDisplay = "";
    [SerializeField] float timeBeforeClearingMessage = 5f;
    [SerializeField] AudioClip messageClip;

    AudioSource audioSource;
    MessageHandler statsHandler;

    private void Start() {
        statsHandler = FindObjectOfType<MessageHandler>();
        audioSource = FindObjectOfType<AudioSource>();
    }
    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            switch (interactableType) {
                case InteractableTypes.Message:
                    statsHandler.UpdateMessage(messageToDisplay, timeBeforeClearingMessage);
                    if (messageClip) {
                        audioSource.PlayOneShot(messageClip);
                    }
                    break;
                case InteractableTypes.Fuel:
                    // TODO: NOT IMPLEMENTED
                    break;
                case InteractableTypes.Chronofluid:
                    // TODO: NOT IMPLEMENTED
                    break;
            }
        }
    }
}
