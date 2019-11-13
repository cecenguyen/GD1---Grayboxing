using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyWeapon weapon;
    private Player player;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); 
    }

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
        animator.SetTrigger("Attack");

        if (player == null)
            player = GameManager.GetPlayer();

        if (player.isAlive == true)
            player.TakeDamage(weapon.damage);
    }
}
