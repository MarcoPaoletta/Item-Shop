using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int moneyCount;

    void Start()
    {
        SetMoneyCountText();
    }

    void Update()
    {
        CheckInputs(); 
        SetMoneyCountText();
    }

    void SetMoneyCountText()
    {
        GameObject.Find("MoneyCountText").GetComponent<Text>().text = LoadMoneyCount() + "$";   
    }

    public static void SaveMoneyCount()
    {
        PlayerPrefs.SetInt("MoneyCountText", moneyCount);
    }

    public static int LoadMoneyCount()
    {
        moneyCount = PlayerPrefs.GetInt("MoneyCountText");
        return moneyCount;
    }

    void CheckInputs()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            GoToGameScene();
        }
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
