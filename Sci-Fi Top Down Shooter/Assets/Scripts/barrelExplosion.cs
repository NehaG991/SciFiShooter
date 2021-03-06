﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barrelExplosion : Alien
{

    private bool exploding;
    private bool done;
    public Animator anim;
    public Image content;
    private Alien alien;

    // Start is called before the first frame update
    void Start()
    {

        // gets the animator commpent, playerHP, and bools to check the the current state of the barrel
        anim = GetComponent<Animator>();
        content = GameObject.Find("content").GetComponent<Image>();
        this.exploding = false;
        this.done = false;

    }

    void Update()
    {
        // changes the bool based on if the barrel is exploding, done exploding, or didn't exploded yet
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("barrelExplosion"))
        {
            this.exploding = true;
        } 
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("barrel - final"))
        {
            enabled = false;
            this.exploding = false;
        }
        else
        {
            this.exploding = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        // if a bullet collides with the barrel, it sets the animator to the exploding animation and changes the bool
        if (exploding == false && done == false)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                exploding = true;
                anim.SetBool("exploding", true);

            }
        }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if hte barrel is exploding and a alien or the player collides with its collider the objects lose health
        if (this.exploding)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                content.fillAmount -= 0.15f;
                collision.attachedRigidbody.angularVelocity = 0f;
            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
                alien = collision.GetComponent<Alien>();
                alien.HealthAmount -= 0.5f;
                Debug.Log(alien.HealthAmount);
            }
        }


    }


}
