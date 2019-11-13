using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    RectTransform hp_fill;

    private Player player;
    
    void SetHpAmount(float amount)
    {
        hp_fill.localScale = new Vector3(amount, 1f, 1f);
    }

    void Update()
    {
        if(player == null)
            player = GameManager.GetPlayer();
        SetHpAmount(player.GetHealthPct());
    }
}
