using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterColliderScript : MonoBehaviour
{
    private BobberScript bobber;
    public bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        bobber = GameObject.FindGameObjectWithTag("Player").GetComponent<BobberScript>();
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collided = true;
        if (collision.gameObject.layer == 3)
        {
            bobber.OnWaterCollision();
            Debug.Log("Bobber collision with water");
        }
    }
}
