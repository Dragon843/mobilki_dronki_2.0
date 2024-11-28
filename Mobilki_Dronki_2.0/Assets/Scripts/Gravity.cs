using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float Y = 9.81f;
    void Start()
    {
        Physics.gravity = new Vector3(0, Y * -1, 0);
    }
}
