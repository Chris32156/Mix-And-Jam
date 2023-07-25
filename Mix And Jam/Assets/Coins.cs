using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI AttackLevelText;
    public TextMeshProUGUI SpeedLevelText;
    public TextMeshProUGUI HealthLevelText;
    public TextMeshProUGUI AttackCoinText;
    public TextMeshProUGUI SpeedCoinText;
    public TextMeshProUGUI HealthCoinText;
    public TextMeshProUGUI AttackModifierText;
    public TextMeshProUGUI SpeedModifierText;
    public TextMeshProUGUI HealthModifierText;

    public TextMeshProUGUI SpeedUpText;
    public TextMeshProUGUI HealText;
    public TextMeshProUGUI SkullstormText;

    public int NumberOfCoins = 0;
    public int AttackLevel = 0;
    public int SpeedLevel = 0;
    public int HealthLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        AttackLevel = PlayerPrefs.GetInt("Attack Level", 1);
        SpeedLevel = PlayerPrefs.GetInt("Speed Level", 1);
        HealthLevel = PlayerPrefs.GetInt("Health Level", 1);

        CoinsText.SetText(NumberOfCoins.ToString());
        InitalizeValues();
    }

    public void InitalizeValues()
    {
        CoinsText.SetText(NumberOfCoins.ToString());

        AttackLevelText.SetText("LV " + AttackLevel.ToString());
        AttackCoinText.SetText((AttackLevel * 100).ToString());
        AttackModifierText.SetText((1 + 0.05 * (AttackLevel - 1)).ToString() + "x");

        SpeedLevelText.SetText("LV " + SpeedLevel.ToString());
        SpeedCoinText.SetText((SpeedLevel * 100).ToString());
        SpeedModifierText.SetText((1 + 0.05 * (SpeedLevel - 1)).ToString() + "x");

        HealthLevelText.SetText("LV " + HealthLevel.ToString());
        HealthCoinText.SetText((HealthLevel * 100).ToString());
        HealthModifierText.SetText((5 + HealthLevel - 1).ToString() + " Hits");

        if(PlayerPrefs.GetInt("SpeedUpUnlock", 0) == 1)
        {
            SpeedUpText.SetText("Bought");
        }
        if (PlayerPrefs.GetInt("HealUnlock", 0) == 1)
        {
            HealText.SetText("Bought");
        }
        if (PlayerPrefs.GetInt("SkullstormUnlock", 0) == 1)
        {
            SkullstormText.SetText("Bought");
        }
    }

    public void AttackSpeedUpgrade()
    {
        if (NumberOfCoins >= AttackLevel * 100)
        {
            NumberOfCoins -= AttackLevel * 100;
            AttackLevel++;
            PlayerPrefs.SetInt("Attack Level", AttackLevel);
            InitalizeValues();
        }
    }

    public void SpeedUpgrade()
    {
        if (NumberOfCoins >= SpeedLevel * 100)
        {
            NumberOfCoins -= SpeedLevel * 100;
            SpeedLevel++;
            PlayerPrefs.SetInt("Speed Level", SpeedLevel);
            InitalizeValues();
        }
    }

    public void HealthUpgrade()
    {
        if (NumberOfCoins >= HealthLevel * 100)
        {
            NumberOfCoins -= HealthLevel * 100;
            HealthLevel++;
            PlayerPrefs.SetInt("Health Level", HealthLevel);
            InitalizeValues();
        }
    }

    public void SpeedUpUnlock()
    {
        if (NumberOfCoins >= 250 && PlayerPrefs.GetInt("SpeedUpUnlock", 0) == 0)
        {
            NumberOfCoins -= 250;
            PlayerPrefs.SetInt("SpeedUpUnlock", 1);
            InitalizeValues();
        }
    }

    public void HealUnlock()
    {
        if (NumberOfCoins >= 750 && PlayerPrefs.GetInt("HealUnlock", 0) == 0)
        {
            NumberOfCoins -= 750;
            PlayerPrefs.SetInt("HealUnlock", 1);
            InitalizeValues();
        }
    }

    public void SkullstormUnlock()
    {
        if (NumberOfCoins >= 1500 && PlayerPrefs.GetInt("SkullstormUnlock", 0) == 0)
        {
            NumberOfCoins -= 1500;
            PlayerPrefs.SetInt("SkullstormUnlock", 1);
            InitalizeValues();
        }
    }
}
