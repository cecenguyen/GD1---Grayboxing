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


        if(Inventory.instance.Add(item))
            Destroy(gameObject);
    }

    void Start()
    {
    }
    
    void Update()
    {
        transform.Rotate(Vector3.up * 30f * Time.deltaTime);
    }
}
