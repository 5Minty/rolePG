using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhitbox : Collidable
{
    public int damagePt = 1;
    public float pushForce = 5f;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player") {
            damage dmg = new damage
            {
                damageAmount = damagePt,
                origin = GameManager.instance.player.transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);//What do u do?
        }
    }
}
