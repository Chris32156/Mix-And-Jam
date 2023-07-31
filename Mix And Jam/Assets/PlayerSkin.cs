using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public SpriteRenderer logo;
    public SpriteRenderer Flag;
    public SpriteRenderer Hull;
    public Sprite Grey;
    public Sprite regularShip;
    public Sprite Skull;
    public Sprite Sword;
    public Sprite Boom;
    public Sprite Shield;
    public Sprite Chest;

    // Start is called before the first frame update
    void Start()
    {
        LoadShip();
    }

    public void LoadShip()
    {
        // Logo
        string logoEquiped = PlayerPrefs.GetString("Logo");
        if (logoEquiped == "Skull")
        {
            logo.sprite = Skull;
        }
        else if(logoEquiped == "Sword")
        {
            logo.sprite = Sword;
        }
        else if (logoEquiped == "Boom")
        {
            logo.sprite = Boom;
        }
        else if (logoEquiped == "Shield")
        {
            logo.sprite = Shield;
        }
        else if (logoEquiped == "Chest")
        {
            logo.sprite = Chest;
        }
        else
        {
            logo.sprite = null;
        }

        // Color
        Color color;
        ColorUtility.TryParseHtmlString(PlayerPrefs.GetString("Color"), out color);
        logo.color = color;

        // Flag
        ColorUtility.TryParseHtmlString(PlayerPrefs.GetString("FlagColor"), out color);
        Flag.color = color;

        // Hull 
        if (PlayerPrefs.GetString("HullColor") == "Grey")
        {
            Hull.color = new Color(255, 255, 255, 100);
            Hull.sprite = Grey;
        }
        else
        {
            Hull.sprite = regularShip;
            ColorUtility.TryParseHtmlString(PlayerPrefs.GetString("HullColor"), out color);
            Hull.color = color;
        }
    }
}
