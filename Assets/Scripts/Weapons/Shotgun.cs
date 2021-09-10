using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    int pelletsPerShot;
    public Shotgun()
    {
        pelletsPerShot = 15;
        StartingMags = 3;
        MagSize = 7;
        AmmoCount = MagSize;
        AmmoInv = MagSize * StartingMags;
        BloomRange = 5f; //Do we need to pass values?
        BloomScalar = 0f;
        ReloadSpeed = 4f;
        Reloading = false;
        DamageValues = new float[] { 30f };
        DistanceValues = new float[] { 40f };
    }

    //add bloom per pellet
    public override IEnumerator Shoot(Player player)
    {
        Debug.Log("Ammo Count is: " + AmmoCount);
        if (AmmoCount != 0)
        {
            --AmmoCount;
            for (int i = 0; i < pelletsPerShot; ++i)
            {
                bool hit = Physics.Raycast(player.MainCamera.transform.position, player.MainCamera.transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, DistanceValues[0]);
                //Debug.Log(hitInfo.distance);
                if (hit && hitInfo.collider.CompareTag("Player"))
                {
                    Player otherPlayer = hitInfo.collider.GetComponentInParent<Player>();
                    otherPlayer.Damage(player, DamageValues[0]);
                }
            }
        }
        else if (!Reloading)
        {
            //Debug.Log("Else statement");
            player.StartCoroutine(Reload(player));
        }
        yield return null;
    }

    //need to override reload
}
