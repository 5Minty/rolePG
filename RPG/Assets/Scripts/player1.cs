using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Mover
{
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        var objs = FindObjectsOfType(typeof(Player1));

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));

        
    }
    public void SwapSprite(int skindId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skindId];
    }
}
