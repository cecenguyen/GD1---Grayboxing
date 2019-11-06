using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interaction()
    {
        base.Interaction();
        Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picking up " + item.name);

        //TODO: Effect of item

        Destroy(gameObject);
    }

    void Effect()
    {

    }
}
