
using UnityEngine;

[CreateAssetMenu]
public class ObjVar : ScriptableObject
{
    private Transform _value;
    public Transform _object
    {
        get { return _value; }
        set { _value = value; }
    }
}
