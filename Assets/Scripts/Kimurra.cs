using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Kimurra : MonoBehaviour
{
    public float walkSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    public GameObject Spot;
    public GameObject MainShadow;

    private List<GameObject> lightStack;
    private Dictionary<GameObject, GameObject> shadowStack;
    private float closestDistance = 0f;

    Vector2 movement;

    void Start()
    {
        lightStack = new List<GameObject>();
        shadowStack = new Dictionary<GameObject, GameObject>();
    }

    void Update()
    {
        //Start - walk
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        //End - walk

        HandleShadows();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * walkSpeed * Time.fixedDeltaTime);
    }

    public void GenerateSpot1()
    { 
        Instantiate(Spot, transform.position-new Vector3(0.3f,0,0), Spot.transform.rotation);
    }

    public void GenerateSpot2()
    {
        Instantiate(Spot, transform.position - new Vector3(-0.3f, 0, 0), Spot.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Light2D>() != null)
        {
            lightStack.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Light2D>() != null)
        {
            lightStack.Remove(collision.gameObject);
            Destroy(shadowStack[collision.gameObject]);
            shadowStack.Remove(collision.gameObject);
        }
    }

    private void HandleShadows()
    {
        closestDistance = 10;
        //Add new shadows
        foreach(GameObject light in lightStack)
        {
            if(!shadowStack.ContainsKey(light))
            {
                shadowStack[light] = Instantiate(MainShadow, transform);
                shadowStack[light].transform.localScale = new Vector3(shadowStack[light].transform.localScale.x, 0.67f, shadowStack[light].transform.localScale.z);
                shadowStack[light].SetActive(true);
            }
        }

        //Handle old shadows
        foreach(KeyValuePair<GameObject, GameObject> entry in shadowStack)
        {
            //angle
            float x = transform.position.x - entry.Key.transform.position.x;
            float y = transform.position.y - entry.Key.transform.position.y + 2.35f;
            float angle = Mathf.Rad2Deg*Mathf.Atan2(x, y);
            entry.Value.transform.eulerAngles = new Vector3(0, 0, 180-angle);

            //animation
            if (GetComponent<SpriteRenderer>().sprite.name != entry.Value.GetComponent<SpriteRenderer>().sprite.name)
            {
                entry.Value.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            }

            //opacity
            //max 0.35
            float distance = Vector3.Distance(transform.position, entry.Key.transform.position - new Vector3(0f, 2.35f, 0f));
            if(closestDistance > distance)
            {
                closestDistance = distance;
            }
            entry.Value.GetComponent<SpriteRenderer>().color = new Color(
                entry.Value.GetComponent<SpriteRenderer>().color.r,
                entry.Value.GetComponent<SpriteRenderer>().color.g,
                entry.Value.GetComponent<SpriteRenderer>().color.b,
                0.5f - 0.45f * distance / 1.788f
            );
        }
        if(closestDistance != 10)
        {
            if(closestDistance > 0.4f)
            {
                MainShadow.GetComponent<SpriteRenderer>().color = new Color(
                    MainShadow.GetComponent<SpriteRenderer>().color.r,
                    MainShadow.GetComponent<SpriteRenderer>().color.g,
                    MainShadow.GetComponent<SpriteRenderer>().color.b,
                    0.2f*closestDistance/1.788f
                );
            }
            else
            {
                MainShadow.GetComponent<SpriteRenderer>().color = new Color(
                    MainShadow.GetComponent<SpriteRenderer>().color.r,
                    MainShadow.GetComponent<SpriteRenderer>().color.g,
                    MainShadow.GetComponent<SpriteRenderer>().color.b,
                    0f
                );
            }
           
        }
        else
        {
            MainShadow.GetComponent<SpriteRenderer>().color = new Color(
                MainShadow.GetComponent<SpriteRenderer>().color.r,
                MainShadow.GetComponent<SpriteRenderer>().color.g,
                MainShadow.GetComponent<SpriteRenderer>().color.b,
                0.313f
            );
        }
    }
}
//yete 0-n 0.35-a, 1.788 0.01