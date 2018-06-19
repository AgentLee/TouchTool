using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    public float dragSpeed = 0.5f;
    private Vector3 dragOrigin;
    bool hold = false;

    private bool isTranslating;
    private Vector3 prevMousePos;
    private Vector3 mouseOffset;
    private Vector3 translation;

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

    private void OnMouseUp()
    {
        isTranslating = false;
    }

    private void OnMouseDown()
    {
        if(!IsModelClicked())
        {
            return;
        }

        isTranslating = true;
        prevMousePos = Input.mousePosition;
    }

    // https://forum.unity.com/threads/click-drag-camera-movement.39513/
    private void TranslateModel()
    {
        if (Input.GetMouseButtonDown(2) /*&& Input.GetKeyDown(KeyCode.LeftControl)*/)
        {
            OnMouseDown();
        }
        else if (Input.GetMouseButtonUp(2) /*&& Input.GetKeyUp(KeyCode.LeftControl)*/)
        {
            OnMouseUp();
        }

        if(isTranslating)
        {
            //mouseOffset = Input.mousePosition - prevMousePos;
            //translation = Camera.main.ScreenToViewportPoint(mouseOffset);

            //GameObject.Find("Model").transform.Translate(translation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TranslateModel();

        if (Input.GetMouseButtonDown(1))
        {
            if (IsModelClicked())
            {
                dragOrigin = Input.mousePosition;
                return;
            }
        }

        if (!Input.GetMouseButton(1))
        {
            return;
        }

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x, 0, pos.y);
        move *= 0.4f;

        GameObject.Find("Model").transform.Translate(move);
    }
}
