using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneEnemy : MonoBehaviour
{
    public float m_speed = 1;   // 速度
    public float m_life = 10;   // 生命
    protected float m_rotSpeed = 30;    // 旋转速度
    
    
    internal Renderer m_renderer;  // 模型渲染组件
    internal bool m_isActiv = false;  // 是否激活
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = this.GetComponent<Renderer>(); // 获得模型渲染组件

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        
        if (m_isActiv && !this.m_renderer.isVisible )  // 如果移动到屏幕外
        {
            Destroy(this.gameObject); // 自我销毁
        }
    }
    void OnBecameVisible()  // 当模型进入屏幕
    {
        m_isActiv = true;
    }
    protected virtual void UpdateMove()
    {
        // 左右移动
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;

        // 前进
        transform.Translate(new Vector3(rx, 0, -m_speed * Time.deltaTime));
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRocket") // 如果撞到主角子弹
        {
            AirplaneRocket rocket = other.GetComponent<AirplaneRocket>();
            if (rocket != null)
            {
                m_life -= rocket.m_power;  // 减少生命

                if (m_life <= 0)
                {
                    Destroy(this.gameObject);  // 自我销毁
                }
            }
        }
        else if (other.tag == "Player")  // 如果撞到主角
        {
            m_life = 0;
            Destroy(this.gameObject); // 自我销毁
        }
    }
}
