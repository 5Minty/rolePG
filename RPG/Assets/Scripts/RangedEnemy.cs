using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RangedEnemy : Mover
{
    public int xpVal = 1;
    public float triggerLength = 1;
    public float shootRadius = 5f;
    private bool shooting;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        shooting = false;
    }

    private void FixedUpdate()
    {
        // Is the player in range to shoot?
        if (Vector3.Distance(playerTransform.position, startingPosition) < shootRadius)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                shooting = true;

            if (shooting)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            shooting = false;
        }

        // Check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }


    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpVal);
        GameManager.instance.ShowText("+ " + xpVal + " head", 30, Color.blue, transform.position, Vector3.up * 40, 1.0f);
    }
}
