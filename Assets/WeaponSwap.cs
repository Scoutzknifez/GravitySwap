using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public Animation animations;
    public bool isAnimating;

    public GameObject handJoint;
    public int gunOnStart = 1;
    public GameObject[] guns;

    private void Start()
    {
        Swap(gunOnStart);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Swap(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Swap(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Swap(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Swap(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Swap(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Swap(5);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Swap(-1);
        }

        if (Input.GetMouseButtonDown(1))
        {
            ADS(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            ADS(false);
        }
    }

    void Swap(int slot)
    {
        if (slot >= guns.Length)
        {
            return;
        }

        foreach (Transform child in handJoint.transform)
        {
            Destroy(child.gameObject);
        }

        if (slot != -1)
        {
            Instantiate(guns[slot], handJoint.transform);
        }
    }

    void ADS(bool isAiming)
    {
        isAnimating = true;

        if (isAiming)
        {
            animations.clip = animations.GetClip("ADS");
            animations.Play();
        }
        else
        {
            animations.clip = animations.GetClip("UNADS");
            animations.Play();
        }

        StartCoroutine(animationFinished());
    }

    IEnumerator animationFinished()
    {
        yield return new WaitForSeconds(.5f);

        isAnimating = false;
    }
}