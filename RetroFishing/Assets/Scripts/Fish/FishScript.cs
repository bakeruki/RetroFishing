using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float timeBetweenDirectionSwitches = 5f;
    public float xHookedOffset = 1f;
    public float yHookedOffset = .5f;
    public int value = 50;
    public GameObject bobber;

    private bool left = false;
    private float timeSinceDirectionSwitch = 0f;
    private BobberScript bobberScript;
    private bool hooked;

    // Start is called before the first frame update
    void Start()
    {
        float random = Random.Range(1f, 3f);
        timeBetweenDirectionSwitches *= random;
        bobber = GameObject.FindGameObjectWithTag("Player");
        bobberScript = bobber.GetComponent<BobberScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hooked)
        {
            if (left)
            {
                transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y, transform.position.z);
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, 1);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y, transform.position.z);
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, 1);
            }

            timeSinceDirectionSwitch += Time.deltaTime;



            if (timeSinceDirectionSwitch > timeBetweenDirectionSwitches)
            {
                timeSinceDirectionSwitch = 0;
                if (left)
                {
                    left = false;
                }
                else
                {
                    left = true;
                }
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, -65));
            transform.position = new Vector3(bobber.transform.position.x + xHookedOffset, bobber.transform.position.y - yHookedOffset, bobber.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            if (!bobberScript.FishHooked())
            {
                Debug.Log("Bobber collision with a fish");
                bobberScript.HookFish(gameObject);
                hooked = true;
            }   
        }
    }

    public int GetValue()
    {
        return value;
    }
}
