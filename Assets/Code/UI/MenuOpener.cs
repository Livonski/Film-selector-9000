using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    public void cycleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}
