using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossRangeAttack : EnemyRangeAttack
{
    public Transform[] fire_points;
    private int fire_points_index;

    void Start()
    {}

    public override void RangeAttack()
    {
        if (Time.time >= next_time_to_fire)
        {
            Debug.Log("BOSS SHOOTING");
            animator.SetTrigger("Attack");
            next_time_to_fire = Time.time + weapon.fire_rate;
            fire_points_index = fire_points_index % fire_points.Length;

            GameObject bullet_object = (GameObject)Instantiate(bullet_prefab, 
                fire_points[fire_points_index].position, fire_points[fire_points_index].rotation);
            EnemyBullet bullet = bullet_object.GetComponent<EnemyBullet>();
            bullet.SetDirection(fire_points[fire_points_index].forward);
            bullet.SetDamage(weapon.damage);

            fire_points_index++;
        }
    }

}
