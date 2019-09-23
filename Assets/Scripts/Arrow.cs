﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BowMaster.Core;

public class Arrow : WeaponController
{
    public FloatVariable force;
    public GameEvent GameOver;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = transform.right * force.value;
    }

    private void LateUpdate()
    {
        if (body.velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(body.velocity.y, body.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string _tag = collision.transform.tag;
        if (_tag == "ground" || _tag == "enemy")
        {
            this.transform.SetParent(collision.transform);
            body.isKinematic = true;
            body.velocity = Vector2.zero;
            body.angularVelocity = 0.0f;
            transform.position += this.transform.right * 0.2f;
            GetComponent<Collider2D>().enabled = false;
            camTarget._object = null;
            OnTaskFinished.Raise();
            GetComponent<SpriteRenderer>().sortingOrder = 7;
            if (_tag == "enemy")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(collision.contacts[0].point, this.transform.right);
                characterHP.ApplyChage(-1);
                if (characterHP.value <= 0)
                {
                    GameOver.Raise();
                }
            }
        }
    }

}