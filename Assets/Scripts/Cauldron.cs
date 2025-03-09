using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Cauldron : MonoBehaviour
{

    private List<GameObject> droppedRecipes = new List<GameObject>(); //Creating a list for the dropped recipies (items)
    private UIManager UImanager; //Reference to the UI manager script
    [SerializeField] private AudioClip itemSFX; //Audio clip that will play when an item is dropped
    [SerializeField] private AudioClip PotionCreatedSFX; //Audio clip that will play when an item is dropped

    void Start()
    {
        UImanager = FindObjectOfType<UIManager>(); //Find an object in the scene that has the UI manager script attached to it
    }

    //Simple on trigger when the player drags and drops an item into Cauldron
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Recipe"))
        {
            if(other.gameObject != null)
            {
            droppedRecipes.Add(other.gameObject); //Adds the items dropped to the list created earlier

            //Play soundeffect

            AudioManager.instance.playSFX(itemSFX, transform, 0.3f);
            Destroy(other.gameObject);
            
            //Debug.Log("Dropped " + other.gameObject.name);
            
            UImanager.DisplayTextFeedback(other.gameObject.name);

            if(droppedRecipes.Count  == UImanager.GetCorrectRecipe().Count) //Checks if the dropped items are exactly 4
            {
                
                checkRecipes(); //If it is then call the Check Recipes method
            }  
            }  
        }
    }


    //Method that will check if the items dropped in the Cauldron match the ones shown in the UI
    private void checkRecipes()
    {
       List<GameObject> correctRecipes = UImanager.GetCorrectRecipe(); //Calls the get correct recipe method from the UI manager script

       if(correctRecipes.Count == droppedRecipes.Count) //Checks to see if the correct recipes and the dropped items match 
       {
         //Create a bool that will check if they are both matching
         bool ismatch = true;

        //Loop that will loop through the correct recipies 
         for(int i = 0; i < correctRecipes.Count; i++)
         {
            //Check the index of each
            GameObject correctRecipe = correctRecipes[i];
            GameObject droppedRecipe = droppedRecipes[i];
            
            if(correctRecipe != null && droppedRecipe != null)
            {
                    //Comparing the sprites of each and checking to see if they match
            Sprite correctSprite = correctRecipe.GetComponent<SpriteRenderer>().sprite;
            Sprite droppedSprite = droppedRecipe.GetComponent<SpriteRenderer>().sprite;

            if(correctSprite != droppedSprite) //If the correct item does not match the dropped item
            {
                ismatch = false;
                break;
            }
            }
           
         }

         if(ismatch)
         {
            Debug.Log("Potion Created");

            UImanager.DisplayPotionFeedback("Potion generated! ");

            AudioManager.instance.playSFX(PotionCreatedSFX, transform, 0.09f);

            droppedRecipes.Clear();

            UImanager.GenerateNewPotion();
         }
         else
         {
            Debug.Log("Wrong ingredients! Try again!");

            UImanager.DisplayPotionFeedback("Wrong ingredients!");
            droppedRecipes.Clear();
         }

       }

     }

}
