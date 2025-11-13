using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AirplaneTitleSceen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnButtonGameStart(){
        SceneManager.LoadScene("AirplaneGame");  // 读取关卡level1
    }
}
