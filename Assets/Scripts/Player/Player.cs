using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    int playerID;
    enum Teams { Red, Blue };
    enum Guns {Primary, Secondary};
    int team;
    int kills;
    Camera mainCamera;


    public Gun primary;
    public Gun secondary;
    List<GameObject> playersInGame;
    public Camera MainCamera => mainCamera;

    //Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponentInChildren<Camera>();
        primary = new Pistol();
        secondary = new Rifle();
        StartCoroutine(primary.Shoot(this));
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            primary.SwapWeapon(this);
            StopAllCoroutines();
            StartCoroutine(primary.Shoot(this));
        }
        if (Input.GetButtonDown("Fire1"))
            StartCoroutine(primary.Shoot(this));

    }
}
