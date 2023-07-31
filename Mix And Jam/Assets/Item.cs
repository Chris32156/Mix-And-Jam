using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public TextMeshProUGUI priceText;

    public string Name;
    public bool Bought = false;
    public int price = 250;

    Coins coins;
    PlayerSkin playerSkin;

    // Start is called before the first frame update
    void Start()
    {
        coins = FindObjectOfType<Coins>();
        playerSkin = FindObjectOfType<PlayerSkin>();

        if (!Bought)
        {
            Bought = PlayerPrefs.GetInt(Name, 0) == 1;
        }

        if (Bought)
        {
            priceText.SetText("Bought");
        }
        else
        {
            priceText.SetText(price.ToString());
        }
    }

    public void Button()
    {
        if (!Bought && coins.NumberOfCoins >= price)
        {
            coins.NumberOfCoins -= price;
            coins.InitalizeValues();
            priceText.SetText("Bought");
            PlayerPrefs.SetInt(Name, 1);
            Bought = true;
        }
        if (Bought)
        {
            PlayerPrefs.SetString("Logo", Name);
            playerSkin.LoadShip();
        }
    }

    public void ColorButton(string color)
    {
        PlayerPrefs.SetString("Color", color);
        playerSkin.LoadShip();
    }

    public void FlagColor(string color)
    {
        if (!Bought && coins.NumberOfCoins >= price)
        {
            coins.NumberOfCoins -= price;
            coins.InitalizeValues();
            priceText.SetText("Bought");
            PlayerPrefs.SetInt(Name, 1);
            Bought = true;
        }
        if (Bought)
        {
            PlayerPrefs.SetString("FlagColor", color);
            playerSkin.LoadShip();
        }
    }

    public void HullColor(string color)
    {
        if (!Bought && coins.NumberOfCoins >= price)
        {
            coins.NumberOfCoins -= price;
            coins.InitalizeValues();
            priceText.SetText("Bought");
            PlayerPrefs.SetInt(Name, 1);
            Bought = true;
        }
        if (Bought)
        {
            PlayerPrefs.SetString("HullColor", color);
            playerSkin.LoadShip();
        }
    }
}
