using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageHandler : MonoBehaviour
{
    [SerializeField] float defaultTimeBeforeClearingMessage = 5f;
    [SerializeField] string[] defaultMessages = {};
    TMP_Text displayText;
    int messageIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        displayText = GetComponent<TMP_Text>();
        ShowDefaultMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowDefaultMessage() {
        displayText.text = defaultMessages[messageIndex]; // Show the message at this index
        messageIndex++; // Go to the next message
        if (messageIndex < defaultMessages.Length){ // If message exists, call again with that one.
            Invoke("ShowDefaultMessage", defaultTimeBeforeClearingMessage);
        }
        
    }

    public void ClearMessage() {
        UpdateMessage("", 0f);
    }

    public void UpdateMessage(string message, float timeBeforeClearing = -1f) {
        if (timeBeforeClearing < -Mathf.Epsilon) {
            timeBeforeClearing = defaultTimeBeforeClearingMessage;
        }
        displayText.text = message;
        if (message != "") { // Only clear message if it is not empty
            Invoke("ClearMessage", timeBeforeClearing);
        }
    }
}
