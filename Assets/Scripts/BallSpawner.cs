using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;

    private GameObject currentBall;

    public void SpawnBall()
    {
        // Prevent multiple balls at once
        if (currentBall != null) return;

        currentBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        // Tell the ball who spawned it so it can notify on death
        ColorBall ballScript = currentBall.GetComponent<ColorBall>();
        if (ballScript != null)
        {
            ballScript.spawner = this;
        }
    }

    public void ClearBall()
    {
        currentBall = null;
    }
}
