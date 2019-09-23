using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public CharacterProperties m_Properties;
    public Transform weaponPos;
    public ObjVar camTarget;
    public FloatVariable m_Hp;

    private Animator anim;
    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        m_Hp.SetValue(m_Properties.health);
    }

    public void EnemyTurn()
    {
        StartCoroutine(WaitAndAim());
    }
    IEnumerator WaitAndAim()
    {
        camTarget._object = this.transform;
        yield return new WaitUntil(body.IsSleeping);
        if (UIManager.gameOver)
            yield break;
        float _dot = Vector3.Dot(this.transform.up, Vector3.up);
        if(_dot < 0.8f)
        {
            body.isKinematic = true;
            body.velocity = Vector3.zero;
            float progress = 0.0f;
            
            Vector3 startRot = transform.eulerAngles;
            while(progress < 1)
            {
                progress += Time.deltaTime*2.0f;
                transform.eulerAngles = Vector3.Lerp(startRot, Vector3.zero, progress);
                transform.position = new Vector3(transform.position.x,Mathf.Lerp(transform.position.y,-0.5f,progress),transform.position.z);
                yield return null;
            }
            body.isKinematic = false;
        }
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("shoot");
    }
    public void ThroughWeapon()
    {
        GameObject weapon = Instantiate(m_Properties.weaponPrefab, weaponPos.position, weaponPos.rotation);
        camTarget._object = weapon.transform;
    }
}
