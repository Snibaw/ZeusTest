using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private float gameSpeed = 1f;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] public GameObject planet;
    public static bool ShowHealthBar = false;
    private float startTime;
    public bool CanMakeBabel = false;
    
    public GameObject NBResources;

    public List<Vector3> AllPointsOnSphere = new List<Vector3>();
    
    [Header("End Of Level")]
    [SerializeField] private GameObject endOfLevelPanel;

    [Header("CountRessources")]
    public List<WolfPack> WolfPacks;
    
    public List<GameObject> Towns = new List<GameObject>();
    
    public delegate void ShowHealthBarsChangedEventHandler(bool show, float newScale);
    public static event ShowHealthBarsChangedEventHandler ShowHealthBarsChanged;

    private void Awake() {

        WolfPacks = new List<WolfPack>();
        Application.targetFrameRate = 60;
        
        // if(SceneManager.GetActiveScene().name == "MainScene")
        // {
        //     if (!PlayerPrefs.HasKey("Tutorial"))
        //     {
        //         SceneManager.LoadScene("Tuto");
        //         return;
        //     }
        // }
        Time.timeScale = gameSpeed;

        if (instance == null) instance = this;
        else if(instance != this) Destroy(gameObject);
        
        startTime = Time.time;
        endOfLevelPanel.SetActive(false);
        
        if(!PlayerPrefs.HasKey("BestScore")) PlayerPrefs.SetInt("BestScore", 0);
    }
    
    
    
    //Delete when not needed anymore
    public void SetDamageText(float damage, float life)
    {
        damageText.text = Mathf.Ceil(damage).ToString() + " / " + Mathf.Ceil(life).ToString();
        StartCoroutine(ResetDamageText(0.5f));
    }
    IEnumerator ResetDamageText(float time)
    {
        yield return new WaitForSeconds(time);
        damageText.text = "";
    }

    public void EndGame(bool isBabelTowerBuilt)
    {
        int score = (int)(Time.time - startTime);
        if (score > PlayerPrefs.GetInt("BestScore",0))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        endOfLevelPanel.SetActive(true);
        endOfLevelPanel.GetComponent<EndGamePanel>().ShowEndGamePanel(score);
        Time.timeScale = 0;
    }
    
    public void ChangeShowHealthBars(bool show, float newScale = 1)
    {
        ShowHealthBar = show;
        if(ShowHealthBarsChanged != null) ShowHealthBarsChanged(show, newScale);
    }
    


}
