using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    int playerID;
    int team;
    int kills;
    int assists;
    int deaths;

    [SerializeField]
    Camera mainCamera;
    enum Teams { Red, Blue };
    enum Guns {Primary, Secondary};
    public int PlayerID => playerID;
    public int Kills => kills;
    public int Assists => assists;
    public int Deaths => deaths;
    public Camera MainCamera => mainCamera;

    public float health;
    public Gun primary;
    public Gun secondary;
    List<GameObject> playersInGame;
    public int Team => team;

    //Start is called before the first frame update
    void Start()
    {
        primary = new Pistol();
        secondary = new Rifle();
        playerID = gameObject.GetInstanceID();
        Debug.Log("playerId is:" + playerID);
        health = 100f;
    }


    public void Damage(Player damagingPlayer, float damageValue)
    {
        this.health -= damageValue;
        Debug.Log("Enemy health is: " + this.health);
        if(health <= 0f)
        {
            ++damagingPlayer.kills;
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
