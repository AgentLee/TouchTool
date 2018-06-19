using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder : MonoBehaviour
{
    public GameObject model;

    Bounds modelBounds;
    GameObject plane;

	// Use this for initialization
	void Start ()
    {
        model = this.gameObject;
        modelBounds = GetBounds(model);

        // Spawn a plane
	    plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        // Need to test this out 
        plane.transform.localScale = modelBounds.size / 10;
        // Set the plane to be underneath the model
        Vector3 pos = new Vector3(model.transform.position.x, -model.GetComponent<MeshFilter>().mesh.bounds.size.y / 2, model.transform.position.z);
        plane.transform.position = pos;
        plane.transform.parent = model.transform;
    }
	
    private Bounds GetBounds(GameObject model)
    {
        Bounds bounds = new Bounds(model.transform.position, Vector3.one);
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }

	// Update is called once per frame
	void Update ()
    {

    }
}
