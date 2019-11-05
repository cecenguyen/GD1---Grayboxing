using UnityEngine;

public class ItemPickup : Interactable
{
    public override void Interaction()
    {
        base.Interaction();
        Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picking up " + transform.name);

        //TODO: Effect of item

        Destroy(gameObject);
    }

    void Effect()
    {

    }
}
