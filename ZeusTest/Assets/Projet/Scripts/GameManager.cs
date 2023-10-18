using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] public GameObject planet;
    private void Awake() {

        Application.targetFrameRate = 60;

        if (instance == null) instance = this;
        else if(instance != this) Destroy(gameObject);
    }
    
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


}
