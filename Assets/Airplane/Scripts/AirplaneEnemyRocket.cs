using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirplaneEnemyRocket : AirplaneRocket
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        Destroy(this.gameObject);
    }
}
