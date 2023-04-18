using System.Collections.Generic;
using UnityEngine;

public class SonarScanner : MonoBehaviour
{


    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> POI = new List<GameObject>();

    public Texture2D enemyTexture;
    public Texture2D POItexture;
    public float updateTimer;
    private float currentTimer;
    Color[] colors;
    private SphereCollider sonarCollider;

    // Start is called before the first frame update
    void Start()
    {
        //create list of colours to reset textures
        colors = new Color[enemyTexture.width * enemyTexture.height];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.black;
        }
        clearTextures();
        currentTimer = updateTimer;
        sonarCollider = GetComponent<SphereCollider>();



    }

    // Update is called once per frame
    void Update()
    {
        //Timer
        //Update textures
        if(currentTimer <= 0)
        {
            currentTimer = updateTimer;
            UpdateSonar();
        }
        currentTimer -= Time.deltaTime;
     


    }


    private void updateTextures(List<GameObject> items, Texture2D texture)
    {
        List<GameObject> removableItems = new List<GameObject>();
        foreach (GameObject item in items)
        {
            if(item == null)
            {
                removableItems.Add(item);
                return;
            }
            //save old rotation
            Quaternion oldrot = this.transform.rotation;
            //we look at the item
            this.transform.LookAt(new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z));
            //save the vector of the rotation towards the item
            Vector2 something = new Vector2(this.transform.forward.x, this.transform.forward.z);
            //set the rotation back
            this.transform.rotation = oldrot;
            //save the rotatation in which the submarine is looking
            Vector2 submarine = new Vector2(this.transform.forward.x, this.transform.forward.z);

            //get the angle between the 2 vectors
            float angle = Vector2.SignedAngle(submarine, something);
            //make a vector out of the angle
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);

            //save the vector to the position
            Vector2 radarPosition = new Vector2(x, y);


            //get the distance between the item and submarine in the world
            float distance = Vector3.Distance(item.transform.position, this.transform.position);

            //normalize the distance compared to the sonar radius
            radarPosition *= (distance / sonarCollider.radius);
            //multiply it by the texture size
            radarPosition *= texture.width / 2;

            //set pixels and apply them
            texture.SetPixel(texture.width / 2 + (int)radarPosition.x, texture.width / 2 + (int)radarPosition.y, Color.white);
            texture.Apply();
        }
        foreach (GameObject removeable in removableItems)
        {
            items.Remove(removeable);
        }
    }


    private void UpdateSonar()
    {
        clearTextures();
        updateTextures(enemies, enemyTexture);
        updateTextures(POI, POItexture);

    }

    private void clearTextures()
    {

        enemyTexture.SetPixels(0, 0, enemyTexture.width, enemyTexture.height, colors);
        enemyTexture.Apply();

        POItexture.SetPixels(0, 0, POItexture.width, POItexture.height, colors);
        POItexture.Apply();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Debug.Log("poopi");
            enemies.Add(other.gameObject);
        }
        if (other.tag == "POI")
        {
           // Debug.Log("aaaa");
            POI.Add(other.gameObject);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //Debug.Log("WHERE MY POOPI");
            enemies.Remove(other.gameObject);
        }

        if (other.tag == "POI")
        {
           // Debug.Log("oooooo");
            POI.Remove(other.gameObject);
        }
    }
}
