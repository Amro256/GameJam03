using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject[] Potions; //An array to hold all of the Potions sprites     

    [SerializeField] Image canvasImage; //Refernece to the Image component of the canvas -- Make this Private after testing)


    // Start is called before the first frame update
    void Start()
    {
        //Method here that will randomise the final item
        randomItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void randomItem()
    {
        //Null check
        if(Potions.Length == 0)
        {
            Debug.Log("No Potions assigned!");
            return;
        }

        //Randomise between an item in the potions array
        int randomPostion = Random.Range(0, Potions.Length); 
        GameObject selectedPotion = Potions[randomPostion];

        SpriteRenderer sr = selectedPotion.GetComponent<SpriteRenderer>();

        if(sr != null) //If the sprite renderer is not null, show the selected potion sprite
        {
            canvasImage.sprite = sr.sprite;
        }

        
    }
}
