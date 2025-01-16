using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
 
    public static CurrencyManager Instance { get; private set; }
    public int CurrentCurrency { get; private set; }
    

    public event Action<int> OnCurrencyChanged;


private void Awake()
    {
        //fr att säkerställa att det bara kna finnas en instans i taget.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
     public void AddCurrency(int amount)
    {
        if(amount < 0 )
        {
            Debug.LogWarning("You can not add a negative amount of currency");
            return;
        }
        CurrentCurrency += amount;
        CurrentCurrency = Mathf.Max(CurrentCurrency, 0); // så att CurrentCurrency aldring kan komma under noll.
        OnCurrencyChanged?.Invoke(CurrentCurrency);
        Debug.Log($"Added {amount} currency. New Total: {CurrentCurrency}");
    }

    public bool TrySpendCurrency(int amount)
    {
        if (CurrentCurrency >= amount)
        {
            AddCurrency(-amount); 
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
