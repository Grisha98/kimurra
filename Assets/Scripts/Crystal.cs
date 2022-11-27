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
    public GameObject ExitLight;
    public LightEdge InputLight;

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

    List<float> possibleAngles = new List<float> { 0, 60, 90, 120, 180, 240, 270, 300 };

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

        SetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LightEdge light = collision.gameObject.GetComponentInParent<LightEdge>();
        ReflectLight(light);
    }

    public void Turn(bool clockwise = true)
    {
        int ai = possibleAngles.FindIndex(a => a == Angle);
        ai += clockwise ? -1 : 1;

        if(ai == -1)
        {
            ai = possibleAngles.Count - 1;
        } else if (ai == possibleAngles.Count){
            ai = 0;
        }
        Angle = possibleAngles[ai];

        SetSprite();
        if(ExitLight != null)
        {
            ExitLight.GetComponent<LightEdge>().Remove();
            ExitLight = null;
        }
        ReflectLight(InputLight);
    }

    private void SetSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Angle].sprite;
        gameObject.GetComponent<Collider2D>().offset = new Vector2(sprites[Angle].colliderX, gameObject.GetComponent<Collider2D>().offset.y);
    }

    private void ReflectLight(LightEdge light)
    {
        if (ExitLight == null && light != null)
        {
            InputLight = light;
            if (angles.ContainsKey(new Vector2(InputLight.Angle, Angle)))
            {
                ExitLight = Instantiate(lightEdge, gameObject.transform);
                InputLight.ChildLight = ExitLight.GetComponent<LightEdge>();
                ExitLight.transform.position = InputLight.LightEnd.transform.position;

                ExitLight.GetComponent<LightEdge>().Angle = angles[new Vector2(light.Angle, Angle)].angle;

                if (angles[new Vector2(light.Angle, Angle)].under)
                {
                    light.LightRay.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    light.LightEnd.GetComponent<SpriteRenderer>().sortingOrder = 0;

                    ExitLight.GetComponent<LightEdge>().LightRay.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    ExitLight.GetComponent<LightEdge>().LightEnd.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    ExitLight.GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
                else
                {
                    light.LightRay.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    light.LightEnd.GetComponent<SpriteRenderer>().sortingOrder = 2;

                    ExitLight.GetComponent<LightEdge>().LightRay.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    ExitLight.GetComponent<LightEdge>().LightEnd.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    ExitLight.GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
            }
        }
    }
}
