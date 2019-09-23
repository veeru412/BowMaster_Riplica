
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Counter : MonoBehaviour
{
    public FloatVariable HP;
    Image img;

    void Start()
    {
        img = GetComponent<Image>();
    }

    void FixedUpdate()
    {
        img.fillAmount = HP.value / 3.0f;
    }
}
