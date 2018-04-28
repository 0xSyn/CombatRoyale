using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
   private GameObject player;
    string playername;
	// Use this for initialization
	void Start () {
		
	}

    public void Initialize(GameObject playr) {
        player = playr;
        playername = player.name;
    }

    // Update is called once per frame
	void Update () {
        if(playername==player.name)
        transform.position=new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);
	}
}
