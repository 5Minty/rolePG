using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hp = 10;
    public int maxHp = 10;
    public float pushRecoverySpeed = .2f;

    //Immunity
    public float immuneT = 1.0f;
    public float lastImmune;

    //Push
    protected Vector3 pushDirection;

    //All fighters can ReceiveDamage / Die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneT)
        {
            lastImmune = Time.time;
            hp -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, .5f);

            if(hp <= 0)
            {
                hp = 0;
                Death();
            }

        }
    }

    protected virtual void Death()
    {

    }
}
