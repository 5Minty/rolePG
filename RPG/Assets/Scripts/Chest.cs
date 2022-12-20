using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public int headAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GameManager.instance.ShowText("+" + headAmount + " head!", 25, Color.yellow, transform.position, Vector3.up * 50, 2.0f);
            Debug.Log("Give " + headAmount + " head");

        }

    }
}
