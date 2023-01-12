using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FireSpell : Collidable
{
    //Damage struct
    public int[] damagePt = { 2, 4, 5, 6, 7, 8 };
    public float[] pushForce = { 2.0f, 2.2f, 2.4f, 2.6f, 3.0f, 3.2f };

    //Upragde
    public int weaponLvl = 0;
    public SpriteRenderer spriteRenderer;

    //Swing
    private Animator anim;
    private float cooldown = .5f;
    private float lastCast;

    protected override void Start()
    {
        base.Start();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastCast > cooldown)
            {
                lastCast = Time.time;
                Cast();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {

        if (coll.tag == "Fighter")
        {
            if (coll.name != "Player")
                return;

            //New damage object that we'll send to fighter that we hit
            Damage dmg = new Damage
            {
                damageAmount = damagePt[weaponLvl],
                origin = transform.position,
                pushForce = pushForce[weaponLvl],
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }

    }

    private void Cast()
    {
        Debug.Log("Cast");
        anim.SetTrigger("cast");
    }

    public void UpgradeWeapon()
    {
        GameManager.instance.head -= GameManager.instance.weaponPrices[weaponLvl];
        weaponLvl++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLvl];

        //Change weapon stats
    }

    public void SetWeaponLevel(int level)
    {
        weaponLvl = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLvl];
    }

}
