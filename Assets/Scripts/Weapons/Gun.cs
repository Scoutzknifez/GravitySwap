using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public abstract class Gun
{
    // Start is called before the first frame update
    public abstract IEnumerator Shoot(Player player);
    public virtual IEnumerator Reload(Player player)
    {
        Debug.Log("Reloading");
        Reloading = true;
        yield return new WaitForSeconds(ReloadSpeed);
        AmmoInv -= MagSize + AmmoCount;
        AmmoCount = MagSize;
        Reloading = false;
        Debug.Log("Finished");
    }
    public void SwapWeapon(Player player) 
    {
        Debug.Log("Swapping");
        Gun temp = player.primary;
        player.primary = player.secondary;
        player.secondary = temp;
    }

    protected int AmmoCount { get; set; }
    protected int AmmoInv { get; set; }
    protected int StartingMags { get; set; }
    protected int MagSize { get; set; }
    protected bool Reloading { get; set; }
    protected float ReloadSpeed { get; set; }
    protected float BloomRange { get; set; }
    protected float BloomScalar { get; set; }
    protected float[] DamageValues { get; set; }
    protected float[] DistanceValues { get; set; }

}
*/