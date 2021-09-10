using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public Pistol()
    {
        StartingMags = 3;
        MagSize = 12;
        AmmoCount = MagSize;
        AmmoInv = MagSize * StartingMags;
        BloomRange = 2f;
        BloomScalar = .2f;
        ReloadSpeed = 2f;
        Reloading = false;
        DamageValues = new float[] { 24f, 17f, 12f};
        DistanceValues = new float[] { 15f, 30f};
    }
    public override IEnumerator Shoot(Player player)
    {
        Debug.Log("Ammo Count is: " + AmmoCount);
        if (AmmoCount != 0)
        {
            bool hit = Physics.Raycast(player.MainCamera.transform.position, player.MainCamera.transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo);
            --AmmoCount;
            //Debug.Log(hitInfo.distance);
            if(hit && hitInfo.collider.CompareTag("Player"))
            {
                Player otherPlayer = hitInfo.collider.GetComponentInParent<Player>();
                if (hitInfo.distance <= DistanceValues[0])
                    otherPlayer.Damage(player, DamageValues[0]);
                else if (hitInfo.distance <= DistanceValues[1])
                    otherPlayer.Damage(player, DamageValues[1]);
                else
                    otherPlayer.Damage(player, DamageValues[2]);
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
