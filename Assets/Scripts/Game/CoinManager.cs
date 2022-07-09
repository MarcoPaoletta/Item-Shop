using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public Text moneyCountText;

    void Start()
    {
        InstantiateCoins(20);        
    }

    void InstantiateCoins(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f)), Quaternion.identity);
        }
    }

    void Update()
    {
        UpdateMoneyCountText();
        GoToItemShop();
    }

    void UpdateMoneyCountText()
    {
        moneyCountText.text = GameManager.moneyCount.ToString() + "$";
    }

    void GoToItemShop()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene(0);
        }
    }

}
