using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
public class Sniper : MonoBehaviour
{ 
    int StartingMags = 5;
    int MagSize = 4;
    int AmmoCount;
    int AmmoInv;
    float BloomRange = 5f; //Do we need to pass values?
    float BloomScalar = 0f;
    float ReloadSpeed = 4f;
    bool Reloading = false;
    float[] DamageValues = new float[] { 150f, 70f };
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


    public IEnumerator Shoot(Player player)
    {
        Debug.Log("Ammo Count is: " + AmmoCount);
        if (AmmoCount != 0)
        {
            bool hit = Physics.Raycast(player.MainCamera.transform.position, player.MainCamera.transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo);
            --AmmoCount;
            if (hit)
                StartCoroutine(BulletTrace(endPoint.position, hitInfo.point));
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
        Vector3 temp = startPoint;
        Debug.Log("Bullet Trace");
        while (Vector3.Distance(startPoint, endPoint) > 3)
        {
            lr.SetPosition(0, startPoint);
            lr.SetPosition(1, endPoint);
            yield return null;
            startPoint = Vector3.Lerp(startPoint, endPoint, .25f);
        }
        lr.SetPositions(new Vector3[] { temp, temp });
    }
}
