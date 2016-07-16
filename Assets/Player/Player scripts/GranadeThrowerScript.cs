using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GranadeThrowerScript : MonoBehaviour {
    [Range(500, 1000)]
    public int ForwardForce = 1000;
    [Range(500, 1000)]
    public int UpwardForce = 500;
    public AudioSource gunSound;
    public Text granadesText;
    public GameObject player;
    public GameObject prefab;
    private GameObject granade;
    private bool spawn;

    public int ammo = 3;
    public float firedelay = 1;
    private bool countdelay = false;
    private bool readyToShoot = true;
    private float delayCounter = 0;


	private void Start () {
        delayCounter = 0;
        spawn = false;
	}
	
	private void FixedUpdate () {

        if (ammo > 0)
        {
            SpawnGranade();
        }
        granadesText.text = "" + ammo;
        CountDelay();
	}
    private void CountDelay()
    {
        if (countdelay)
        {
            delayCounter += Time.deltaTime;
            if (delayCounter >= firedelay)
            {
                readyToShoot = true;
                delayCounter = 0;
                countdelay = false;
            }
        }
    }
    private void SpawnGranade()
    {
        Debug.DrawLine(SpawnPosition(), SpawnPosition() + player.transform.forward, Color.yellow);
        if (readyToShoot && Input.GetMouseButtonDown(0))
        {
            readyToShoot = false;
            countdelay = true;
            spawn = true;
        }
        if (spawn && Input.GetMouseButtonUp(0))
        {
            spawn = false;
            

            granade = Instantiate(prefab, SpawnPosition(), player.transform.rotation) as GameObject;
            Rigidbody granadeRigidBody = granade.GetComponent<Rigidbody>();
            // Ignore Collisions between player and the granade
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), granade.GetComponent<Collider>());

            granadeRigidBody.mass = 2.5f;
            // Apply force to the granade
            granadeRigidBody.AddForce(player.transform.forward* ForwardForce + player.transform.up* UpwardForce);

            ammo -= 1;
            gunSound.Play();
        }
    }
    private Vector3 SpawnPosition()
    {
        return (player.transform.position + player.transform.forward*0.025f);
    }
}
