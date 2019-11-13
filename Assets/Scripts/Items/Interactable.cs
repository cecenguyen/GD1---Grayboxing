using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 1f;

    bool interacted = false;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public virtual void Interaction()    //Inheritance
    {
        if(interacted == false)
            interacted = true;
        Debug.Log("Interacting with " + transform.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
