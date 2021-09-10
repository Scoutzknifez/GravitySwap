using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroutTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(ClickToShoot());
    }



    IEnumerator ClickToShoot()
    {
        Debug.Log("Hello World");
        yield return new WaitForSeconds(2f);
    }
}
