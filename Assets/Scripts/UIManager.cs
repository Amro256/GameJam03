using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    [Header("Array")]
    [SerializeField] GameObject[] Potions; //An array to hold all of the Potions sprites   
    [SerializeField] GameObject[] Recipies; //An array to hold all of the possible recipies  


    [Header("UI Image slots")]
    [SerializeField] Image PotionImage; //Refernece to the Image component of the canvas -- Make this Private after testing)
    [SerializeField] RawImage Recipe1Image;
    [SerializeField] RawImage Reciep2Image;
    [SerializeField] RawImage Recipe3Image;
    [SerializeField] RawImage Recipe4Image;


    [Header("Time settings")]
    [SerializeField] TMP_Text timerText;
    [SerializeField] float timeLeft = 20f;
    private bool isTimerRunning = false;


     private List<GameObject> correctRecipes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Method here that will randomise the final item
        randomItem();
        isTimerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimerRunning)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                displayTimer(timeLeft);
            }
            else
            {
                Debug.Log("You failed!");
                timeLeft = 0f;
                isTimerRunning = false;
            }
        }
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

        SpriteRenderer potionSR = selectedPotion.GetComponent<SpriteRenderer>();

        if(potionSR != null) //If the sprite renderer is not null, show the selected potion sprite
        {
            PotionImage.sprite = potionSR.sprite;
        }

        correctRecipes.Clear();
         List<GameObject> chosenRecipes = new List<GameObject>();
                   

            while(chosenRecipes.Count < 4)
            {
                GameObject selectedRecipe = Recipies[Random.Range(0, Recipies.Length)];

                if(!chosenRecipes.Contains(selectedRecipe))
                {
                    chosenRecipes.Add(selectedRecipe);
                }
            }

            correctRecipes.AddRange(chosenRecipes);


             // Debugging: log the chosen recipes
    Debug.Log("Correct Recipes Assigned:");
    foreach (var recipe in chosenRecipes)
    {
        Debug.Log(recipe.name);
    }


            assignRecipeImage(Recipe1Image, chosenRecipes[0]);
            assignRecipeImage(Reciep2Image, chosenRecipes[1]);
            assignRecipeImage(Recipe3Image, chosenRecipes[2]);
            assignRecipeImage(Recipe4Image, chosenRecipes[3]);
        

    }

    void displayTimer(float time)
    {
        float seconds = Mathf.FloorToInt(time % 60);
        float mileseconds = (time % 1) * 100;

        timerText.text = string.Format("{0:00} : {1:00}", seconds, mileseconds); 
    }


    private void assignRecipeImage(RawImage recipeImage, GameObject recipeObject)
    {
        SpriteRenderer recipeSR = recipeObject.GetComponent<SpriteRenderer>();

        if(recipeSR != null)
        {
            recipeImage.texture = recipeSR.sprite.texture;

            Debug.Log("Assigned sprite: " + recipeSR.sprite.name + " to UI image.");
        }
    }

    public List<GameObject> GetCorrectRecipes()
{
    return correctRecipes;
}
}
