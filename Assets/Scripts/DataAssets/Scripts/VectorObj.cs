
using UnityEngine;


    [CreateAssetMenu]
    public class VectorObj : ScriptableObject
    {
        [Multiline]
        public string discription;

        public Vector2 value;

        public void SetValue(Vector2 amount)
        {
            value = amount;
        }
        public void ApplyChage(Vector2 amount)
        {
            value += amount;
        }
    }


