using UnityEngine;
using System.Collections;
using System;

public class PlayerControls : MonoBehaviour {

    float k;

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 100), this.GetComponent<ObjSightRange>().viewAngle.ToString());
    }

	void Update () 
    {
        if (Input.GetKey("w"))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        }
        else if (Input.GetKey("s"))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
        }

        if (Input.GetKey("a"))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        }
        else if (Input.GetKey("d"))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
        }
        if (!Input.anyKey)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x == 0)
        {
            k = Mathf.PI / 2.0f;
        }
        else
        {
            k = (transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y) / (transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        }
        k = Mathf.Atan(k);
        if ((transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x) < 0)
        {
            k += Mathf.PI;
        }
        k += Mathf.PI;
        if(k > 2 * Mathf.PI)
        {
            k -= 2 * Mathf.PI;
        }
        this.GetComponent<ObjSightRange>().viewAngle = (int)(k * 180 / Mathf.PI);
	}
}
