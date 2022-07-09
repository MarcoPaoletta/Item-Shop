using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Header("Item")]
    public Image skin;

    [Header("BuyButton")]
    public Button buyButton;
    public Image buyButtonImage;
    public Text buyButtonText;

    private string itemPrice;
    private int itemSelectedIndex;

    private Color itemBoughtButtonColor;
    private Color itemSelectedButtonColor;

    #region Update()

    void Update()
    {
        SetSelectedItem();
    }

    void SetSelectedItem()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject item = GameObject.Find("Item (" + i.ToString() + ")");
            Text itemBuyButtonText = item.transform.Find("BuyButton").GetComponentInChildren<Text>();
            Image skin = item.transform.Find("Skin").GetComponent<Image>();

            if(itemBuyButtonText.text == "SELECTED")
            {
                Player.spriteRendererColor = skin.color;
            }
        }
    }

    #endregion

    #region Start()

    void Start()
    {
        SetItemPrice(); 
        SetColors();
        StylizeBoughtItemButton();
        StylizeSelectedItemButton();  
    }

    void SetItemPrice()
    {
        itemPrice = buyButtonText.text.Replace("$", ""); 
    }

    void SetColors()
    {
        ColorUtility.TryParseHtmlString("#FF6A84", out itemSelectedButtonColor);
        ColorUtility.TryParseHtmlString("#00FF0A", out itemBoughtButtonColor);
    }

    void StylizeSelectedItemButton()
    {
        itemSelectedIndex = PlayerPrefs.GetInt("ItemSelectedIndex");
        GameObject selectedItem = GameObject.Find("Item (" + itemSelectedIndex.ToString() + ")"); 
        selectedItem.transform.Find("BuyButton").GetComponent<Image>().color = itemSelectedButtonColor;
        selectedItem.GetComponentInChildren<Text>().text = "SELECTED";
    }

    void StylizeBoughtItemButton() 
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject item = GameObject.Find("Item (" + i.ToString() + ")");
            string itemIndex = item.name.Replace("Item", "").Replace("(", "").Replace(")", "");

            if(ItemBought(itemIndex))
            {
                GameObject buyButton = item.transform.Find("BuyButton").gameObject;
                buyButton.GetComponentInChildren<Text>().text = "BOUGHT";
                buyButton.GetComponent<Image>().color = itemBoughtButtonColor;
            }
        }
    }

    bool ItemBought(string itemIndex)
    {
        if(itemIndex == PlayerPrefs.GetString("ItemBoughtIndex" + itemIndex))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    #endregion

    # region OnBuyButtonClicked()

    public void OnBuyButtonClicked()
    {
        if(!ItemBoughtOrSelected() && EnoughMoney())
        {
            BuyItem();
            SaveBoughtItems();
        }

        if(ItemBoughtOrSelected())
        {
            SelectItem();
            SetItemSelectedIndex();
        }

        SetButtonsColor();
        SetSkinForThePLayer();
    }

    bool ItemBoughtOrSelected()
    {
        if(buyButtonText.text == "BOUGHT" || buyButtonText.text == "SELECTED")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool EnoughMoney()
    {
        if(GameManager.moneyCount >= int.Parse(itemPrice))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void BuyItem()
    { 
        buyButton.image.color = itemBoughtButtonColor;
        buyButtonText.text = "BOUGHT";
        GameManager.moneyCount -= int.Parse(itemPrice);
    }

    void SaveBoughtItems()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject item = GameObject.Find("Item (" + i.ToString() + ")");
            Text itemBuyButtonText = item.transform.Find("BuyButton").GetComponentInChildren<Text>();

            if(itemBuyButtonText.text == "BOUGHT" || itemBuyButtonText.text == "SELECTED")
            {
                string itemIndex = itemBuyButtonText.transform.parent.transform.parent.name.Replace("Item", "").Replace("(", "").Replace(")", "");
                PlayerPrefs.SetString("ItemBoughtIndex" + itemIndex, itemIndex);
            }
        }
    }

    void SelectItem()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject item = GameObject.Find("Item (" + i.ToString() + ")");
            Text itemBuyButtonText = item.transform.Find("BuyButton").GetComponentInChildren<Text>();

            if(itemBuyButtonText.text == "SELECTED")
            {
                itemBuyButtonText.text = "BOUGHT";
            }

            buyButtonText.text = "SELECTED";
        }
    }

    void SetItemSelectedIndex()
    {
        string itemIndex = buyButtonText.transform.parent.transform.parent.name.Replace("Item", "").Replace("(", "").Replace(")", "");
        itemSelectedIndex = int.Parse(itemIndex); 
        PlayerPrefs.SetInt("ItemSelectedIndex", itemSelectedIndex);
    }

    void SetButtonsColor()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject item = GameObject.Find("Item (" + i.ToString() + ")");
            Text itemBuyButtonText = item.transform.Find("BuyButton").GetComponentInChildren<Text>();
            Image itemBuyButtonImage = item.transform.Find("BuyButton").GetComponent<Image>();

            if(itemBuyButtonText.text == "SELECTED")
            {
                itemBuyButtonImage.color = itemSelectedButtonColor;
            }

            if(itemBuyButtonText.text == "BOUGHT")
            {
                itemBuyButtonImage.color = itemBoughtButtonColor;
            }
        }
    }

    void SetSkinForThePLayer()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject item = GameObject.Find("Item (" + i.ToString() + ")");
            Text itemBuyButtonText = item.transform.Find("BuyButton").GetComponentInChildren<Text>();
            Image skin = item.transform.Find("Skin").GetComponent<Image>();

            if(itemBuyButtonText.text == "SELECTED")
            {
                Player.spriteRendererColor = skin.color;
            }
        }
    }
    
    #endregion
}
