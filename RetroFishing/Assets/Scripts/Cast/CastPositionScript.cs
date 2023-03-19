using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastPositionScript : MonoBehaviour
{
    public BobberScript bobber;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<BobberScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bobber.HasCasted())
        {
            transform.position = new Vector2(bobber.getX(), bobber.getY());
        }
    }
}
