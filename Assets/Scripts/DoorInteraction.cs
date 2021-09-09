using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField]
    bool isInteractable = true;
    [SerializeField]
    [Tooltip("Doors don't have correct pivot so pass one to them.")]
    Transform rotatePosition;

    int rotationConstant = 5;
    int targetRotation = 90;
    [SerializeField]
    Color startColor;

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = startColor;
    }

    public void Interact()
    {
        StartCoroutine(RotateDoor());
    }
    // Start is called before the first frame update

    IEnumerator RotateDoor()
    {
        targetRotation = -targetRotation;
        rotationConstant = -rotationConstant;
        int yRotation = (int)rotatePosition.rotation.y;
        Debug.Log(yRotation + " " + targetRotation + " " + rotationConstant);
        while (yRotation != targetRotation)
        {
           yRotation += rotationConstant;
           rotatePosition.Rotate(new Vector3(0, rotationConstant, 0));
            yield return null;
        }

    }
}
