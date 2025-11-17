using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplanePlayer : MonoBehaviour
{
    public float m_speed = 1;
    protected Transform m_transform;
    public int m_life = 3;
    
    public AudioClip m_shootClip;  // 声音
    protected AudioSource m_audio;  // 声音源
    public Transform m_explosionFX;
    
    
    public Transform m_rocket;
    float m_rocketTimer = 0;
    
    protected Vector3 m_targetPos; // 目标位置
    public LayerMask m_inputMask; // 鼠标射线碰撞层
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        m_audio = this.GetComponent<AudioSource>();
        m_targetPos = this.m_transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        // //纵向移动距离
        // float movev = 0;
        //
        // //水平移动距离
        // float moveh = 0;
        //
        // //按上键
        // if (Input.GetKey(KeyCode.UpArrow))
        // {
        //     movev += m_speed * Time.deltaTime;
        // }
        //
        // // 按下键
        // if (Input.GetKey(KeyCode.DownArrow))
        // {
        //     movev -= m_speed * Time.deltaTime;
        // }
        //
        // // 按左键
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     moveh -= m_speed * Time.deltaTime;
        // }
        //
        // // 按右键
        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     moveh += m_speed * Time.deltaTime;
        // }
        //
        // //移动
        // this.m_transform.Translate(new Vector3(moveh, 0, movev));

        MoveTo();
        
        m_rocketTimer -= Time.deltaTime;
        if ( m_rocketTimer <= 0 )
        {
            m_rocketTimer = 0.1f;
            
            // 按空格键或鼠标左键发射子弹
            if ( Input.GetKey( KeyCode.Space ) || Input.GetMouseButton(0) )
            {
                // Instantiate( m_rocket, m_transform.position, m_transform.rotation );
                var spawnPool = PathologicalGames.PoolManager.Pools["mypool"];
                spawnPool.Spawn("Rocket", m_transform.position, m_transform.rotation );
                m_audio.PlayOneShot(m_shootClip);
                
            }
        }
    }

    void MoveTo()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 ms = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(ms);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, 1000, m_inputMask))
            {
                m_targetPos = hitinfo.point;
            }
        }

        Vector3 pos = Vector3.MoveTowards(m_transform.position, m_targetPos, m_speed * Time.deltaTime);
        this.m_transform.position = new Vector3(pos.x, 1, pos.z);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag!="PlayerRocket")  // 如果与主角子弹以外的碰撞体相撞
        {
            m_life -= 1;  // 减少生命
            AirplaneGameManager.Instance.ChangeLife(m_life);  // 添加代码, 更新UI
            if (m_life <= 0)
            {
                Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
                Destroy(this.gameObject);  // 自我销毁
            }
        }
    }
}
