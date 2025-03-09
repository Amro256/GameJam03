using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cauldron : MonoBehaviour
{

    private List<GameObject> droppedRecipes = new List<GameObject>(); //Creating a list for the dropped recipies (items)
    private UIManager UImanager; //Reference to the UI manager script


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
            Debug.Log("Dropped " + other.gameObject.name);
            //Destroy(other.gameObject);

            if(droppedRecipes.Count == 4) //Checks if the dropped items are exactly 4
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


    //    // Debugging: log the correct recipes and their sprites
    // Debug.Log("Correct Recipes:");
    // foreach (var recipe in correctRecipes)
    // {
    //     Debug.Log(recipe.name + " Sprite: " + recipe.GetComponent<SpriteRenderer>().sprite.name);
    // }

       if(correctRecipes.Count == 4 && droppedRecipes.Count == 4) //Checks to see if the correct recipes and the dropped items match 
       {
         //Create a bool that will check if they are both matching
         bool ismatch = true;

        //Loop that will loop through the correct recipies 
         for(int i = 0; i < correctRecipes.Count; i++)
         {
            //Check the index of each
            GameObject correctRecipe = correctRecipes[i];
            GameObject droppedRecipe = droppedRecipes[i];
            
            //Comparing the sprites of each and checking to see if they match
            Sprite correctSprite = correctRecipe.GetComponent<SpriteRenderer>().sprite;
            Sprite droppedSprite = droppedRecipe.GetComponent<SpriteRenderer>().sprite;

            if(correctSprite != droppedSprite) //If the correct item does not match the dropped item
            {
                ismatch = false;
                break;
            }
         }


         if(ismatch)
         {
            Debug.Log("Potion Created");

            droppedRecipes.Clear();

            UImanager.GenerateNewPotion();
         }
         else
         {
            Debug.Log("Wrong ingredients! Try again!");
         }

       }

     }

}
