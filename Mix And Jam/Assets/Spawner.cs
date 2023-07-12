using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public int startingAmmount;
    public GameObject Enemy;
    public TextMeshProUGUI WaveText;

    int CurrentWave;
    // Start is called before the first frame update
    void Start()
    {
        CurrentWave = startingAmmount;

        StartWave();
    }

    public void CheckIfAllDied()
    {
        var enemies = FindObjectsOfType<Enemy>();
        Debug.Log("A");
        Debug.Log("Enemy Length: " + enemies.Length.ToString());
        if(enemies.Length <= 1)
        {
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
