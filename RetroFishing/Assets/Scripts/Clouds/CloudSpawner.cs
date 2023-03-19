using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloud1;
    public GameObject cloud2;
    public float spawnTime = 1f;
    public float highPoint = 3f;
    public float lowPoint = 1f;

    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCloud();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnCloud();
            timer = 0;
        }
    }

    void SpawnCloud()
    {
        int random = Random.Range(1, 2);
        GameObject chosenCloud = cloud1;

        if (random == 1)
        {
            chosenCloud = cloud1;
        }
        else if (random == 2)
        {
            chosenCloud = cloud2;
        }

        GameObject cloud = Instantiate(chosenCloud, new Vector3(transform.position.x, Random.Range(lowPoint, highPoint), transform.position.z), transform.rotation);
        cloud.transform.parent = transform;
    }
}
