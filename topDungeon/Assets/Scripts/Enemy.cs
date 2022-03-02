using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Experience
    public int xpValue = 1;

    //Logic
    public float triggerLength = 1.5f;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //Hitbox
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;

    protected override void Start()
    {
        base.Start();

        playerTransform = GameObject.Find("Player").transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        var distance = Vector3.Distance(playerTransform.position, startingPosition);

        //is the player near with an enemy
        if (distance < chaseLength)
        {
            if (distance < triggerLength)
                chasing = true;


            if (chasing)
            {
                if (!collidingWithPlayer)
                    UpdateMotor((playerTransform.position - transform.position).normalized);
            }
            else
            {
                UpdateMotor(startingPosition - transform.position); //return to start position
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position); //return to start position
            chasing = false;
        }

        //check overlaps
        collidingWithPlayer = false;
        // collision work
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].name == "Player")
                collidingWithPlayer = true;

            hits[i] = null;
        }

    }


    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue.ToString() + " XP", 25, Color.blue, transform.position, Vector3.up * 25, 1.5f);
    }
}
