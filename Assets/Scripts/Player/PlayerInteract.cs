using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private float interact_range = 2f;
    void Start()
    {
        if (camera == null)
        {
            Debug.LogError("No camera reference in PlayerInteract");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        RaycastHit hit;

        //Raycast syntax: origin, direction, hitinfo, maxdistance, what we should hit
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, interact_range, mask))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                interactable.Interaction(); //Interaction of each individual item
            }
        }
    }
}
