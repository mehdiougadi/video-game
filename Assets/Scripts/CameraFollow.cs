using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float camSpeed = 2;
    public GameObject target;

    void Start()
    {
    }

    void Update()
    {
        Vector3 newPos = new Vector3(target.transform.position.x, target.transform.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position,newPos,camSpeed*Time.deltaTime);
    }
}