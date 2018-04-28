using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletMovement : MonoBehaviour {
    public int speed=2;
    private float lifetime;

    private void OnTriggerEnter(Collider player) {
        if (player.CompareTag("Player")) {
            Debug.Log("hit player");
            player.GetComponent<PlayerController>().TakeDamage();
            Destroy(gameObject);
        }
		if (player.CompareTag("wall")) {
            Debug.Log("bullet hit wall");
            Destroy(gameObject);
        }
    }
	
    void onCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("wall"))
        {
            print("hit wall");
            Destroy(gameObject);
        }
    }


	void Update () {
	    transform.position += transform.right * Time.deltaTime*speed;
	    lifetime += .1f;
	    if (lifetime > 20) {
	        Destroy(gameObject);
        }
	}
}
