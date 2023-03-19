using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class BobberScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject castPosition;

    public float forceBias = 1.0f;
    public float gravityForce = 1.0f;
    public float maxForceStrength = 10.0f;
    public float defaultXForce = 5f;
    public float defaultYForce = 5f;
    public float xSinkSpeed = 0.1f;
    public float ySinkSpeed = 0.1f;
    public float reelSpeed = 1.0f;

    private float forceStrength = 1.0f;
    private bool readyToCast = false;
    private bool hasCasted = false;
    private bool startCastPowerupTimer = false;
    private bool isInWater = false;
    private bool fishHooked = false;
    private bool reeling = false;
    private float castPowerupTimer;
    private float castRadius;
    private float theta;
    private GameObject hookedFish;
    private GameLogic logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
        readyToCast = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (isInWater)
            {
                UpdateWaterState();
            }

            if (!hasCasted)
            {
                UpdateCastState();
            }
        }
    }

    private void UpdateWaterState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            reeling = true;
        }

        if (reeling)
        {
            float step = reelSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, castPosition.transform.position, step);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            SetLandingDistance();
            SetLandingAngle();
            reeling = false;
        }

        if (!reeling)
        {
            if (theta > 0)
            {
                float thetaRadians = theta * (Mathf.PI / 180);
                float dX = Mathf.Cos(thetaRadians) * xSinkSpeed;
                float dY = Mathf.Sin(thetaRadians) * ySinkSpeed;
                dX *= Time.deltaTime;
                dY *= Time.deltaTime;
                transform.position = new Vector2(transform.position.x - dX, transform.position.y - dY);
                SetLandingAngle();
            }
            else
            {
                KillVelocity();
            }
        }

        if(transform.position == castPosition.transform.position)
        {
            if (hookedFish)
            {
                logic.addMoney(hookedFish.GetComponent<FishScript>().GetValue());
                Destroy(hookedFish);
                hookedFish = null;
            }

            ResetCastState();
        }
    }

    private void UpdateCastState()
    {
        if (readyToCast)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startCastPowerupTimer = true;
            }

            if (startCastPowerupTimer)
            {
                castPowerupTimer += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                forceStrength = castPowerupTimer * forceBias;

                if (forceStrength > maxForceStrength)
                {
                    forceStrength = maxForceStrength;
                }

                startCastPowerupTimer = false;
                rb.AddForce(new Vector2(defaultXForce * forceStrength * 100, defaultYForce * forceStrength * 100));
                rb.gravityScale = gravityForce;
                hasCasted = true;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                readyToCast = true;
            }
        }
    }

    private void ResetCastState()
    {
        isInWater = false;
        hasCasted = false;
        readyToCast = false;
        reeling = false;
        fishHooked = false;
        castPowerupTimer = 0f;
        forceStrength = 1f;
    }

    private void SetLandingAngle()
    {
        float dX = transform.position.x - castPosition.transform.position.x;
        float dY = castPosition.transform.position.y - transform.position.y;
        float theta = Mathf.Atan2(dX, dY);

        this.theta = (180 / Mathf.PI) * theta;
    }

    private void SetLandingDistance()
    {
        castRadius = transform.position.x - castPosition.transform.position.x;
    }

    private void KillVelocity()
    {
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
    }

    public void OnWaterCollision()
    {
        SetLandingDistance();
        SetLandingAngle();
        SetInWater();
        KillVelocity();
    }

    public void HookFish(GameObject fish)
    {
        fishHooked = true;
        hookedFish = fish;
    }

    public bool FishHooked()
    {
        return fishHooked;
    }

    public bool HasCasted()
    {
        return hasCasted;
    }

    public float getX()
    {
        return transform.position.x;
    }

    public float getY()
    {
        return transform.position.y;
    }

    private void SetInWater()
    {
        isInWater = true;
    }
}
