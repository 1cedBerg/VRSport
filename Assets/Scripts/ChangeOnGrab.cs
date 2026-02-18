using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeOnGrab : MonoBehaviour
{
public GameObject sphere;

public void OnGrab(SelectEnterEventArgs args)
{
    sphere.SetActive(false);
    args.interactableObject.transform.GetComponent<MeshRenderer>().material.color = Color.red;
}


public void OnDrop(SelectEnterEventArgs args)
{
    sphere.SetActive(true);   
    args.interactableObject.transform.GetComponent<MeshRenderer>().material.color = Color.gray;
}
}