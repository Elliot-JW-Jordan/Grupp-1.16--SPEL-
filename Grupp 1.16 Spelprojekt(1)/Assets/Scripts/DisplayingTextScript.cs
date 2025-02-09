using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DisplayingTextScript : MonoBehaviour
{


    [Header("Displaying Text settings")]
    public TextMeshProUGUI infoText;
    public float defualtDisplayTime = 2f; // l�ngden p� tiden som en text visas

    // Skappar en K� f�r saker som texten ska visa 
    private Queue<Message> messageQueue = new Queue<Message>();

    //bool f�r att indikera om k�n "prosseseras"
    private bool isProcessingQueue = false;
          //skapar en 'container' f�r att lagra 'medelanden' och dess l�ngd som den ska visas i
          private class Message
    {
        public string text;
        public float durationn;
         public Message(string text, float durationn)
        {
            this.text = text;
            this.durationn = durationn;
        }
    }
    private void Awake()
    {
        Debug.Log("DisplayingTextScript was initialized  on" + gameObject.name + "s");
        // s�kerst�ller att texten b�rjar som tomm
        if(infoText != null)
        {
            infoText.text = "";
        }
        DontDestroyOnLoad(gameObject);
    }
    private void OnDestroy()
    {
        Debug.LogError("DisplayingTextScript was destroyed   on" + gameObject.name + "s");
    }

    //kKall p� denna meTOd fr�n externa klasser eller metodesr
    public void DisplayMessage(string message, float displayDurationn = -1f)
    {
        if (this == null)
        {
            return;
        }
        // ifall den ext�rna metod/klassen inte angede n�gon tid, anv�nd 'default'
        if (displayDurationn <= 0)
        {
            displayDurationn = defualtDisplayTime;
        }
        //det nya medelandet 'Enqueue'
        messageQueue.Enqueue(new Message(message, displayDurationn));
                  // ifall koden inte redan processerar medelanden p�b�rja karant�nnu!
                  if (!isProcessingQueue)
        {
            StartCoroutine(ProcessQueue());
        }
    }
    //efter koden has bvisat ett medelande, t�ms texten innan n�sta medelande
    private IEnumerator ProcessQueue()
    {
        isProcessingQueue = true;

        //ett medeladne �t g�gnen
        while (messageQueue.Count > 0)
        {
            Message currentMessage = messageQueue.Dequeue();

            // visa den nuvarande medelandet
            if (infoText != null)
            {
                infoText.text = currentMessage.text;
            }

            //v�nta 
            yield return new WaitForSeconds(currentMessage.durationn);

            //T�m texten 
            if (infoText != null)
            {
                infoText.text = "";
            }
            // spelet v�ntar mellan att visa upp medelanden
            yield return new WaitForSeconds(0.5f);
        }
        isProcessingQueue = false;
    }
   
}
