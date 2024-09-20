using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpinningWheel : MonoBehaviour
{
    [SerializeField] private int facesNum;
    [SerializeField] private float rotationTime;
    [SerializeField] private FilmDisplay[] filmDisplays;
    [SerializeField] private SpinResultDisplay resultDisplay;
    [SerializeField] private FilmManager filmManager;

    private float rotationStep;
    private float rotationSpeed;
    private float timer = 0f;
    private bool isSpinning;

    private Film[] films;
    private int frontFace = 0;

    [SerializeField] private float updateInterval = 0.1f;
    private float updateTimer = 0; 

    private void Start()
    {
        rotationStep = 360 / facesNum;
    }

    private void Update()
    {
        if (isSpinning)
        {
            timer += Time.deltaTime;
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            updateTimer += Time.deltaTime;

            if (updateTimer >= updateInterval)
            {
                UpdateFilmDisplays();
                updateTimer = 0; 
            }
        }

        if (timer >= rotationTime)
        {
            isSpinning = false;
            timer = 0;
            resultDisplay.ShowRollResult(filmDisplays[frontFace].film);
        }
    }

    public void Spin()
    {
        if (!isSpinning)
        {
            Vector3 currentRotation = transform.eulerAngles;
            currentRotation.z = 0;

            transform.eulerAngles = currentRotation;

            int numRotations = Random.Range(50, 100);
            Debug.Log(numRotations);
            rotationSpeed = (rotationStep / rotationTime) * numRotations;
            updateInterval = rotationTime / numRotations;
            isSpinning = true;

            CreateFilmQueue(numRotations);
            FillDisplays();
        }
    }

    public void CreateFilmQueue(int count)
    {
        List<FilmStatus> whitelist = new List<FilmStatus>();
        whitelist.Add(FilmStatus.notWatched);
        List<Film> avaliableFilms = filmManager.GetFilms(whitelist);
        List<Film> unusedFilms = new List<Film>(avaliableFilms);

        films = new Film[count];

        for (int i = 0; i < count; i++)
        {
            if (unusedFilms.Count == 0)
            {
                unusedFilms = new List<Film>(avaliableFilms);
            }
            int rand = Random.Range(0, unusedFilms.Count-1);
            films[i] = unusedFilms[rand];
            unusedFilms.Remove(films[i]);
        }
    }

    private void UpdateFilmDisplays()
    {
        float currentRotation = transform.eulerAngles.z;
        int filmCount = films.Length;
        int displayCount = filmDisplays.Length;

        int startFilmIndex = Mathf.FloorToInt(currentRotation / 360f * filmCount) % filmCount;

        for (int i = 0; i < displayCount; i++)
        {
            int filmIndex = (startFilmIndex + i) % filmCount;
            filmDisplays[i].InstantiateDisplay(films[filmIndex]); 
        }

        frontFace = frontFace == 5 ? 0 : frontFace + 1;
    }

    public void FillDisplays()
    {
        for (int i = 0; i < filmDisplays.Length; i++)
        {
            filmDisplays[i].InstantiateDisplay(films[i]);
        }
    }

    public void FillDisplaysWithRandomFilms()
    {
        for (int i = 0; i < filmDisplays.Length; i++)
        {
            int rand = Random.Range(0,films.Length);
            filmDisplays[i].InstantiateDisplay(films[rand]);
        }
    }
}
