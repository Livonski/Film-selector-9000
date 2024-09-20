using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmEditorInvoker : MonoBehaviour
{
    [SerializeField] private FilmDisplay display;
    [SerializeField] private FilmEditor editor;
    [SerializeField] private int ID;

    private void Start()
    {
        ID = display.ID;
        editor = FindObjectOfType<FilmEditor>();
    }

    public void InvokeEditor()
    {
        editor.displayFilmInfo(ID);
    }
}
