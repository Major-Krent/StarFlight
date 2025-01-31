using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Manager : MonoBehaviour
{

    public GameObject player;
    private BossController Boss;
    private ItemGenerator Item;


    // Start is called before the first frame update
    void Start()
    {

        Boss = FindObjectOfType<BossController>();
        Item= FindObjectOfType<ItemGenerator>();


    }



    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
                PlayerDie();
        }
        GameClear();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    private void PlayerDie()
    {
        if (player == null)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    private void GameClear()
    {
        
        if (Item.isBossGen ==true)
        {
            if (Boss == null) // ��̬���� Boss ����
            {
                Boss = FindObjectOfType<BossController>();
            }

            if (Boss != null) // ȷ�� Boss ���ں��ٷ���������
            {
                Debug.Log("ok1");
                if (Boss.isBossDie == true)
                {
                    Debug.Log("ok");
                    SceneManager.LoadScene("ClearScene");
                }
            }
        }
    }
}
