using UnityEngine;

public class SpawnButtonSmack : MonoBehaviour
{
    [Header("Spawn")]
    public GameObject ballPrefab;
    public Transform spawnPoint;

    [Header("Button Feel")]
    public float cooldownSeconds = 0.5f;
    public float smackSpeedThreshold = 1.0f; // tune in play mode

    private float nextAllowedTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < nextAllowedTime) return;

        // We consider it a valid press if the thing entering is moving fast enough.
        // This works well with kinematic XR objects because we estimate speed via last frame position.
        var smack = other.GetComponent<SmackVelocity>();
        if (smack == null) smack = other.GetComponentInParent<SmackVelocity>();
        if (smack == null) return;

        if (smack.CurrentSpeed < smackSpeedThreshold) return;

        SpawnBall();
        nextAllowedTime = Time.time + cooldownSeconds;
    }

    public BallSpawner spawner;

private void SpawnBall()
{
    if (spawner != null)
        spawner.SpawnBall();
}

}
