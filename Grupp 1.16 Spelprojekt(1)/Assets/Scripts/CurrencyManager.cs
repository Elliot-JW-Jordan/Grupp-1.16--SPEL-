using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }
    private int playerTotalCurrency;

    public int PlayerTotalCurrency
    {
        get => playerTotalCurrency;
        private set
        {
            playerTotalCurrency = value;
            OnCurrencyChanged?.invoke(playerTotalCurrency);
        }
    }
    public delegate void CurrencyChanged(int newCurrency);
    public event CurrencyChanged OnCurrencyChanged;

    private void Awake()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
     public void AddCurrency(int amount)
    {
        playerTotalCurrency += amount;
    }
    public bool SpeendCurrency(int amount)
    {
        if (amount <= playerTotalCurrency)
        {
            playerTotalCurrency -= amount;
            return true;
        }
        return false;
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
