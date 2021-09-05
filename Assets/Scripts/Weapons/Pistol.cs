using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    int reloadSpeed;
    public override IEnumerator Shoot(Player player)
    {
        Debug.Log("You are firing");
        Physics.Raycast(player.MainCamera.transform.position, player.MainCamera.transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo);
        Debug.Log("hitInfo: " + hitInfo.distance);
        yield return null;
    }
    public override void Reload(Player player)
    {
        return;
    }
}
