using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilmDisplayManager : MonoBehaviour
{
    [SerializeField] private FilmManager filmManager;
    [SerializeField] private GameObject displayPrefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private int offset;

    [SerializeField] private List<FilmDisplay> filmDisplays;

    private void Start()
    {
        filmDisplays = new List<FilmDisplay>();
    }

    private void debugInfo()
    {
        if (displayPrefab != null)
        {
            Debug.Log("Current film displays");
            foreach (FilmDisplay display in filmDisplays)
            {
                Debug.Log($"{display.ID} {display.gameObject.name}");
            }
        }
        else
        {
            Debug.Log("Display is empty");
        }
    }

    public void AddDisplayData(Film film)
    {
        GameObject newDisplay = Instantiate(displayPrefab, transform.position, Quaternion.identity, parent.transform);
        newDisplay.name = film.name;

        FilmDisplay filmDisplay = newDisplay.GetComponent<FilmDisplay>();
        filmDisplay.InstantiateDisplay(film);
        filmDisplays.Add(filmDisplay);

        debugInfo();
    }

    public void RemoveDisplayData(int ID)
    {
        FilmDisplay displayToRemove = filmDisplays[0];
        foreach (FilmDisplay display in filmDisplays)
        {
            if (display.ID == ID) 
                displayToRemove = display;
        }
        filmDisplays.Remove(displayToRemove);
        debugInfo();
    }

    public void EditDisplayData(int ID, Film film)
    {
        foreach (FilmDisplay display in filmDisplays)
        {
            if (display.ID == ID)
                display.InstantiateDisplay(film);
        }
        debugInfo();
    }

    public void UpdateDisplayData()
    {
        foreach(FilmDisplay display in filmDisplays)
        {
            Film film = filmManager.GetFilm(display.ID);
            display.InstantiateDisplay(film);
        }
    }

    public void ClearDisplay()
    {
        if (filmDisplays.Count > 0)
        {
            FilmDisplay[] displays = filmDisplays.ToArray();
            for (int i = displays.Length - 1; i >= 0; i--)
            {
                Destroy(displays[i].gameObject);
                debugInfo();
            }
            filmDisplays.Clear();
        }
    }
}
