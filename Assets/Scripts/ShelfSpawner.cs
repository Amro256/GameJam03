using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShelfSpawner : MonoBehaviour
{
    //Variables
    [SerializeField] GameObject itemsPrefabs; //These are the items that the player will be able to drag in the game
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(itemsPrefabs,transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
