using UnityEngine;

public class Ship : MonoBehaviour
{
    private CollectionSystem fishCollector;
    private GameObject targetFish;
    private bool isMoving = false;

    public float moveSpeed = 5.0f;

    void Start()
    {
        fishCollector = Camera.main.GetComponent<CollectionSystem>();
        
    }

    private void OnMouseDown()
    {
        if (!isMoving)
        {
            isMoving = true;
            targetFish = fishCollector.GetNextTarget();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && targetFish != null)
        {
            Vector3 direction = (targetFish.transform.position - transform.position).normalized;


            // move the ship towards target
            transform.position = Vector3.MoveTowards(transform.position,
                                                     targetFish.transform.position,
                                                     moveSpeed * Time.deltaTime);


            // rotate ship to face direction 
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else if (isMoving && targetFish == null)
        {
            isMoving= false; 
            transform.rotation = Quaternion.identity;
        }
    }
}
