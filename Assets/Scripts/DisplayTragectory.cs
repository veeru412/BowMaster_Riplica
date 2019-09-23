using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTragectory : MonoBehaviour
{
    public FloatVariable force;
    public GameObject dotPrefab;
    private GameObject[] dots;
    private void Start()
    {
        dots = new GameObject[10];
        for (int i= 0; i < dots.Length; i++)
        {
            dots[i] = Instantiate(dotPrefab, transform);
        }
    }
    private void Update()
    {
        DrawProgectile(transform.position, transform.right*force.value);
    }
    private void DrawProgectile(Vector2 pos,Vector2 vel)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            vel += Physics2D.gravity * Time.fixedDeltaTime;
            pos += vel * Time.fixedDeltaTime;
            dots[i].transform.position = pos;
        }
    }
}
