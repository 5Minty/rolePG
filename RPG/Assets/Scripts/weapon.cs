using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : Collidable
{
    //Damage struct
    public int damagePt = 1;
    public float pushForce = 2.0f;

    //Upragde
    public int weaponLvl = 0;
    private SpriteRenderer spriteRenderer;

    //Swing
    private float cooldown = .5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {

        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;

            //New damage object that we'll send to fighter that we hit
            damage dmg = new damage
            {
                damageAmount = damagePt,
                origin = transform.position,
                pushForce = pushForce
            };
                
            coll.SendMessage("ReceiveDamage", dmg);
            Debug.Log(coll.name);

          
        }
        
    }

    private void Swing()
    {
        Debug.Log("Swing");
    }

}
