using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FilmManager : MonoBehaviour
{
    [SerializeField] private List<Film> films = new List<Film>();
    [SerializeField] private FilmDisplayManager displayManager;

    private int maxID = 0;
    public void OnFilmsLoaded(List<Film> loadedFilms)
    {
        ClearFilms();

        foreach (Film film in loadedFilms)
        {
            Debug.Log($"adding film {film.ID} {film.name} with status {film.status}");
            AddFilm(film);
            maxID = Mathf.Max(film.ID + 1, maxID);
        }

        //Debug
        Debug.Log("Current films");
        foreach (Film film in films)
        {
            Debug.Log($"{film.ID} {film.name} with status {film.status}");
        }
    }

    public void ClearFilms()
    {
        displayManager.ClearDisplay();
        films.Clear();
    }

    public void AddFilm(string name, Sprite picture)
    {
        Film newFilm = new Film(maxID, name, picture);
        maxID++;
        films.Add(newFilm);
        displayManager.AddDisplayData(newFilm);
    }

    public void AddFilm(Film film)
    {
        films.Add(film);
        displayManager.AddDisplayData(film);
    }

    public void RemoveFilm(int ID)
    {
        Film filmToRemove = new Film();
        foreach (Film film in films)
        {
            if (film.ID == ID)
                filmToRemove = film;
        }
        films.Remove(filmToRemove);
        displayManager.RemoveDisplayData(ID);
    }

    public void ReplaceFilm(int ID, Film newFilm)
    {
        Film filmToRemove = new Film();
        foreach (Film film in films)
        {
            if (film.ID == ID)
                filmToRemove = film;
        }
        films.Remove(filmToRemove);
        films.Add(newFilm);
    }

    public void MarkFilm(int filmID, FilmStatus newStatus)
    {
        for (int i = 0; i < films.Count; i++)
        {
            Film film = films[i];
            if (film.ID == filmID)
            {
                Debug.Log("marking film");
                film.status = newStatus;
                films[i] = film;
            }
        }
        displayManager.UpdateDisplayData();
    }

    public void EditFilm(int ID, Film newFilmData)
    {
        ReplaceFilm(ID, newFilmData);
        displayManager.EditDisplayData(ID, newFilmData);
    }

    public List<Film> GetFilms()
    {
        return films;
    }

    public List<Film> GetFilms(List<FilmStatus> whitelist)
    {
        List<Film> output = new List<Film>();

        foreach (Film film in films)
        {
            if(whitelist.Contains(film.status))
                output.Add(film);
        }
        return output;
    }

    public Film GetFilm(int ID)
    {
        for (int i = 0; i < films.Count; i++)
        {
            Film film = films[i];
            if (film.ID == ID)
                return film;
        }
        return films[0];
    }
}

[System.Serializable]
public struct Film
{
    public int ID;
    public string name;
    public FilmStatus status;
    public Sprite picture;

    public Film(int ID, string name, Sprite picture)
    {
        this.ID = ID;
        this.name = name;
        status = FilmStatus.notWatched;
        this.picture = picture;
    }

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(ID);
        writer.Write(name);
        writer.Write(status.ToString());

        Texture2D texture = picture.texture;
        Rect rect = picture.rect;

        Texture2D spriteTexture = new Texture2D((int)rect.width, (int)rect.height);

        spriteTexture.SetPixels(texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
        spriteTexture.Apply();

        Vector2 pivot = picture.pivot;
        float pixelsPerUnit = picture.pixelsPerUnit;

        byte[] imageData = spriteTexture.EncodeToPNG();

        writer.Write(imageData.Length);
        writer.Write(imageData);

        writer.Write(pivot.x);
        writer.Write(pivot.y);
        writer.Write(pixelsPerUnit);
    }

    public static Film Deserialize(BinaryReader reader)
    {
        Film film = new Film();
        film.ID = reader.ReadInt32();
        film.name = reader.ReadString();
        film.status = (FilmStatus)Enum.Parse(typeof(FilmStatus), reader.ReadString());

        int imageDataLength = reader.ReadInt32();
        byte[] imageData = reader.ReadBytes(imageDataLength);

        float pivotX = reader.ReadSingle();
        float pivotY = reader.ReadSingle();
        float pixelsPerUnit = reader.ReadSingle();

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(pivotX / texture.width, pivotY / texture.height), pixelsPerUnit);

        film.picture = sprite;

        return film;
    }

}

public enum FilmStatus
{
    watched,
    inProgress,
    notWatched
}