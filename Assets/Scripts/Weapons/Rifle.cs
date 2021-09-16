using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Rifle : Gun
{
    int reloadSpeed;
    [SerializeField]
    Camera playerCam;
    public override IEnumerator Shoot(Player player)
    {
        while(Input.GetButton("Fire1"))
        {
            Debug.Log("You are firing fast");
            Physics.Raycast(player.MainCamera.transform.position, player.MainCamera.transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo);
            Debug.Log("hitInfo: " + hitInfo.point);
            yield return new WaitForSeconds(.1f);
        }
    }
}
*/