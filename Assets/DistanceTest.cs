using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTest : MonoBehaviour
{
    public Transform _t1;
    public Transform _t2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
            Debug.Log(Vector2.SqrMagnitude(_t1.position - _t2.position));
            Debug.Log(Vector2.Distance(_t1.position, _t2.position));
        }
    }
}
