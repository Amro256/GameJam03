using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfSpawner : MonoBehaviour
{
    //Variables
    [SerializeField] GameObject[] itemsPrefabs; //These are the items that the player will be able to drag in the game
    //[SerializeField] float spawnIntervel = 5; 
    private GameObject currentItem; //Will be used to track the current gameobject on each spawner

    private List<GameObject> spawnedItems = new List<GameObject>();

    bool canSpawn = true;


    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(itemsPrefabs,transform.position, transform.rotation);
        randomisedShelfItem();
        
    }

    void randomisedShelfItem()
    {
        if(itemsPrefabs.Length == 0)
        {
            Debug.Log("No Items spawned");
            return;
        }

        if(currentItem != null) //Check if the current item is not null
        {
            Destroy(currentItem); //If it is, destroy the current one and another will be spawned in
        }

        int randomShelfItem = Random.Range(0, itemsPrefabs.Length); 

        currentItem = Instantiate(itemsPrefabs[randomShelfItem], transform.position, transform.rotation);


        spawnedItems.Add(currentItem);
    }

    //Make the items spawn every couple of seconds so players have a chance

    IEnumerator ItemSpawn()
    {
        while(true)
        {
            if(canSpawn)
            {
                randomisedShelfItem();
            

            }
            yield return new WaitForSeconds(4f);
            
        }
    }


    //method to remove
    public void RemoveSpawnedItem(GameObject item)
    {
        if(spawnedItems.Contains(item))
        {
            spawnedItems.Remove(item);
            Destroy(item);
        }
    }


    public void startSpwaning()
    {
        canSpawn = true;
    }

    public void stopSpawning()
    {
        canSpawn = false;
    }
 
    
}
