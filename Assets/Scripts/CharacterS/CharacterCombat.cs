using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;
    public float attackCooldown = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCooldown >= 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public void Attack(CharacterStats target)
    {
        if (attackCooldown < 0.0f)
        {
            AudioManager.instance.Play("Attack");
            if (target.gameObject.tag=="Player") { AudioManager.instance.Play("Attack"); }
            target.TakeDamage(myStats.damage.getValue());
            attackCooldown = 1/myStats.attackSpeed.getValue(); 
        }
    }
}
