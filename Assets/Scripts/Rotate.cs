using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://answers.unity.com/questions/177391/drag-to-rotate-gameobject.html
public class Rotate : MonoBehaviour
{
    private float sensitivity;
    private Vector3 prevMousePos;
    private Vector3 mouseOffset;
    private Vector3 rotation;
    private bool isRotating;

    // Use this for initialization
    void Start()
    {
        sensitivity = 0.4f;
        rotation = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        RotateModel();
    }

    private bool IsModelClicked()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name == "Model")
            {
                return true;
            }
        }

        return false;
    }

    private void OnMouseDown()
    {
        //if(!IsModelClicked())
        //{
        //    return;
        //}

        isRotating = true;
        // Store mouse position
        prevMousePos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        isRotating = false;
    }

    private void RotateModel()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }

        if (isRotating)
        {
            // Find distance mouse traveled
            mouseOffset = (Input.mousePosition - prevMousePos);
            // Rotate about y axis
            rotation.y = -(mouseOffset.x + mouseOffset.y) * sensitivity;
            // Rotate model
            GameObject.Find("Model").transform.Rotate(rotation);
            // Store for next rotation
            prevMousePos = Input.mousePosition;
        }
    }
}
