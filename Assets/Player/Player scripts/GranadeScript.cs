using UnityEngine;
using System.Collections.Generic;

public class GranadeScript : MonoBehaviour
{
    [Range(1, 10)]
    public float explosionRadius = 5;
    public float explosionTime;
    public GameObject particlesPrefab;
    private GameObject particleObject;
    private ParticleSystem particleScript;
    private MeshRenderer meshRenderer;
    private float explosionTimer;
    private bool _exploded;
    private bool scaleUp = false;
    public AudioSource grenadeExplodeSound;
    private Collider coll;
	private void Start () {
        explosionTimer = 0;
        _exploded = false;
        coll = GetComponent<Collider>();
        particleScript = particlesPrefab.GetComponent<ParticleSystem>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
	

	private void FixedUpdate () {
        ExplosionCountDown();
        AfterExplosionCountDownUntilDeletion();
	}
    private void AfterExplosionCountDownUntilDeletion()
    {
        if (_exploded)
        {
            explosionTimer += Time.deltaTime;   
          //  if(scaleUp && explosionTimer < particleScript.startLifetime/2)
       //     {
        //        gameObject.transform.localScale *= 50;
       //     }
       //     else if (scaleUp && explosionTimer > particleScript.startLifetime/2)
         //   {
       //         gameObject.transform.localScale /= 50;
       //     }

            if (explosionTimer >= particleScript.startLifetime)
            {
                Destroy(particleObject);
                Destroy(gameObject);
            }
        }
    }
    private void ExplosionCountDown()
    {
        if (!_exploded)
        {
            explosionTimer += Time.deltaTime;
            if (explosionTimer >= explosionTime)
            {
                explosionTimer = 0;
                Explode();
            }
        }
    }
    private void Explode()
    {
        meshRenderer.enabled = false;
        particleObject = Instantiate(particlesPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
        _exploded = true;
        grenadeExplodeSound.Play();
        ParalyzeDrone();
        scaleUp = true;
    }


    private void ParalyzeDrone()
    {
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");
        for (int i = 0; i < drones.Length; i++)
        {
            if ((drones[i].transform.position - gameObject.transform.position).magnitude < explosionRadius)
            {
                EnemyAgent agentScript = drones[i].GetComponent<EnemyAgent>();
                agentScript.Paralyze();
            }
        }
    }
    public bool Exploded
    {
        get { return _exploded; }
    }
}
