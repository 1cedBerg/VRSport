using UnityEngine;
using TMPro;

public class MeasureDistance : MonoBehaviour
{
    public TMP_Text displayText;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ball") return;

        float dist = Vector3.Distance(Vector3.zero, collision.GetContact(0).point);

        displayText.text = "Dist: " + dist.ToString("#.00");

        Destroy(collision.gameObject);
    }
}
