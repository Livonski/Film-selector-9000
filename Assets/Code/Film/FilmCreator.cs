using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilmCreator : MonoBehaviour
{
    [SerializeField] private FilmManager filmManager;
    [SerializeField] private ImageLoader imageLoader;

    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Image previewImage;

    public void CreateFilm()
    {
        string text = textMeshProUGUI.text;
        sprite = imageLoader.selectedImage;
        filmManager.AddFilm(text, sprite);
    }

    private void Update()
    {
        Sprite selectedImage = imageLoader.selectedImage;
        if (selectedImage != null)
        {
            previewImage.sprite = selectedImage;
        }
    }
}
