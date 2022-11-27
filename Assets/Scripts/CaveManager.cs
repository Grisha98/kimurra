using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManager : MonoBehaviour
{
    private static CaveManager _instance;

    public static CaveManager Instance { get { return _instance; } }

    public List<GameObject> Spikes;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpikeStatus(bool active)
    {
        foreach(GameObject spike in Spikes)
        {
            spike.SetActive(active);
        }
    }

}
