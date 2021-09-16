using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pistol : MonoBehaviour
{
    int StartingMags = 3;
    int MagSize = 12;
    float BloomRange = 2f;
    float BloomScalar = .2f;
    float ReloadSpeed = 2f;
    bool Reloading = false;
    float[] DamageValues = new float[] { 24f, 17f, 12f};
    float[] DistanceValues = new float[] { 15f, 30f };
    int AmmoCount;
    int AmmoInv;
    [SerializeField]
    Text ammoCountUI;
    [SerializeField]
    Player playerInfo;
    [SerializeField]
    bool isEquipped;
    [SerializeField]
    Transform endPoint;
    LineRenderer lr;
    [SerializeField]
    GameObject bulletTrace;

    private void Start()
    {
        if (isEquipped)
            playerInfo.GetComponentInParent<Player>();
        AmmoCount = MagSize;
        AmmoInv = MagSize * StartingMags;
        ammoCountUI.text = AmmoCount.ToString();
        lr = this.gameObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (playerInfo == null)
            return;
        
        if (playerInfo.primary == null)
            playerInfo.primary = this.gameObject;
        else if (playerInfo.secondary == null)
            playerInfo.secondary = this.gameObject;


        if (Input.GetButtonDown("Fire1"))
            StartCoroutine(Shoot(playerInfo));
    }





    public IEnumerator Shoot(Player player)
    {
        Debug.Log("Ammo Count is: " + AmmoCount);
        if (AmmoCount != 0)
        {
            bool hit = Physics.Raycast(player.MainCamera.transform.position, player.MainCamera.transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo);
            --AmmoCount;
            ammoCountUI.text = AmmoCount.ToString();
            //Debug.Log(hitInfo.distance);
            if (hit)
            {
                GameObject bullet = Instantiate(bulletTrace, endPoint);
                StartCoroutine(bullet.GetComponent<BulletTrace>().Bullet(endPoint.position, hitInfo.point));
            }
                if (hit && hitInfo.collider.CompareTag("Player"))
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
    public IEnumerator Reload(Player player)
    {
        Debug.Log("Reloading");
        if (AmmoInv == 0)
            yield return null;
        Reloading = true;
        yield return new WaitForSeconds(ReloadSpeed);
        if (AmmoInv > MagSize)
        {
            AmmoCount = MagSize;
            AmmoInv -= MagSize + AmmoCount;
        }
        else if(AmmoInv > 0)
        {
            AmmoCount = AmmoInv;
            AmmoInv = 0;
        }
        Reloading = false;
        ammoCountUI.text = AmmoCount.ToString();
        Debug.Log("Finished");
    }
    public void SwapWeapon(Player player)
    {
        Debug.Log("Swapping");
        GameObject temp = player.primary;
        player.primary = player.secondary;
        player.secondary = temp;
    }
}
