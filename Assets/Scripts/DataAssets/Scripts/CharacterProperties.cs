
using UnityEngine;

[CreateAssetMenu]
public class CharacterProperties : ScriptableObject
{
    public string name;
    public GameObject weaponPrefab;
    [Range(0,5)]
    public int difficulty;
    public float health;
    public Sprite icon;
}
