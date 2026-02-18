using System.Collections;
using UnityEngine;

public class ColorBall : MonoBehaviour
{
    [Header("Visuals")]
    public Material[] colorMaterials; // Size 4: Red, Blue, Green, Yellow
    public MeshRenderer targetRenderer;

    [Header("Timing")]
    public float changeDelay = 0.25f;
    public float hitCooldown = 0.12f;

    [Header("FX (Optional)")]
    public ParticleSystem nextColorHintPrefab;
    public ParticleSystem losePopPrefab;

    [Header("Lose Effect")]
    public float loseShrinkSeconds = 0.2f;

    public BallColor CurrentColor { get; private set; }

    private bool canRegisterHit = true;
    private bool isDead = false;

    public BallSpawner spawner;


    private void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        SetColor((BallColor)Random.Range(0, 4));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;
        if (!canRegisterHit) return;

        var face = collision.collider.GetComponent<PaddleFaceColor>();
        if (face == null) face = collision.collider.GetComponentInParent<PaddleFaceColor>();

        if (face == null) return; // ignore non-paddle collisions (walls, etc.)

        // Lock out repeated hits briefly
        StartCoroutine(HitCooldownRoutine());

        if (face.faceColor == CurrentColor)
        {
            BallColor next = PickDifferentColor(CurrentColor);

            // Hint now
            PlayHint(next);

            // Actually change after delay
            StartCoroutine(ChangeColorAfterDelay(next, changeDelay));
if (ScoreManager.Instance != null)
{
    ScoreManager.Instance.AddPoint(1);
}


        }
        else
        {
            Lose();
        }
    }

    private IEnumerator HitCooldownRoutine()
    {
        canRegisterHit = false;
        yield return new WaitForSeconds(hitCooldown);
        canRegisterHit = true;
    }

    private IEnumerator ChangeColorAfterDelay(BallColor next, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isDead) yield break;
        SetColor(next);
    }

    private BallColor PickDifferentColor(BallColor current)
    {
        BallColor next = current;
        int safety = 20;
        while (next == current && safety > 0)
        {
            next = (BallColor)Random.Range(0, 4);
            safety--;
        }
        return next;
    }

    private void SetColor(BallColor color)
    {
        CurrentColor = color;

        if (targetRenderer != null && colorMaterials != null && colorMaterials.Length >= 4)
        {
            targetRenderer.material = colorMaterials[(int)color];
        }
    }

    private void PlayHint(BallColor next)
    {
        if (nextColorHintPrefab == null) return;

        ParticleSystem ps = Instantiate(nextColorHintPrefab, transform.position, Quaternion.identity);
        var main = ps.main;

        // If your particle uses Start Color, this will tint it.
        // If your particle uses a material instead, you can swap materials similarly.
        main.startColor = ToUnityColor(next);

        ps.Play();
        Destroy(ps.gameObject, main.duration + main.startLifetime.constantMax + 0.5f);
    }

    private Color ToUnityColor(BallColor c)
    {
        if (c == BallColor.Red) return Color.red;
        if (c == BallColor.Blue) return Color.blue;
        if (c == BallColor.Green) return Color.green;
        return Color.yellow;
    }

    public void Lose()
    {
        if (isDead) return;
        isDead = true;

        if (losePopPrefab != null)
        {
            ParticleSystem ps = Instantiate(losePopPrefab, transform.position, Quaternion.identity);
            ps.Play();
            Destroy(ps.gameObject, 2f);
        }
        if (ScoreManager.Instance != null)
{
    ScoreManager.Instance.ResetCurrent();
}

        StartCoroutine(ShrinkAndDestroy());
    }

    private IEnumerator ShrinkAndDestroy()
    {
        Vector3 startScale = transform.localScale;
        float t = 0f;

        while (t < loseShrinkSeconds)
        {
            t += Time.deltaTime;
            float a = Mathf.Clamp01(t / loseShrinkSeconds);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, a);
            yield return null;
        }
        if (spawner != null)
{
    spawner.ClearBall();
}

        Destroy(gameObject);
    }
}
