using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BowMaster.Core
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject weaponPrefab;
        public FloatVariable force;
        public ObjVar camTarget;
        public Transform torso;
        public Transform weaponPos;
        public float rotSpeed;
        public float forceMul;
        public FloatVariable m_Hp;
        public Text angelTxt;
        public GameObject _ui;
        Animation anim;
        bool myTurn = false;
        Rigidbody2D body;

        private void Start()
        {
            anim = GetComponent<Animation>();
            body = GetComponent<Rigidbody2D>();
            m_Hp.SetValue(3);
#if UNITY_ANDROID || UNITY_IOS
            rotSpeed = 25.0f;
                forceMul = 6.0f;
#endif
        }

        private void Update()
        {
            if (myTurn)
            {
#if UNITY_ANDROID || UNITY_IOS
            
                OnMobile();
#else
                OnEditor();
#endif
            }
        }
        private void OnEditor()
        {
            if (Input.GetMouseButton(0))
            {
                float y = Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
                torso.Rotate(-Vector3.forward * y);
                force.ApplyChage(-Input.GetAxis("Mouse X") * forceMul);
                force.SetValue(Mathf.Clamp(force.value, 0.0f, 30.0f));
                angelTxt.text = "" + (int)torso.transform.localEulerAngles.z+"^";
            }
            if (Input.GetMouseButtonUp(0))
            {
                Fire();
            }
        }
        private void Fire()
        {
            GameObject arrow = Instantiate(weaponPrefab, weaponPos.position, weaponPos.rotation);
            camTarget._object = arrow.transform;
            weaponPos.gameObject.SetActive(false);
            myTurn = false;
            anim.Play("settle");
            _ui.SetActive(false);
        }
        public void Reset()
        {
            angelTxt.text = "0^";
            StartCoroutine(CheckForStandUp());
        }
        IEnumerator CheckForStandUp()
        {
            force.SetValue(0.0f);
            camTarget._object = this.transform;
            yield return new WaitUntil(body.IsSleeping);
            if (UIManager.gameOver)
                yield break;
            body.isKinematic = true;
            body.velocity = Vector3.zero;
            body.angularVelocity = 0.0f;
            float _dot = Vector3.Dot(transform.up, Vector3.up);
            if (_dot < 0.8f)
            {
                float progress = 0.0f;
                Vector3 startRot = transform.eulerAngles;
                while (progress < 1)
                {
                    progress += Time.deltaTime * 2.0f;
                    transform.eulerAngles = Vector3.Lerp(startRot, Vector3.zero, progress);
                    transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, -0.5f, progress), transform.position.z);
                    yield return null;
                }
                body.isKinematic = false;
                yield return new WaitForSeconds(0.5f);
                anim.Play("AIM2");
                myTurn = true;
            }
            else
            {
                anim.Play("AIM2");
                myTurn = true;
                body.isKinematic = false;
            }
            _ui.SetActive(true);
        }

        private void OnMobile()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    torso.Rotate(-Vector3.forward * touchDeltaPosition.y*rotSpeed*Time.deltaTime);
                    force.ApplyChage(-touchDeltaPosition.x * forceMul* Time.deltaTime);
                    force.SetValue(Mathf.Clamp(force.value, 0.0f, 30.0f));
                    angelTxt.text = "" + (int)torso.transform.localEulerAngles.z + "^";
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    Fire();
                }
            }

        }
    }
}

