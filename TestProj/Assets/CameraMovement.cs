using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    GameObject Player;

	void Start () 
    {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () 
    {
        this.GetComponent<Rigidbody2D>().velocity = (Player.transform.position - transform.position) * 5;
	}
}
