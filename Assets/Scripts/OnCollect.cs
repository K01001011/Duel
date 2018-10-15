using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.collider.GetComponent<Player>();
            player.RepairPlayer(10);
            Master.DestroyPortion(this);
        }
    }
}
