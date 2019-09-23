
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Power : MonoBehaviour
{
    Text txt;
    public FloatVariable force;
    private void Start()
    {
        txt = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        txt.text = (int)((force.value / 30.0f) * 100) + "%";
    }
}
