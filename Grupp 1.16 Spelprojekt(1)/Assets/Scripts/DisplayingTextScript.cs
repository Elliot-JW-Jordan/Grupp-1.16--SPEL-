using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DisplayingTextScript : MonoBehaviour
{


    [Header("Displaying Text settings")]
    public TextMeshProUGUI infoText;
    public float defualtDisplayTime = 2f; // längden på tiden som en text visas

    // Skappar en Kö för saker som texten ska visa 
    private Queue<Message> messageQueue = new Queue<Message>();

    //bool för att indikera om kön "prosseseras"
    private bool isProcessingQueue = false;
          //skapar en 'container' för att lagra 'medelanden' och dess längd som den ska visas i
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
        // säkerställer att texten börjar som tomm
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

    //kKall på denna meTOd från externa klasser eller metodesr
    public void DisplayMessage(string message, float displayDurationn = -1f)
    {
        if (this == null)
        {
            return;
        }
        // ifall den extärna metod/klassen inte angede någon tid, använd 'default'
        if (displayDurationn <= 0)
        {
            displayDurationn = defualtDisplayTime;
        }
        //det nya medelandet 'Enqueue'
        messageQueue.Enqueue(new Message(message, displayDurationn));
                  // ifall koden inte redan processerar medelanden påbörja karantännu!
                  if (!isProcessingQueue)
        {
            StartCoroutine(ProcessQueue());
        }
    }
    //efter koden has bvisat ett medelande, töms texten innan nästa medelande
    private IEnumerator ProcessQueue()
    {
        isProcessingQueue = true;

        //ett medeladne åt gågnen
        while (messageQueue.Count > 0)
        {
            Message currentMessage = messageQueue.Dequeue();

            // visa den nuvarande medelandet
            if (infoText != null)
            {
                infoText.text = currentMessage.text;
            }

            //vänta 
            yield return new WaitForSeconds(currentMessage.durationn);

            //Töm texten 
            if (infoText != null)
            {
                infoText.text = "";
            }
            // spelet väntar mellan att visa upp medelanden
            yield return new WaitForSeconds(0.5f);
        }
        isProcessingQueue = false;
    }
   
}
