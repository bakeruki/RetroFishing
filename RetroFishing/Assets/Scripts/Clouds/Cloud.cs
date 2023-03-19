using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float size = 1.0f;
    public float speed = 1.0f;
    public float alpha = 1.0f;

    public float xDeadZone = -25f;

    // Start is called before the first frame update
    void Start()
    {
        size = Random.Range(0.5f, 1.5f);
        speed = Random.Range(1f, 2f);
        alpha = Random.Range(0.5f, 0.9f);

        SetScale();
        SetAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();

        if(transform.position.x < xDeadZone)
        {
            Destroy(gameObject);
        }
    }
    void SetScale()
    {
        transform.localScale = new Vector3(size, size, size);
    }

    void SetAlpha()
    {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
    }

    void UpdatePosition()
    {
        transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
    }
}
