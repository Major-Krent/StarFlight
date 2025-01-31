using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    public List<Drone_Controller> drones = new List<Drone_Controller>(); // 所有无人机
    public Transform player; // 玩家对象

    enum FormationType { Wing, Follow }
    FormationType currentFormation = FormationType.Follow;

    public float wingSpacing = 2f; // 两翼间隔
    public float depthSpacing = 2f; // 前后间隔
    public float followDistance = 10f;
    public float droneSeparationDistance = 5f; // 无人机之间最小间隔
    public float speed = 5f;

    private Dictionary<int, Drone_Controller> wingFormationMap = new Dictionary<int, Drone_Controller>(); // 阵型位置与无人机的映射
    private List<Vector3> wingFormationOffsets = new List<Vector3>(); // 两翼阵型偏移列表
    private bool wingFormationInitialized = false; // 是否已初始化两翼阵型

    int droneCount;
    [HideInInspector] public int type = 0;

    void Update()
    {
        droneCount = drones.Count;

        // 切换阵型
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentFormation = (FormationType)(((int)currentFormation + 1) % System.Enum.GetValues(typeof(FormationType)).Length);

            if (currentFormation == FormationType.Wing)
            {
                InitializeWingFormation();
            }
        }

        // 执行当前阵型逻辑
        switch (currentFormation)
        {
            case FormationType.Follow:
                SetFormationFollow();
                type = 0;
                break;

            case FormationType.Wing:
                MaintainWingFormation();
                type = 1;
                break;
        }

        MaintainSeparation();
    }

    void MaintainSeparation()
    {
        for (int i = 0; i < drones.Count; i++)
        {
            for (int j = i + 1; j < drones.Count; j++)
            {
                Drone_Controller droneA = drones[i];
                Drone_Controller droneB = drones[j];

                float distance = Vector3.Distance(droneA.transform.position, droneB.transform.position);

                if (distance < droneSeparationDistance)
                {
                    Vector3 separationDirection = (droneA.transform.position - droneB.transform.position).normalized;
                    Vector3 adjustment = separationDirection * (droneSeparationDistance - distance) * 0.5f;
                    droneA.transform.position += adjustment;
                    droneB.transform.position -= adjustment;
                }
            }
        }
    }

    void SetFormationFollow()
    {
        foreach (Drone_Controller drone in drones)
        {
            Vector3 targetPosition = player.position + drone.offset;
            drone.offset = new Vector3(-3, 0, 0);
            Vector3 direction = (drone.transform.position - targetPosition).normalized;
            Vector3 newPosition = targetPosition + direction * followDistance;

            drone.transform.position = Vector3.Lerp(drone.transform.position, newPosition, Time.deltaTime * speed);
        }

        wingFormationMap.Clear();
        wingFormationInitialized = false;
    }

    void InitializeWingFormation()
    {
        wingFormationOffsets.Clear();
        wingFormationMap.Clear();

        Vector3 playerBackward = player.right;
        Vector3 playerRight = player.up;

        // 忽略玩家滚转
        playerBackward.y = 0;
        playerRight.y = 0;
        playerBackward.Normalize();
        playerRight.Normalize();

        int needrow = Mathf.Max(1, drones.Count / 8);

        for (int i = 0; i < drones.Count; i++)
        {
            int row = i / needrow; // 行
            int col = i % 2 == 0 ? -1 : 1; // 左右翼

            Vector3 offset = playerBackward * (row + 1) * depthSpacing + playerRight * col * (row + 1) * wingSpacing;
            wingFormationOffsets.Add(offset);
        }

        for (int i = 0; i < drones.Count; i++)
        {
            if (i < wingFormationOffsets.Count)
            {
                wingFormationMap[i] = drones[i];
            }
        }

        wingFormationInitialized = true;
    }

    void MaintainWingFormation()
    {
        if (!wingFormationInitialized) return;

        // **新修复：确保所有无人机都进入阵型**
        for (int i = 0; i < drones.Count; i++)
        {
            if (!wingFormationMap.ContainsValue(drones[i])) // 如果这个无人机还没有阵型位置
            {
                AddDroneToWingFormation(drones[i]); // 立即分配位置
            }
        }

        for (int i = 0; i < wingFormationOffsets.Count; i++)
        {
            if (wingFormationMap.ContainsKey(i) && wingFormationMap[i] != null&&player!=null)
            {
                Drone_Controller drone = wingFormationMap[i];
                Vector3 targetPosition = player.position + wingFormationOffsets[i];

                drone.transform.position = Vector3.Lerp(drone.transform.position, targetPosition, Time.deltaTime * speed);
            }
        }
    }

    public void AddDrone(Drone_Controller newDrone)
    {
        drones.Add(newDrone);

        if (currentFormation == FormationType.Wing)
        {
            AddDroneToWingFormation(newDrone); // **新无人机立即加入 `Wing` 阵型**
        }
    }

    void AddDroneToWingFormation(Drone_Controller drone)
    {
        for (int i = 0; i < wingFormationOffsets.Count; i++)
        {
            if (!wingFormationMap.ContainsKey(i) || wingFormationMap[i] == null)
            {
                wingFormationMap[i] = drone;
                return;
            }
        }

        // **如果没有空位，扩展阵型**
        ExtendWingFormation(drone);
    }

    void ExtendWingFormation(Drone_Controller newDrone)
    {
        int newRow = wingFormationOffsets.Count / 8;
        int newCol = wingFormationOffsets.Count % 2 == 0 ? -1 : 1;

        Vector3 playerBackward = player.right.normalized * (newRow + 1) * depthSpacing;
        Vector3 playerRight = player.up.normalized * newCol * wingSpacing;

        Vector3 newOffset = playerBackward + playerRight;
        wingFormationOffsets.Add(newOffset);
        wingFormationMap[wingFormationOffsets.Count - 1] = newDrone;
    }
}
