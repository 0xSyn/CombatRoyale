using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
   public GameObject bullet;
    //public Camera cam;
	public AudioClip shoot;
	public AudioClip enter;
	public AudioClip destroy;
	private AudioSource source;
    public float speed;
    public GameObject spawnPoints;
    private bool _state_dead=false;
    float respawnTimer=20;
    private ParticleSystem ps_fire;
    private bool canFire = true;
    float fireTimer=1f;
    private CharacterController controller;

    // Use this for initialization
    void Start () {
        //name="player"+Random.Range(0, 999999);
        //var camera=Instantiate(cam, new Vector3(transform.position.x, transform.position.y, transform.position.z-20), transform.rotation);
	    //camera.GetComponent<CameraController>().Initialize(gameObject);
		source.PlayOneShot(enter, 1.0f);
	    ps_fire = GetComponentInChildren<ParticleSystem>();
        controller = GetComponent<CharacterController>();
    }
	
	void Awake () {
    
        source = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
        respawn();
	    if (!isLocalPlayer)
	        return;
        if (!_state_dead) {
            var angles = transform.rotation.eulerAngles;
            fireRecharge();
            Vector3 move=Vector3.zero;
            if (Input.GetKey(KeyCode.A)) {
                angles.z += Time.deltaTime * (speed * 10);
                transform.rotation = Quaternion.Euler(angles);
            }
            else if (Input.GetKey(KeyCode.D)) {
                angles.z += -Time.deltaTime * (speed * 10);
                transform.rotation = Quaternion.Euler(angles);
            }
            if (Input.GetKey(KeyCode.W)) {
                move = transform.right * speed;
            }
            else if (Input.GetKey(KeyCode.S)) {
                move = -transform.right * speed;
            }
            if (Input.GetKey(KeyCode.K)) {
                TakeDamage();
            }

            controller.Move(move*Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Space)&&canFire) {
                ps_fire.Play();
                CmdFire();
                canFire = false;
            }
        }
	    
	}
    void onCollisionEnter(Collision collider)
    {
        if (gameObject.CompareTag("wall"))
        {
            print("hit wall");
            transform.position = Vector3.zero;
        }
    }
    [Command]
    void CmdFire() {
        var b = Instantiate(bullet, transform.position + transform.right*2, transform.rotation);
		NetworkServer.Spawn(b);
		source.PlayOneShot(shoot, 1.0f);
        ///NetworkServer.
        
    }

    void fireRecharge() {
        if (!canFire) {
            fireTimer -= .01f;
            if (fireTimer <= 0) {
                fireTimer = 1f;
                canFire = true;
            }

        }
    }


    public void TakeDamage() {
        _state_dead = true;
        Renderer[] rend = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rend) {
            r.enabled = false;
        }
        source.PlayOneShot(destroy, 1.0f);
    }


    private void respawn() {
        if (_state_dead) {
            respawnTimer -= .1f;
            if (respawnTimer < 0) {
                _state_dead = false;
                respawnTimer = 20;
                Renderer[] rend = GetComponentsInChildren<Renderer>();
                foreach (Renderer r in rend) {
                    r.enabled = true;
                 }
                Vector3 sp= spawnPoints.transform.GetChild(Mathf.FloorToInt(Random.Range(0, 4))).transform.position;
                transform.position=new Vector3(sp.x,sp.y,transform.position.z);
               
            }
        }
    }
}
