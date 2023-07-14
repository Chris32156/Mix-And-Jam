using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public int startingAmmount;
    public GameObject Enemy;
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
        for (int i = 0; i < CurrentWave; i++)
        {
            Instantiate(Enemy);
        }
        WaveText.SetText("Wave " + CurrentWave);
        CurrentWave++;
    }
}
