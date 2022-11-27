using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveLight : MonoBehaviour
{
    public bool LeftWall;
    public GameObject LightEdge;
    // Start is called before the first frame update
    void Start()
    {
        GenerateLight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateLight()
    {
        //Generate source
        //Instantiate(LightEdge, transform);  
    }

}
