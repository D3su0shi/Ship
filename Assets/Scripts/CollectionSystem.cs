using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


public class CollectionSystem : MonoBehaviour
{
    public GameObject prefabFish;
    private List<GameObject> fishOnScreen = new List<GameObject>();
    public int totalFishCollected;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            // Adjust Z for ScreenToWorldPoint conversion
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            spawnFish(worldPosition);
        }
    }

    void spawnFish(Vector3 worldPosition)
    {
        if (prefabFish != null)
        {
            // Instantiate the new fish
            GameObject fish = Instantiate<GameObject>(prefabFish);
            fish.transform.position = worldPosition;
            // Add it to the list (which tracks the spawn order)
            fishOnScreen.Add(fish);
        }
    }

    public GameObject GetNextTarget()
    {
        if (fishOnScreen.Count > 0)
        {
            return fishOnScreen[0];
        }    
        return null;
    }

    public void RemoveCollectedFish(GameObject collectedFish)
    { 
        fishOnScreen.Remove(collectedFish);
        totalFishCollected++;
    }
}