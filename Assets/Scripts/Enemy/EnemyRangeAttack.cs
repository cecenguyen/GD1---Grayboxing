using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    public EnemyRangeWeapon weapon;

    public GameObject bullet_prefab;
    public Transform fire_point;

    private float next_time_to_fire = 0f;

    public void RangeAttack()
    {
        if (Time.time >= next_time_to_fire)
        {
            next_time_to_fire = Time.time + weapon.fire_rate;
            GameObject bullet_object = (GameObject)Instantiate(bullet_prefab, fire_point.position, fire_point.rotation);
            EnemyBullet bullet = bullet_object.GetComponent<EnemyBullet>();
            bullet.SetDirection(fire_point.forward);
            bullet.SetDamage(weapon.damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
