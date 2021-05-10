using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class Interactable : MonoBehaviour
{
    public Color activeColor;
    public Color disabledColor;
    [Range(0f, 1f)]
    public float darkenOnHover = .5f;

    public bool isInteractable;
    public UnityEvent onInteract;

    private void Start()
    {
        if (isInteractable)
        {
            gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = activeColor;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = disabledColor;
        }
    }

    public void Interact()
    {
        if (!isInteractable)
        {
            return;
        }

        onInteract.Invoke();
    }

    public void OnHover()
    {
        if (!isInteractable)
        {
            return;
        }

        Color hoverColor = Color.Lerp(Color.black, activeColor, darkenOnHover);
        gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = hoverColor;
    }

    public void LeaveHover()
    {
        if (!isInteractable)
        {
            return;
        }

        gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = activeColor;
    }
}
