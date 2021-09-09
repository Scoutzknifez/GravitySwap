using UnityEngine;
using UnityEngine.Events;
public class Interaction : MonoBehaviour
{
    public Camera mainCamera;
    public float interactionDistance = 100f;
    private GameObject recentHit = null;
    Color defaultColor;

    [SerializeField]
    Color hoverColor;

    void Update()
    {
        CheckIfInteractableInForwardView();
        ListenForInteractKey();
    }

    void CheckIfInteractableInForwardView()
    {
        int interactableLayer = LayerMask.GetMask("Interactable");
        bool hit = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out RaycastHit rayHitInfo, interactionDistance, interactableLayer);

        if (hit)
        {
            if (recentHit == null)
            {
                recentHit = rayHitInfo.collider.gameObject;
                OnHover(recentHit);
                return;
            }
            else
            {
                // recentHit != null
                if (recentHit.Equals(rayHitInfo.collider.gameObject))
                {
                    // Its the same object, dont do anything
                    return;
                }
                else
                {
                    // It is a new object in view
                    LeaveHover(recentHit);
                    recentHit = rayHitInfo.collider.gameObject;
                    OnHover(recentHit);
                }
            }
        }
        else
        {
            if (recentHit != null)
            {
                LeaveHover(recentHit);
                recentHit = null;
            }
        }
    }

    void ListenForInteractKey()
    {
        if (Input.GetKeyDown("e") && recentHit != null)
        {
            recentHit.GetComponent<IInteractable>().Interact();
        }
    }


    public void OnHover(GameObject interactable)
    {
        //Called only once
        defaultColor = interactable.GetComponent<MeshRenderer>().material.color;
        interactable.GetComponent<MeshRenderer>().material.color = hoverColor;
    }

    public void LeaveHover(GameObject interactable)
    {
        interactable.GetComponent<MeshRenderer>().material.color = defaultColor;
    }
}
