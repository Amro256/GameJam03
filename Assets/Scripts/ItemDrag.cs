using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrag : MonoBehaviour
{

    //Variables / Bools

    private bool isDragging = false;
    Vector3 offset;


    // Update is called once per frame
    void Update()
    {
        if(isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }


    private void OnMouseDown() //If the left mouse button is being pressed (and held down), set is dragging to true 
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseUp() //If the left mouse button was released, set is dragging to false
    {
        isDragging = false;
    }
}
