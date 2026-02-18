using UnityEngine;

public class LoseOnTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var ball = collision.collider.GetComponent<ColorBall>();
        if (ball == null) ball = collision.collider.GetComponentInParent<ColorBall>();
        if (ball == null) return;

        ball.Lose();
    }

    private void OnTriggerEnter(Collider other)
    {
        var ball = other.GetComponent<ColorBall>();
        if (ball == null) ball = other.GetComponentInParent<ColorBall>();
        if (ball == null) return;

        ball.Lose();
    }
}
