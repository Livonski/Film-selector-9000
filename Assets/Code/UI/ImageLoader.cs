using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleFileBrowser;
using System;

public class ImageLoader : MonoBehaviour
{
    //public Image displayImage;
    public Sprite selectedImage { get; private set; }
    public bool imageLoaded {  get; private set; }

    [SerializeField] private Sprite genericImage;

    public void OnButtonClick()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    public void clearSelectedImage()
    {
        selectedImage = genericImage;
        imageLoaded = false;
    }

    private IEnumerator ShowLoadDialogCoroutine()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png", ".jpeg"));
        FileBrowser.SetDefaultFilter(".png");

        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files);

        if (FileBrowser.Success)
        {
            string filePath = FileBrowser.Result[0];
            LoadImage(filePath);
        }
    }

    private void LoadImage(string filePath)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        selectedImage = sprite;
        imageLoaded = true;
    }
}

