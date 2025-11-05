using Unity.VisualScripting;
using UnityEngine;

public class shipcod : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector2 ThrustDirection = new Vector2(1,0);
    public float ThrustForce;
    private float shipRadius;
    private const float RotateDegreePerSecond = 180f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        shipRadius = GetComponent<CircleCollider2D>().radius;
    }

    private void FixedUpdate()
    {
        //thrust
        if (Input.GetKeyDown(KeyCode.Space) == true) 
        {
            rb2d.AddForce(ThrustDirection.normalized * ThrustForce);
        }
    }

    void OnBecameInvisible()
    {
        Vector2 newPosition = transform.position;

        // Exited LEFT
        if (newPosition.x < ScreenUtils.ScreenLeft - shipRadius)
        {
            newPosition.x = ScreenUtils.ScreenRight + shipRadius;
        }
        // Exited RIGHT
        else if (newPosition.x > ScreenUtils.ScreenRight + shipRadius)
        {
            newPosition.x = ScreenUtils.ScreenLeft - shipRadius;
        }

        // Exited BOTTOM
        if (newPosition.y < ScreenUtils.ScreenBottom - shipRadius)
        {
            newPosition.y = ScreenUtils.ScreenTop + shipRadius;
        }
        // Exited TOP
        else if (newPosition.y > ScreenUtils.ScreenTop + shipRadius)
        {
            newPosition.y = ScreenUtils.ScreenBottom - shipRadius;
        }

        transform.position = newPosition;
    }


    // Update is called once per frame
    void Update()
    {

        float rotationInput = Input.GetAxis("Rotate");

        // Check if the player is pressing a rotation button
        if (Mathf.Abs(rotationInput) > 0)
        {
            float rotationAmount = RotateDegreePerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);
        }
         
    }
}
