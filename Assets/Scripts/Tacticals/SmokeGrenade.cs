using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SmokeGrenade : MonoBehaviour
{
    [SerializeField]
    ParticleSystem smokeParticle;
    [SerializeField]
    Image smokeOverlay;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(SmokeScreen());
    }

    IEnumerator SmokeScreen()
    {

        yield return new WaitForSeconds(2f);

    }
}
