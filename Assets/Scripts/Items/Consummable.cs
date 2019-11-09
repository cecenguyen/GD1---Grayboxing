using UnityEngine;

[CreateAssetMenu(fileName = "New Consummable", menuName = "Inventory/Consummable")]
public class Consummable : Item
{
    Player player;
    private int amount_heal = 10;

    public override void Use()
    {
        base.Use();
        player = GameManager.GetPlayer();
        player.Heal(amount_heal);
        RemoveFromInventory();
    }
}
