using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public int startingAmmount;
    public GameObject Enemy;
    public GameObject Boss1;
    public TextMeshProUGUI WaveText;
    public int CurrentWave;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        CurrentWave = startingAmmount;
        player = FindObjectOfType<Player>();

        StartWave();
    }

    public void CheckIfAllDied()
    {
        var enemies = FindObjectsOfType<Enemy>();
        int numOfEnemies = 0;

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
        if (CurrentWave % 5 == 0)
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
        }
        WaveText.SetText("Wave " + CurrentWave);
        CurrentWave++;
    }
}
