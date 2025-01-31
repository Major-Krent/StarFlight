using System.Collections;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance; // ����ģʽ
    private GameUIManager UI;

    public int Score { get; private set; }
    public float UseTime { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ��ֹ�����л�ʱ������
        }
        else
        {
            Destroy(gameObject); // ����Ѿ���ʵ�����ڣ����ٵ�ǰʵ��
        }
    }

    void Update()
    {
        // ��� UI �Ƿ����
        if (UI == null)
        {
            UI = FindObjectOfType<GameUIManager>();
        }

        // ����ҵ� UI����ͬ������
        if (UI != null)
        {
            Score = UI.score;
            UseTime = UI.time;
        }
    }
}
