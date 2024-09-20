using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FilmSaveLoader : MonoBehaviour
{
    [SerializeField] private FilmManager filmManager;
    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "films.dat");
        Debug.Log(filePath);
    }

    public void SaveFilms()
    {
        List<Film> films = filmManager.GetFilms();
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            writer.Write(films.Count);

            foreach (Film film in films)
            {
                Debug.Log(film.ID + "" + film.name);
                film.Serialize(writer);
            }
        }
    }

    public void LoadFilms()
    {
        List<Film> films = new List<Film>();
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("incorrect file path");
        }
        else
        { 
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    Film film = Film.Deserialize(reader);
                    films.Add(film);
                }
            }
           filmManager.OnFilmsLoaded(films);
        }
    }
}
