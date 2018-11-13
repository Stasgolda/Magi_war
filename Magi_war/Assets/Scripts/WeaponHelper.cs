using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WeaponHelper : MonoBehaviour
{

    public int damage;
    public bool isAttack;

    private PlayerXP playerXp;
 
    void Start()
    {
        playerXp = FindObjectOfType<PlayerXP>();
        GetComponent<Rigidbody>().useGravity = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isAttack && other.tag == "Enemy")
        {
            PlayerHP health = other.GetComponent<PlayerHP>();
            if (health)
            {
                health.takeDamage(damage);
                if (health.isDead && playerXp)
                {
                    playerXp.AddXP(health.maxHP / 2f);
                }
            }
        }
    }
}
