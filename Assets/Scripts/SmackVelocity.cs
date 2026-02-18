using UnityEngine;

public class SmackVelocity : MonoBehaviour
{
    public float CurrentSpeed { get; private set; }

    private Vector3 lastPos;

    private void OnEnable()
    {
        lastPos = transform.position;
        CurrentSpeed = 0f;
    }

    private void LateUpdate()
    {
        float dt = Time.deltaTime;
        if (dt <= 0f) return;

        Vector3 delta = transform.position - lastPos;
        CurrentSpeed = delta.magnitude / dt;
        lastPos = transform.position;
    }
}
