using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Sniper : Gun
{
    public Sniper()
    {
        StartingMags = 5;
        MagSize = 4;
        AmmoCount = MagSize;
        AmmoInv = MagSize * StartingMags;
        BloomRange = 5f; //Do we need to pass values?
        BloomScalar = 0f;
        ReloadSpeed = 4f;
        Reloading = false;
        DamageValues = new float[] { 150f, 70f };
    }
    public override IEnumerator Shoot(Player player)
    {
        Debug.Log("Ammo Count is: " + AmmoCount);
        if (AmmoCount != 0)
        {
            bool hit = Physics.Raycast(player.MainCamera.transform.position, player.MainCamera.transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo);
            --AmmoCount;
            //Debug.Log(hitInfo.distance);
            if (hit && hitInfo.collider.CompareTag("Player"))
            {
                Player otherPlayer = hitInfo.collider.GetComponentInParent<Player>();
                //check the collider obj for what part of the body
                if (hitInfo.collider.gameObject.name.Contains("Leg") || hitInfo.collider.gameObject.name.Contains("Thigh"))
                    otherPlayer.Damage(player, DamageValues[1]);
                else
                    otherPlayer.Damage(player, DamageValues[0]);
            }
        }
        else if (!Reloading)
        {
            //Debug.Log("Else statement");
            player.StartCoroutine(Reload(player));
        }
        yield return null;
    }
}
