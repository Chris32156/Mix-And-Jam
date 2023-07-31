using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinCanvas : MonoBehaviour
{
    public GameObject HullCanvas;
    public GameObject FlagCanvas;
    public GameObject LogoCanvas;

    public void Hull()
    {
        HullCanvas.SetActive(true);
        FlagCanvas.SetActive(false);
        LogoCanvas.SetActive(false);
    }

    public void Flag()
    {
        HullCanvas.SetActive(false);
        FlagCanvas.SetActive(true);
        LogoCanvas.SetActive(false);
    }

    public void Logo()
    {
        HullCanvas.SetActive(false);
        FlagCanvas.SetActive(false);
        LogoCanvas.SetActive(true);
    }
}
