using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun
{
    // Start is called before the first frame update
    public abstract IEnumerator Shoot(Player player);
    public abstract void Reload(Player player);
    public void SwapWeapon(Player player) 
    {
        Debug.Log("Swapping");
        Gun temp = player.primary;
        player.primary = player.secondary;
        player.secondary = temp;
    }
   
}
