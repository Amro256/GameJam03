using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject[] Potions; //An array to hold all of the Potions sprites     

    [SerializeField] Image canvasImage; //Refernece to the Image component of the canvas -- Make this Private after testing)

    [SerializeField] TMP_Text timerText;
    [SerializeField] float timeLeft = 20f;
    private bool isTimerRunning = false;

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

        SpriteRenderer sr = selectedPotion.GetComponent<SpriteRenderer>();

        if(sr != null) //If the sprite renderer is not null, show the selected potion sprite
        {
            canvasImage.sprite = sr.sprite;
        }
    }

    void displayTimer(float time)
    {
        float seconds = Mathf.FloorToInt(time % 60);
        float mileseconds = (time % 1) * 100;

        timerText.text = string.Format("{0:00} : {1:00}", seconds, mileseconds); 
    }
}
