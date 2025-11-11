using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneEnemySpawn : MonoBehaviour
{
    public Transform m_enemyPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());  // 启动协程
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy() // 使用协程创建敌人
    {           
        yield return new WaitForSeconds(Random.Range(1, 3));  // 每N秒生成一个敌人
        Instantiate(m_enemyPrefab, transform.position, Quaternion.identity);

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 15));  // 每N秒生成一个敌人
            Instantiate(m_enemyPrefab, transform.position, Quaternion.identity);
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon (transform.position, "item.png", true);
    }
}
