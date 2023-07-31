using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public int startingAmmount;
    public GameObject Enemy;
    public GameObject Boss1;
    public GameObject Boss2;
    public GameObject Boss3;
    public GameObject Chest;
    public TextMeshProUGUI WaveText;
    public int CurrentWave;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        startingAmmount = PlayerPrefs.GetInt("StartingAmmount", 1);
        CurrentWave = startingAmmount;
        player = FindObjectOfType<Player>();

        StartWave();
    }

    public void CheckIfAllDied()
    {
        var enemies = FindObjectsOfType<Enemy>();
        int numOfEnemies = 0;

        if (Random.Range(1, 100) < 5 && FindObjectsOfType<Chest>().Length == 0)
        {
            Instantiate(Chest);
        }

        foreach (Enemy Enemy in enemies)
        {
            if (!Enemy.isDead)
            {
                numOfEnemies++;
            }
        }

        if (numOfEnemies == 0)
        {
            player.AddGold(CurrentWave - 1);
            StartWave();
        }
    }

    void StartWave()
    {
        int boss1Spawns = 0;
        int boss2Spawns = 0;
        int boss3Spawns = 0;

        if (CurrentWave % 5 == 0)
        {
            boss1Spawns = CurrentWave / 5;
        }
        if (CurrentWave % 10 == 0)
        {
            boss2Spawns = CurrentWave / 10;
        }
        if (CurrentWave % 25 == 0)
        {
            boss3Spawns = CurrentWave / 25;
        }

        for (int i = 0; i < boss1Spawns; i++)
        {
            Instantiate(Boss1);
        }
        for (int i = 0; i < boss2Spawns; i++)
        {
            Instantiate(Boss2);
        }
        for (int i = 0; i < boss3Spawns; i++)
        {
            Instantiate(Boss3);
        }

        for (int i = 0; i < CurrentWave - boss1Spawns - boss2Spawns - boss3Spawns; i++)
        {
            Instantiate(Enemy);
        }

        /*if (CurrentWave % 5 == 0)
        {
            boss1Spawns = CurrentWave / 5;
            for (int i = 0; i < boss1Spawns; i++)
            {
                Instantiate(Boss1);
            }
            for (int i = 0; i < CurrentWave - boss1Spawns; i++)
            {
                Instantiate(Enemy);
            }
        }
        else
        {
            for (int i = 0; i < CurrentWave; i++)
            {
                Instantiate(Enemy);
            }
        } */
        WaveText.SetText("Wave " + CurrentWave);
        CurrentWave++;
    }
}
