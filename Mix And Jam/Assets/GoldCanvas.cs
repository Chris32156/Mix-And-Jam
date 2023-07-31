using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCanvas : MonoBehaviour
{
    public GameObject goldCanvas;
    public GameObject chest;

    public void No()
    {
        chest = FindObjectOfType<Chest>().gameObject;
        Time.timeScale = 1;
        goldCanvas.SetActive(false);
        Destroy(chest);
    }
}
