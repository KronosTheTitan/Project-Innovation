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
        colors = new Color[enemyTexture.width * enemyTexture.height];
        clearTextures();
        //Clear texture
        //Cler lists
        currentTimer = updateTimer;
        sonarCollider = GetComponent<SphereCollider>();
        enemyTexture.SetPixel(64, 32, Color.red);
        enemyTexture.Apply();

        //

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.black;
        }
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
    
    private void UpdateSonar()
    {
        clearTextures();
        //get differance of coordinates of player and enemy
        // normalize that distance by dividing it of the sonar collider range
        // multiply it by the radar range
        // do these 3 comments above for each enemy in the list

        foreach(GameObject enemy in enemies)
        {
            Vector3 deltaPosition = enemy.transform.position - this.transform.position;

            Vector2 enemyPos = new Vector2(enemy.transform.position.x, enemy.transform.position.z);
            Vector2 playerPos = new Vector2(this.transform.position.x, this.transform.position.z);

            float distance = Vector3.Distance(enemy.transform.position, this.transform.position);
            Vector2 radarPosition = new Vector2(deltaPosition.x, deltaPosition.z);
            //enemyPos = radarPosition;

            Quaternion oldrot = this.transform.rotation;
            this.transform.LookAt(enemyPos);
            Vector2 something = new Vector2(this.transform.forward.x, this.transform.forward.z);
            this.transform.rotation = oldrot;
            Vector2 submarine = new Vector2(this.transform.forward.x, this.transform.forward.z);


            radarPosition /= sonarCollider.radius;

            //gets the rotation direction of the player and normalizes it (blue vector)
            Vector2 forwardVector = new Vector2(this.transform.forward.x, this.transform.forward.z);
            forwardVector.Normalize();
            Debug.Log("Forward: " + forwardVector);

            radarPosition.x = (-1 * forwardVector.y);
            radarPosition.y = (-1 * forwardVector.x);

            //something.Normalize

            enemyPos.Normalize();
            float angle = Vector2.SignedAngle(submarine, something);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);

            radarPosition.x = x;
            radarPosition.y = y;





            radarPosition *= (distance / sonarCollider.radius);
            Debug.Log("radar normal: " + radarPosition);
            radarPosition *= enemyTexture.width / 2;
            Debug.Log("radar enemy: " + radarPosition);
            //radarPosition *= enemyTexture.width / 2;
            

            enemyTexture.SetPixel(64+(int)radarPosition.x, 64+(int)radarPosition.y, Color.white);
            enemyTexture.Apply();
        }

    }

    private void clearTextures()
    {

        enemyTexture.SetPixels(0, 0, enemyTexture.width, enemyTexture.height, colors);
        enemyTexture.Apply();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("poopi");
            enemies.Add(other.gameObject);
        }
        if (other.tag == "POI")
        {
            Debug.Log("aaaa");
            POI.Add(other.gameObject);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("WHERE MY POOPI");
            enemies.Remove(other.gameObject);
        }

        if (other.tag == "POI")
        {
            Debug.Log("oooooo");
            POI.Remove(other.gameObject);
        }
    }
}
