using Unity.VisualScripting;
using UnityEngine;

public class shipcod : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector2 ThrustDirection = new Vector2(1, 0);
    public float ThrustForce = 6.7f;
    private float shipRadius;
    private const float RotateDegreePerSecond = 48f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // Assuming ScreenUtils and CircleCollider2D are available/configured
        shipRadius = GetComponent<CircleCollider2D>().radius;
    }

    private void FixedUpdate()
    {
        //thrust
        if (Input.GetKey(KeyCode.Space) == true)
        {
            // Debug Log 1: Thrust application
            Debug.Log("Thrust is applied when the space bar is pressed or held");
            // Debug Log 2: Thrust direction
            Debug.Log("Thrust is applied in the direction the ship is facing. Thrust Direction: " + ThrustDirection);

            rb2d.AddForce(ThrustDirection.normalized * ThrustForce);
        }
    }

    void OnBecameInvisible()
    {
        Vector2 newPosition = transform.position;
        bool wrapped = false; // Flag to track if wrapping occurred

        // Exited LEFT
        if (newPosition.x < ScreenUtils.ScreenLeft - shipRadius)
        {
            newPosition.x = ScreenUtils.ScreenRight + shipRadius;
            wrapped = true;
        }
        // Exited RIGHT
        else if (newPosition.x > ScreenUtils.ScreenRight + shipRadius)
        {
            newPosition.x = ScreenUtils.ScreenLeft - shipRadius;
            wrapped = true;
        }

        // Exited BOTTOM
        if (newPosition.y < ScreenUtils.ScreenBottom - shipRadius)
        {
            newPosition.y = ScreenUtils.ScreenTop + shipRadius;
            wrapped = true;
        }
        // Exited TOP
        else if (newPosition.y > ScreenUtils.ScreenTop + shipRadius)
        {
            newPosition.y = ScreenUtils.ScreenBottom - shipRadius;
            wrapped = true;
        }

        transform.position = newPosition;

        // Debug Log 4: Screen wrapping
        if (wrapped)
        {
            Debug.Log("The ship screen wraps properly. Wrapping should work correctly for the left, right, top, and bottom sides and all four corners. New position: " + newPosition);
        }
    }


    // Update is called once per frame
    void Update()
    {
        // The "beyblade" axis likely maps to Left/Right arrow keys or 'A'/'D'
        float rotationInput = Input.GetAxis("beyblade");

        // Check if the player is pressing a rotation button
        if (Mathf.Abs(rotationInput) > 0)
        {
            // Debug Log 3: Ship rotation
            if (rotationInput < 0)
            {
                Debug.Log("The ship rotates counter-clockwise when the left key is pressed or held");
            }
            else // rotationInput > 0
            {
                Debug.Log("The ship rotates clockwise when the right key is pressed or held");
            }

            float rotationAmount = RotateDegreePerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);
        }


        // Get the current rotation around the Z-axis in degrees
        float rotationZ = transform.eulerAngles.z;

        // Convert the Z rotation from degrees to radians
        float rotationZRadians = rotationZ * Mathf.Deg2Rad;

        // Calculate the new X and Y components of the ThrustDirection vector
        ThrustDirection.x = Mathf.Cos(rotationZRadians);
        ThrustDirection.y = Mathf.Sin(rotationZRadians);
    }
}