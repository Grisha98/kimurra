using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveKey : MonoBehaviour
{
    public bool isGood;
    public Sprite BrightSprite;
    public Sprite RegSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        GetComponent<SpriteRenderer>().sprite = BrightSprite;
        if (isGood)
        {

        }
        else
        {
            CaveManager.Instance.SpikeStatus(true);
        }
    }

    public void Close()
    { 
        GetComponent<SpriteRenderer>().sprite = RegSprite;
        if (isGood)
        {

        }
        else
        {
            CaveManager.Instance.SpikeStatus(false);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Close();
    }
}
