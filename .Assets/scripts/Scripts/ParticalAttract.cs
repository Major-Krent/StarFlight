using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalAttract : MonoBehaviour
{
    public Transform center;  // 球体的中心位置
    public float attractionForce = 100.0f;  // 吸引力大小，可以调整
    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;

    void Start()
    {
        // 获取粒子系统组件
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // 获取当前粒子系统的所有粒子
        int numParticles = particleSystem.GetParticles(particles);

        // 遍历所有粒子并使它们朝中心吸引
        for (int i = 0; i < numParticles; i++)
        {
            // 计算粒子到中心的方向
            Vector3 directionToCenter = (center.position - particles[i].position).normalized;

            // 向中心施加吸引力
            particles[i].velocity += directionToCenter * attractionForce * Time.deltaTime;
        }

        // 将修改后的粒子信息应用到粒子系统
        particleSystem.SetParticles(particles, numParticles);
    }
}
