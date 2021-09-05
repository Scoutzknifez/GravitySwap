using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class Interactable : MonoBehaviour
{
    public Color activeColor;
    public Color disabledColor;
    [Range(0f, 1f)]
    public float darkenOnHover = 1f;

    public bool isInteractable;
    public UnityEvent onInteract;
    Color hoverColor;

    private void Start()
    {
        hoverColor = Color.black;
        if (isInteractable)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = activeColor;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = disabledColor;
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
        //Called only once
        activeColor = Color.Lerp(activeColor, hoverColor, darkenOnHover);
        gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = activeColor;
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
