using System.Collections;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance; // 单例模式
    private GameUIManager UI;

    public int Score { get; private set; }
    public float UseTime { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 防止场景切换时被销毁
        }
        else
        {
            Destroy(gameObject); // 如果已经有实例存在，销毁当前实例
        }
    }

    void Update()
    {
        // 检查 UI 是否存在
        if (UI == null)
        {
            UI = FindObjectOfType<GameUIManager>();
        }

        // 如果找到 UI，则同步数据
        if (UI != null)
        {
            Score = UI.score;
            UseTime = UI.time;
        }
    }
}
