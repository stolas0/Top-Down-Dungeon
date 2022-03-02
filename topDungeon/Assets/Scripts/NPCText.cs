using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCText : Collidable
{

    public string message;

    private float cooldown = 4f;
    private float lastShown;


    protected override void Start()
    {
        base.Start();
        lastShown = -cooldown;    
    }


    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastShown > cooldown)
        {
            lastShown = Time.time;

            var textPos = transform.position + new Vector3(0, GetComponent<BoxCollider2D>().bounds.extents.y, 0);
            GameManager.instance.ShowText(message, 15, Color.white, textPos, Vector3.zero, cooldown);
        }

    }
}
