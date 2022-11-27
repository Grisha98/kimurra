using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEdge : MonoBehaviour
{
    public float Angle;
    public GameObject LightRay;
    public float LightStep;
    public GameObject LightEnd;

    private bool stopped;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, Angle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stopped)
        {
            LightRay.transform.localScale += new Vector3(0, LightStep, 0);
            if(LightRay.transform.localScale.y != 0)
            {
                LightEnd.transform.localScale = new Vector3(1, 1 / LightRay.transform.localScale.y, 1);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Crystal>() != null && !ReferenceEquals(collision.gameObject.transform, transform.parent))
        {
            stopped = true;
        }
    }
}
