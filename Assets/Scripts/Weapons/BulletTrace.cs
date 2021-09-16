using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrace : MonoBehaviour
{
    LineRenderer lr;
    // Start is called before the first frame update
    public IEnumerator Bullet(Vector3 startPoint, Vector3 endPoint)
    {
        lr = this.gameObject.GetComponent<LineRenderer>();
        Vector3 temp = startPoint;
        Debug.Log("Bullet Trace");
        while (Vector3.Distance(startPoint, endPoint) > 3)
        {
            lr.SetPosition(0, startPoint);
            lr.SetPosition(1, endPoint);
            yield return null;
            startPoint = Vector3.Lerp(startPoint, endPoint, .25f);
        }
        Destroy(gameObject);
    }
}
