using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneSuperEnemy : AirplaneEnemy
{
    public Transform m_rocket;
    protected float m_fireTimer = 0;
    protected Transform m_player;
    protected override void UpdateMove()
    {
        m_fireTimer -= Time.deltaTime;
        if (m_fireTimer <= 0)
        {
            m_fireTimer = 2;  // 每2秒射击一次
            if (m_player != null)
            {
                Vector3 relativePos = m_player.position - transform.position;  // 获取朝向主角的方向向量
                Instantiate(m_rocket, transform.position, Quaternion.LookRotation(relativePos)); // 创建子弹
            }
            else
            {
                GameObject obj = GameObject.FindGameObjectWithTag("Player"); // 查找主角
                if (obj != null)
                {
                    m_player = obj.transform;
                }
            }
        }
        
        
        transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
    }
}
