using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cauldron : MonoBehaviour
{

    private List<GameObject> droppedRecipes = new List<GameObject>();
    private UIManager UImanager;
    // Start is called before the first frame update
    void Start()
    {
        UImanager = FindObjectOfType<UIManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Recipe"))
        {
            if(other.gameObject != null)
            {
            droppedRecipes.Add(other.gameObject);
            Debug.Log("Dropped " + other.gameObject.name);
            //Destroy(other.gameObject);

            if(droppedRecipes.Count == 4)
            {
                checkRecipes();
            }
            }
            
        }
    }

    private void checkRecipes()
    {
       List<GameObject> correctRecipes = UImanager.GetCorrectRecipes();


       // Debugging: log the correct recipes and their sprites
    Debug.Log("Correct Recipes:");
    foreach (var recipe in correctRecipes)
    {
        Debug.Log(recipe.name + " Sprite: " + recipe.GetComponent<SpriteRenderer>().sprite.name);
    }


       if(correctRecipes.Count == 4 && droppedRecipes.Count == 4)
       {
         bool ismatch = true;

         for(int i = 0; i < correctRecipes.Count; i++)
         {
            GameObject correctRecipe = correctRecipes[i];
            GameObject droppedRecipe = droppedRecipes[i];
            
            Sprite correctSprite = correctRecipe.GetComponent<SpriteRenderer>().sprite;
            Sprite droppedSprite = droppedRecipe.GetComponent<SpriteRenderer>().sprite;

            if(correctSprite != droppedSprite)
            {
                ismatch = false;
                break;
            }
         }


            if(ismatch)
            {
                Debug.Log("Potion created!");
            }
            else
            {
                Debug.Log("Wrong ingredients! Try again!");
            }

       }


     }

}
