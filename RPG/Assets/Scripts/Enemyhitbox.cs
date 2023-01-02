using UnityEngine;

public class Enemyhitbox : Collidable
{
    public int damagePt = 1;
    public float pushForce = 5f;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player") {
            Damage dmg = new Damage
            {
                damageAmount = damagePt,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);//What do u do?
        }
    }
}
