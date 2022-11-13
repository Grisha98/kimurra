using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float verticalBound = 7.07f;
    public float horizontalBound = 9.82f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {
        float x = Mathf.Abs(target.position.x) < horizontalBound ? target.position.x : transform.position.x;
        float y = Mathf.Abs(target.position.y) < verticalBound ? target.position.y : transform.position.y;
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
