using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    DisplayingTextScript displaying;
 
    public static CurrencyManager Instance { get; private set; }
    public int CurrentCurrency { get; private set; }
    

    public event Action<int> OnCurrencyChanged;


private void Awake()
    {
        displaying = FindObjectOfType<DisplayingTextScript>();
        //fr att säkerställa att det bara kna finnas en instans i taget.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
         // för nu   DontDestroyOnLoad(gameObject); //help
        }
    }
     public void AddCurrency(int amount)
    {
        if(amount < 0 )
        {
            Debug.LogWarning("You can not add a negative amount of currency");
            return;
        }
        Debug.Log($"Adding currency: {amount}. currency before ; {CurrentCurrency}");
        CurrentCurrency += amount;
      //  CurrentCurrency = Mathf.Max(CurrentCurrency, 0); // så att CurrentCurrency aldring kan komma under noll.
        Debug.Log($" currency after ; {CurrentCurrency}"); //debugg
        OnCurrencyChanged?.Invoke(CurrentCurrency);
        Debug.Log($"Added {amount} currency. New Total: {CurrentCurrency}");
        displaying.DisplayMessage($"+{amount}", 1f);
        
    }

    public bool TrySpendCurrency(int amount)
    {
        if (CurrentCurrency >= amount)
        {
            Debug.Log($"Trying to spend {amount} currecny. Current {CurrentCurrency}");
            CurrentCurrency -= amount;
            CurrentCurrency = Mathf.Max(CurrentCurrency, 0); // så att CurrentCurrency aldring kan komma under noll.
                                                             // AddCurrency(-amount); // försöker ta bort ifån splearens summa
            Debug.Log($"Added {amount} currency. New Total: {CurrentCurrency}");
            Debug.Log($"Spent {amount} currency, New total : {CurrentCurrency}");
            return true;
        }
        return false;
    }

    public void SetCurrency(int amount)
    {
        CurrentCurrency = Mathf.Max(amount, 0);
        OnCurrencyChanged?.Invoke(CurrentCurrency);
    }


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
