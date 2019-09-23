using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BowMaster.Core;

public class AIWeaponCntrl : WeaponController
{
    public CharacterProperties parentPrpts;
    public Transform child;
    public GameEvent GameOver;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        Vector3 target = GameObject.FindWithTag("Player").transform.position;

        float firingAngle = Random.Range(15.0f, 50.0f);

        Vector3 posOffest = Vector3.right*GetXOffsetPos();
        posOffest.y = Random.Range(-2.0f, 1.0f);
        float target_Distance = Vector3.Distance(transform.position , target+posOffest);
        Vector2 direction = ((target +posOffest) - transform.position ).normalized;

        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / (Physics2D.gravity.y * -1));

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        Vector2 vel = new Vector2(Vx * direction.x, Vy);

        body.AddForce(vel, ForceMode2D.Impulse);
    }

    private void LateUpdate()
    {
        if (body.velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(body.velocity.y, body.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (child != null)
            {
                Vector2 pos = child.localPosition;
                pos.y = Mathf.Sin(Time.time*18f) * 0.3f;
                child.localPosition = pos;
            }
        }
      
    }

    float GetXOffsetPos()
    {
        float xOffset = 0;
        int rNo = Random.Range(1, 6);
        if(parentPrpts.difficulty < rNo)
        {
            xOffset = Random.Range(6.0f, 15.0f);
            xOffset = Random.Range(0, 10) > 6 ? xOffset : -xOffset;
        }
        return xOffset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string _tag = collision.transform.tag;
        if (_tag == "ground" || _tag == "Player")
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
            if (_tag == "Player")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(collision.contacts[0].point, this.transform.right);
                characterHP.ApplyChage(-1);
                if(characterHP.value <= 0)
                {
                    GameOver.Raise();
                }
            }
        }
    }

}
