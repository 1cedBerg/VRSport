using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeOnActivate : MonoBehaviour
{
    public GameObject block;

    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;

    private Color defaultColor;
    private Material material;

    private void OnEnable()
    {
        interactable.activated.AddListener(OnActivate);
    
    }

    private void OnDisable()
    {

        interactable.deactivated.RemoveListener(OnDeactivate);
    }

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        defaultColor = material.color;
    }

    public void OnActivate(ActivateEventArgs args)
    {
        material.color = Color.red;
    }

    public void OnDeactivate(DeactivateEventArgs args)
    {
        material.color = Color.green;
    }
}
