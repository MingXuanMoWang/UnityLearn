using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    public Transform m_transform;

    // 角色控制器组件
    CharacterController m_ch;

    // 角色移动速度
    float m_movSpeed = 3.0f;

    // 重力
    float m_gravity = 2.0f;
    
    public int m_life = 5;
    
    // 摄像机Transform
    Transform m_camTransform;

    // 摄像机旋转角度
    Vector3 m_camRot;

    // 摄像机高度(即表示主角的身高)
    float m_camHeight = 1.4f;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        // 获取角色控制器组件
        m_ch = this.GetComponent<CharacterController>();

        m_camTransform = Camera.main.transform;
        m_camTransform.position = m_transform.TransformPoint(0, m_camHeight, 0);
        
        // 设置摄像机的旋转方向与主角一致
        m_camTransform.rotation = m_transform.rotation;
        m_camRot = m_camTransform.eulerAngles;
        
        Screen.lockCursor = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (m_life <= 0)
            return;

        Control();
    }

    void Control()
    {
        //获取鼠标移动距离
        float rh = Input.GetAxis("Mouse X");
        float rv = Input.GetAxis("Mouse Y");

        // 旋转摄像机
        m_camRot.x -= rv;
        m_camRot.y += rh;
        m_camTransform.eulerAngles = m_camRot;
        
        // 使主角的面向方向与摄像机一致
        Vector3 camrot = m_camTransform.eulerAngles;
        camrot.x = 0; camrot.z = 0;
        m_transform.eulerAngles = camrot;
        
        Vector3 motion = Vector3.zero;
        motion.x = Input.GetAxis("Horizontal") * m_movSpeed * Time.deltaTime;
        motion.z = Input.GetAxis("Vertical") * m_movSpeed * Time.deltaTime;
        motion.y -= m_gravity * Time.deltaTime;
        m_ch.Move( m_transform.TransformDirection(motion) );
        
        
        
        m_camTransform.position = m_transform.TransformPoint(0, m_camHeight, 0);


    }
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "Spawn.tif");
    }
}
