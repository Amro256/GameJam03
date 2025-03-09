using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    [Header("Arrays")]
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
    [SerializeField] float timeLeft = 20f; //
    private bool isTimerRunning = false;

    [Header("Other settings")]
    [SerializeField] TMP_Text TextFeedback;
    public Animator textAni;


    private List<GameObject> correctRecipes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Method here that will randomise the final item
        randomiseItemAndRecipe();
        isTimerRunning = true; //Set the timer is running to true once the game starts
    }

    void Update()
    {
        //Timer code
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
                GameManager.instance.TimeOverScreen();
                isTimerRunning = false;
            }
        }
    }


    //Method to randmoise the Item and Recipes and display correctly in the UI
    private void randomiseItemAndRecipe()
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

                if(!chosenRecipes.Contains(selectedRecipe)) //Avoid duplicates
                {
                    chosenRecipes.Add(selectedRecipe);
                }
            }

            correctRecipes.AddRange(chosenRecipes);

            //Assigned each recipe image with the choosen 4 recipes
            assignRecipeImage(Recipe1Image, chosenRecipes[0]);
            assignRecipeImage(Reciep2Image, chosenRecipes[1]);
            assignRecipeImage(Recipe3Image, chosenRecipes[2]);
            assignRecipeImage(Recipe4Image, chosenRecipes[3]);
    }

    //Timer method 
    void displayTimer(float time)
    {
        float seconds = Mathf.FloorToInt(time % 60);
        float mileseconds = (time % 1) * 100;

        timerText.text = string.Format("{0:00} : {1:00}", seconds, mileseconds); 
    }


    // Method that will assign the recipie images to the UI element in Game
    private void assignRecipeImage(RawImage recipeImage, GameObject recipeObject)
    {
        SpriteRenderer recipeSR = recipeObject.GetComponent<SpriteRenderer>();

        if(recipeSR != null) //Checks if the renderer is not null
        {
            recipeImage.texture = recipeSR.sprite.texture;
        }
    }


    //Method that will return the correct recipes
    public List<GameObject> GetCorrectRecipe()
    {
        return correctRecipes;
    }

    public void GenerateNewPotion()
    {
        Debug.Log("Generate new potion");
        randomiseItemAndRecipe();
    }


    public void DisplayTextFeedback(string itemname)
    {
        TextFeedback.color = Color.green;
        TextFeedback.text = itemname + " dropped";
        textAni.SetTrigger("FadeIn");

        StartCoroutine(HideTextFeedback());
    }

    public void DisplayPotionFeedback(string message)
    {
        TextFeedback.text = message;
        TextFeedback.color = Color.red;
        textAni.SetTrigger("FadeIn");

        StartCoroutine(HideTextFeedback());
    }

    private IEnumerator HideTextFeedback()
    {
        yield return new WaitForSeconds(0.25f);
        textAni.SetTrigger("FadeOut");
    }
}
