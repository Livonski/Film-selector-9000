using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinResultDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Dropdown dropdown;
    public int ID { get; private set; }
    public Film film { get; private set; }

    public void ShowRollResult(Film film)
    {
        gameObject.SetActive(true);
        FindObjectOfType<FilmManager>().MarkFilm(film.ID, FilmStatus.inProgress);
        text.SetText(film.name);
        image.sprite = film.picture;
        ID = film.ID;
        dropdown.value = ((int)film.status) - 1;
        this.film = film;
    }
}
