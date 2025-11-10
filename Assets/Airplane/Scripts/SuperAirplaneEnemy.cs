using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAirplaneEnemy : AirplaneEnemy
{
    protected override void UpdateMove()
    {
        transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
    }
}
