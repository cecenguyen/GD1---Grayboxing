using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyWeapon weapon;
    private Player player;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(transform.name + " is hitting the Player");
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        if (player == null)
            player = GameManager.GetPlayer();

        if (player.isAlive == true)
            player.TakeDamage(weapon.damage);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
