using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilmEditor : MonoBehaviour
{
    [SerializeField] private FilmManager filmManager;
    [SerializeField] private ImageLoader imageLoader;

    [SerializeField] private TMP_InputField textMeshProInput;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [SerializeField] private TMP_Dropdown tmp_Dropdown;

    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject editorPanel;
    [SerializeField] private Image previewImage;

    [SerializeField] private int ID;

    public void displayFilmInfo(int id)
    {
        editorPanel.SetActive(true);
        ID = id;
        Film film = filmManager.GetFilm(ID);
        textMeshProInput.text = film.name;
        previewImage.sprite = film.picture;
        tmp_Dropdown.value = (int)film.status;
        Debug.Log((int)film.status);
    }

    public void editFilm()
    {
        string text = textMeshProUGUI.text;
        Film film = filmManager.GetFilm(ID);
        sprite = imageLoader.imageLoaded ? imageLoader.selectedImage : film.picture;
        Film newFilm = new Film(ID, text, sprite);
        newFilm.status = (FilmStatus)tmp_Dropdown.value;
        filmManager.EditFilm(ID, newFilm);
    }

    private void Update()
    {
        if (ID != 0 && filmManager.GetFilms().Count > 0)
        {
            Sprite selectedImage = imageLoader.selectedImage;
            Film film = filmManager.GetFilm(ID);
            previewImage.sprite = imageLoader.imageLoaded ? selectedImage : film.picture;
        }
    }
}
