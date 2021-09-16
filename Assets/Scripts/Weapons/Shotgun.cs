using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shotgun : MonoBehaviour
{
    int pelletsPerShot = 15;
    int StartingMags = 3;
    int MagSize = 7;
    int AmmoCount;
    int AmmoInv;
    float BloomRange = 5f; //Do we need to pass values?
    float BloomScalar = 0f;
    float ReloadSpeed = 4f;
    bool Reloading = false;
    float[] DamageValues = new float[] { 30f };
    float[] DistanceValues = new float[] { 40f };
    [SerializeField]
    Text ammoCountUI;
    [SerializeField]
    Player playerInfo;
    [SerializeField]
    bool isEquipped;
    [SerializeField]
    Transform endPoint;
    LineRenderer lr;

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


    //add bloom per pellet
    public IEnumerator Shoot(Player player)
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
                    if(player.GetComponent<LineRenderer>() == null)
                        player.gameObject.AddComponent<LineRenderer>();
                    LineRenderer line = player.gameObject.GetComponent<LineRenderer>();
                    line.SetPositions(new Vector3[] { player.MainCamera.transform.position, hitInfo.point });
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

    public IEnumerator Reload(Player player)
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
        GameObject temp = player.primary;
        player.primary = player.secondary;
        player.secondary = temp;
    }

    public IEnumerator BulletTrace(Vector3 startPoint, Vector3 endPoint)
    {
        Debug.Log("Bullet Trace");
        while(Vector3.Distance(startPoint, endPoint) > 3)
        {
            lr.SetPosition(0, startPoint);
            lr.SetPosition(1, endPoint);
            yield return new WaitForSeconds(.4f);
            startPoint = Vector3.Lerp(startPoint, endPoint, .25f);
        }
    }
    //need to override reload
}
