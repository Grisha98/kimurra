using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{

    struct CrystalState
    {
        public Sprite sprite;
        public float colliderX;

        public CrystalState(Sprite sprite, float colliderX)
        {
            this.sprite = sprite;
            this.colliderX = colliderX;
        }
    }

    struct LightState
    {
        public float angle;
        public Vector3 position;
        public bool under;

        public LightState(float angle, Vector3 position, bool under)
        {
            this.angle = angle;
            this.position = position;
            this.under = under;
        }
    }

    public List<Sprite> Sprites;
    Dictionary<float, CrystalState> sprites;
    public float Angle;
    public GameObject lightEdge;
    public GameObject Light;

    //Light angle, Crystal angle
    Dictionary<Vector2, LightState> angles = new Dictionary<Vector2, LightState> {
        {new Vector2(120, 0), new LightState(60, new Vector3(0.059f, 0.837f, 0), false) },
        {new Vector2(240, 0), new LightState(300, new Vector3(0.059f, 0.837f, 0), false) },

        {new Vector2(240, 90), new LightState(120, new Vector3(0.013f, 0.682f, 0), true) },
        {new Vector2(300, 90), new LightState(60, new Vector3(0.239f, 0.787f, 0), true) },

        {new Vector2(300, 180), new LightState(240, new Vector3(0.059f, 0.65f, 0), true) },
        {new Vector2(60, 180), new LightState(120, new Vector3(0.059f, 0.65f, 0), true) },

        {new Vector2(60, 270), new LightState(300, new Vector3(-0.013f, 0.787f, 0), true) },
        {new Vector2(120, 270),  new LightState(240, new Vector3(-0.013f, 0.682f, 0), true) },
    };

    // Start is called before the first frame update
    void Start()
    {
        sprites = new Dictionary<float, CrystalState>();
        sprites.Add(0f, new CrystalState(Sprites[0], 0.1f));
        sprites.Add(60f, new CrystalState(Sprites[1], 0.21f));
        sprites.Add(90f, new CrystalState(Sprites[2], 0.22f));
        sprites.Add(120f, new CrystalState(Sprites[3], 0.03f));
        sprites.Add(180f, new CrystalState(Sprites[4], 0.03f));
        sprites.Add(240f, new CrystalState(Sprites[5], 0.03f));
        sprites.Add(270f, new CrystalState(Sprites[6], -0.22f));
        sprites.Add(300f, new CrystalState(Sprites[7], -0.19f));

        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Angle].sprite;
        gameObject.GetComponent<Collider2D>().offset = new Vector2(sprites[Angle].colliderX, gameObject.GetComponent<Collider2D>().offset.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LightEdge light = collision.gameObject.GetComponentInParent<LightEdge>();
        if (Light == null && light != null)
        {
            if(angles.ContainsKey(new Vector2(light.Angle, Angle)))
            {
                Light = Instantiate(lightEdge, gameObject.transform);
                Light.transform.position = light.LightEnd.transform.position;

                Light.GetComponent<LightEdge>().Angle = angles[new Vector2(light.Angle, Angle)].angle;

                if(angles[new Vector2(light.Angle, Angle)].under)
                {
                    light.LightRay.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    light.LightEnd.GetComponent<SpriteRenderer>().sortingOrder = 0;

                    Light.GetComponent<LightEdge>().LightRay.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    Light.GetComponent<LightEdge>().LightEnd.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    Light.GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
                else
                {
                    light.LightRay.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    light.LightEnd.GetComponent<SpriteRenderer>().sortingOrder = 2;

                    Light.GetComponent<LightEdge>().LightRay.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    Light.GetComponent<LightEdge>().LightEnd.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    Light.GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
            }
        }
    }
}
