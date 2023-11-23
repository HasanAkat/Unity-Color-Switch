using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotatingSpeed = 100f;

    void Update()
    {
        transform.Rotate(0f,0f, rotatingSpeed *  Time.deltaTime);
        
    }
}
