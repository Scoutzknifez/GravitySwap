using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Camera mainCamera;
    public float interactionDistance = 3.5f;

    private GameObject recentHit = null;


    void Update()
    {
        CheckIfInteractableInForwardView();
        ListenForInteractKey();
    }

    void CheckIfInteractableInForwardView()
    {
        int interactableLayer = LayerMask.GetMask("Interactable");

        bool hit = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit rayHitInfo, interactionDistance, interactableLayer);

        if (hit)
        {
            if (recentHit == null)
            {
                recentHit = rayHitInfo.collider.gameObject;
                recentHit.GetComponent<Interactable>().OnHover();
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
                    recentHit.GetComponent<Interactable>().LeaveHover();
                    recentHit = rayHitInfo.collider.gameObject;
                    recentHit.GetComponent<Interactable>().OnHover();
                }
            }
        }
        else
        {
            if (recentHit != null)
            {
                recentHit.GetComponent<Interactable>().LeaveHover();
                recentHit = null;
            }
        }
    }

    void ListenForInteractKey()
    {
        if (Input.GetKeyDown("e") && recentHit != null)
        {
            recentHit.GetComponent<Interactable>().Interact();
        }
    }
}
