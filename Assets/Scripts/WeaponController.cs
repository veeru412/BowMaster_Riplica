using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BowMaster.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WeaponController : MonoBehaviour
    {
        [HideInInspector]
        public Rigidbody2D body;

        public ObjVar camTarget;

        public GameEvent OnTaskFinished;

        public FloatVariable characterHP;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

       
    }
}

