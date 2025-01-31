using UnityEngine;

public class Drone_Controller : MonoBehaviour
{
    public Vector3 offset; // 无人机的偏移量
    private float hp = 1;
    public GameObject Explosion;

    void Start()
    {
        // 注册到管理器
        DroneManager manager = FindObjectOfType<DroneManager>();
        if (manager != null)
        {
            manager.drones.Add(this);
        }
    }

    private void Update()
    {
        if(hp<=0)
        {
            OnDestroy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet_En1")
        {
            hp -= 1;
        }
        if (other.gameObject.tag == "Enemy")
        {
            hp -= 10;
        }
    }

    private void OnDestroy()
    {
        // 从管理器移除
        DroneManager manager = FindObjectOfType<DroneManager>();
        if (manager != null)
        {
            manager.drones.Remove(this);
        }
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
