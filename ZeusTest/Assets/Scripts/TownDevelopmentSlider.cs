using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TownDevelopmentSlider : MonoBehaviour
{
    public static TownDevelopmentSlider instance;
    [SerializeField] private GameObject[] crownsImages;
    [SerializeField] private Image[] slidersImages;
    [SerializeField] private Image[] housesImages;
    [SerializeField] private Slider leftSlider;
    [SerializeField] private Slider middleSlider;
    [SerializeField] private GameObject[] crownImages; // 0 is  left, 1 middle, 2 right
    int totalScore = 3;
    List<int> scores = new List<int>() { 1, 1, 1 };
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
    private IEnumerator Start()
    {
        //Wait for town to be initialized
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i < GameManager.instance.Towns.Count; i++)
        {
            Color color = GameManager.instance.Towns[i].GetComponent<TownBehaviour>().townColor;
            slidersImages[i].color = color;
            housesImages[i].color = color;
            crownsImages[i].SetActive(false);
        }
    }
    public void UpdateSliders(int townIndex, int score)
    {
        scores[townIndex] = score+1;
        totalScore = scores[0] + scores[1] + scores[2];

        float previousValue = (float)scores[0] / totalScore;
        leftSlider.value = previousValue;
        middleSlider.value = previousValue + (float)scores[1] / totalScore;

        PutCrownOnBestTown();
    }

    private void PutCrownOnBestTown()
    {
        for(int i=0; i < crownsImages.Length; i++)
        {
            if (scores[i] == scores.Max())
            {
                crownsImages[i].SetActive(true);
                continue;
            }
            crownsImages[i].SetActive(false);
        }
    }

    public void MoveCameraToTown(int townIndex)
    {
        Camera.main.transform.parent.GetComponent<CameraMovement>().MoveToObject(GameManager.instance.Towns[townIndex]);
    }
}
