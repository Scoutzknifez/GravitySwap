using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDebugging : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    [Tooltip("The speed at which the player moves in any given direction")]
    float speed = 100f;

    private CharacterController player;
    private Vector3 moveVector;

    private void Start()
    {
        player = gameObject.GetComponent<CharacterController>();
        speed *= Time.fixedDeltaTime / 10f;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Flight");

        moveVector = transform.right * x + transform.forward * z + transform.up * y;
        moveVector *= speed;
        player.Move(moveVector);
    }
}
