using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptySchest;
    public int pesosAmount = 10;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptySchest;
            GameManager.instance.ShowText("+" + pesosAmount + " pesos!", 25, Color.red, transform.position, Vector3.up * 25, 1.5f);

            GameManager.instance.pesos += pesosAmount;
        }
    }
}
