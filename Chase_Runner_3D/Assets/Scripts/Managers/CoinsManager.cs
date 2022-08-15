using TMPro;
using UnityEngine;


public class CoinsManager : MonoBehaviour
{ 
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI curLevelCoinsText;


    private int _curCoins;

    private int _curLevelCoin;
    private void Awake()
    {
        CheckForCollectedCoins();
    }

    private void Start()
    {
        _curLevelCoin = 0;
        EventsManager.OnCoinPickUp += IncrementCoin;
        EventsManager.OnGameLose += ShowCurLevelCoins;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("CollectedCoins",_curCoins);
        EventsManager.OnCoinPickUp -= IncrementCoin;
        EventsManager.OnGameLose -= ShowCurLevelCoins;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("CollectedCoins",_curCoins);
    }

    private void CheckForCollectedCoins()
    {
        _curCoins = 0;
        if (PlayerPrefs.HasKey("CollectedCoins")) 
            _curCoins = PlayerPrefs.GetInt("CollectedCoins"); 
        else
            PlayerPrefs.SetInt("CollectedCoins",_curCoins);

        coinsText.text = _curCoins.ToString();
    }
    
    #region Event Callbacks

    private void IncrementCoin()
    {
        _curCoins++;
        _curLevelCoin++;
        coinsText.text = _curCoins.ToString();
    }
    private void ShowCurLevelCoins()
    {
        
        curLevelCoinsText.text = "+" + _curLevelCoin;
    }

    #endregion
}
