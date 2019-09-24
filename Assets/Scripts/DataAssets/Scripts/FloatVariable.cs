  
using UnityEngine;

    [CreateAssetMenu]
    public class FloatVariable : ScriptableObject
    {
        [Multiline]
        public string discription;

        public float value;

        public void SetValue(float amount)
        {
            value = amount;
        }
        public void ApplyChage(float amount)
        {
            value += amount;
        }
    }

