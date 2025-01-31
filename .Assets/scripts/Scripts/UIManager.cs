using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // “˝»ÅETextMeshPro ◊Èº˛
using UnityEngine.TextCore.Text;

public class GameUIManager : MonoBehaviour
{
    public float time;
    public int score;
    public TextMeshProUGUI playerHPText;
    public Image hpBarFill;
    public TextMeshProUGUI droneCountText;
    public TextMeshProUGUI Showtime;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Alert;

    private PlayerController player;
    private DroneManager droneManager;
    private Enemy1Movement Enm1;
    private Enemy2Movement Enm2;
    private Enemy3Movement Enm3;
    private ItemGenerator Boss;

    private Color textColor;
    private bool showit=true;

    void Start()
    {
        textColor=Alert.color;
        score = 0;
        time = 0;
        player = FindObjectOfType<PlayerController>();
        droneManager = FindObjectOfType<DroneManager>();
        Boss=FindObjectOfType<ItemGenerator>();
        Enm1 = FindObjectOfType<Enemy1Movement>();
        Enm2 = FindObjectOfType<Enemy2Movement>();
        Enm3 = FindObjectOfType<Enemy3Movement>();
    }

    void Update()
    {
        

        time += Time.deltaTime;

        Showtime.text = "Time: "+ (int)time;

        Score.text = "Score: " + score;

        if (player != null)
        {
            float hpPercentage = (float)player.HP / 10; 
            hpBarFill.fillAmount = hpPercentage; 
            playerHPText.text = "HP: " + player.HP;
        }

        if (droneManager != null)
        {
            droneCountText.text = "Drones: " + droneManager.drones.Count;
        }

        if(Boss.isBossGo&&showit)
        {

            Alert.gameObject.SetActive(true);
            StartCoroutine(FadeText());
            showit= false;
        }

    }

    IEnumerator FadeText()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1f, 0f, 1f));
            yield return new WaitForSeconds(0.8f);

            yield return StartCoroutine(Fade(0f, 1f, 1f));
            yield return new WaitForSeconds(0.8f);

            yield return StartCoroutine(Fade(1f, 0f, 1f));
            yield return new WaitForSeconds(0.8f);

            yield return StartCoroutine(Fade(0f, 1f, 1f));
            yield return new WaitForSeconds(0.8f);

            yield return StartCoroutine(Fade(1f, 0f, 1f));
            yield return new WaitForSeconds(0.8f);

            Alert.gameObject.SetActive(false);
        }
    }

    IEnumerator Fade(float startAlpha,float endAlpha, float duration)
    {
        float elapsed = 0f;
        while(elapsed<duration)
        {
            elapsed += Time.deltaTime;
            textColor.a=Mathf.Lerp(startAlpha,endAlpha,elapsed/duration);
            Alert.color = textColor;
            yield return null;
        }
    }
}