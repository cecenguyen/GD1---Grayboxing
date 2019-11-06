using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    // Start is called before the first frame update
    new public string name = "New Item";
    public Sprite icon = null;
    public bool is_default_item = false;
}
