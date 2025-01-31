using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.Collections.LowLevel.Unsafe;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 50f;
    private Vector3 moveDir;
    private float baseRotationX;
    private float currentRotationX;
    private bool faceRight = false;
    public int HP;
    public GameObject Bullet;
    private float span = 0.1f;
    private float cnt = 0;
    private bool canshoot = true;
    Vector3 Offset1 = new Vector3(-1, 0, 2);
    Vector3 Offset2 = new Vector3(-1, 0, -2);
    private KeyCode lastPressedKey = KeyCode.None;
    private bool isApressed=false;
    private bool isDpressed = false;

    private ItemGenerator Boss;

    void Start()
    {
        HP = 10;
        baseRotationX = transform.eulerAngles.x;
        //baseRotationX = 270.0f;
        currentRotationX = baseRotationX;
        Boss = FindObjectOfType<ItemGenerator>();
    }

    void Update()
    {
        Movement();
        GenBullet();
        Die();

        Vector3 position = transform.position;
        position.z = Mathf.Clamp(position.z, -40, 40);
        position.x = Mathf.Clamp(position.x, -90, 45);
        transform.position = position;

        if(Boss.isBossGo)
        {
            StartCoroutine(StopShooting(12f));
        }
    }

    void Movement()
    {
        moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir += new Vector3(1, 0, 0);
        }

        //Debug.Log("eulerAngles.x =" + transform.eulerAngles.x);

        if (Input.GetKey(KeyCode.A))
        {
            isApressed = true;
            lastPressedKey = KeyCode.A;  // 更新为A键是最后按下的键
            moveDir += new Vector3(0, 0, -1);
            if (transform.eulerAngles.x < baseRotationX + 45)
            {
                transform.Rotate(-200f * Time.deltaTime, 0, 0);
            }
        }
        else { isApressed = false; }
        if (Input.GetKey(KeyCode.D))
        {
            isDpressed= true;
            lastPressedKey = KeyCode.D;  // 更新为D键是最后按下的键
            moveDir += new Vector3(0, 0, 1);
            if (transform.eulerAngles.x < baseRotationX + 45)
            {
                transform.Rotate(200f * Time.deltaTime, 0, 0);
            }
        }
        else { isDpressed = false; }

        if (!isApressed&&!isDpressed)
        {
            Vector3 clampedRotation = transform.eulerAngles;
            clampedRotation.x = baseRotationX;
            transform.eulerAngles = clampedRotation;
        }
        Debug.Log(lastPressedKey);
/*
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            if (lastPressedKey == KeyCode.A)
            {
                moveDir += new Vector3(0, 0, -1);
                if (transform.eulerAngles.x < baseRotationX + 45)
                {
                    transform.Rotate(-200f * Time.deltaTime, 0, 0);
                }
            }
            else if (lastPressedKey == KeyCode.D)
            {
                moveDir += new Vector3(0, 0, 1);
                if (transform.eulerAngles.x < baseRotationX + 45)
                {
                    transform.Rotate(200f * Time.deltaTime, 0, 0);
                }
            }
        }
*/
        if (moveDir.magnitude > 1)
        {
            moveDir.Normalize();
        }


        transform.Translate(moveDir * speed * Time.deltaTime, Space.World);


        Vector3 position = transform.position;
        //position.y = 0;
        transform.position = position;

        currentRotationX = transform.eulerAngles.x;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet_En1")
        {
            HP -= 1;
        }
        if (other.gameObject.tag == "Enemy")
        {
            HP -= 10;
        }
    }



    void GenBullet()
    {
        if (canshoot)
        {
            cnt += Time.deltaTime;
            if (cnt > span)
            {
                Instantiate(Bullet, transform.position + Offset1, Quaternion.identity);
                Instantiate(Bullet, transform.position + Offset2, Quaternion.identity);
                cnt = 0;
            }
        }
    }

    IEnumerator StopShooting(float seconds)
    {
        canshoot = false;
        yield return new WaitForSeconds(seconds);
        canshoot = true;    
    }

    void Die()
    {
        if(HP<=0)
        {
            Destroy(gameObject);
        }
    }

}
