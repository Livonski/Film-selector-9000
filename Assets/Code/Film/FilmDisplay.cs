using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilmDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Dropdown dropdown;
    public int ID { get; private set; }
    public Film film { get; private set; }

    public void InstantiateDisplay(Film film)
    {
        text.SetText(film.name);
        image.sprite = film.picture;
        ID = film.ID;
        dropdown.value = ((int)film.status);
        this.film = film;
    }

    public void DeleteFilm()
    {
        FilmManager filmManager = FindObjectOfType<FilmManager>();
        filmManager.RemoveFilm(ID);
        Destroy(gameObject);
    }
}

[System.Serializable]
public struct FilmDisplayData
{
    public TextMeshProUGUI text;
    public Image image;
    public GameObject display;
    public int ID;

    public FilmDisplayData(TextMeshProUGUI text, Image image, GameObject display, int ID)
    {
        this.text = text;
        this.image = image;
        this.display = display;
        this.ID = ID;
    }
}
