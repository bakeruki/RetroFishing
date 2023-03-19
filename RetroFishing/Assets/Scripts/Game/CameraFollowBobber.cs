using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBobber : MonoBehaviour
{
    public GameObject bobber;
    public float camDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bobber.GetComponent<BobberScript>().HasCasted())
        {
            transform.position = bobber.transform.position + new Vector3(0, 0, -camDistance);
        }
        
    }
}
