using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int time;
    public TextMeshProUGUI Timetext;
    public TextMeshProUGUI ScoreText;
    private SaveData manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = SaveData.Instance;
        time = (int)manager.UseTime;
        score = manager.Score;
    }

    // Update is called once per frame
    void Update()
    {
        Timetext.text = "Use Time: " + (int)time;
        ScoreText.text = "Your Score: " + score;
    }
}
