
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Filler : MonoBehaviour
{
    Image img;
    public FloatVariable HP;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        img.fillAmount = HP.value / 3;
    }
}
