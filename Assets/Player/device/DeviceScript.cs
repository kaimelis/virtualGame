using UnityEngine;
using System.Collections.Generic;

public class DeviceScript : MonoBehaviour
{
    public AudioSource activateSound;
    public AudioSource noiseSound;

    [Range(1, 10)]
    public float distractionRange = 5;
    public float lifeTime = 10;
    private bool _active;
    private bool _expired;
    private List<GameObject> drones;
    private float lifeTimer;
    
    private void Start()
    {
        drones = new List<GameObject>();
        _active = false;
        _expired = false;
        lifeTimer = 0;
    }
    private void FixedUpdate()
    {
        Work();
    }
    private void Work()
    {
        Activate();
        if (_active)
        {
            DistractDrones();
            LifeTimeCountDown();
        }
    }
    private void Activate()
    {
        if (!_active && !_expired && Input.GetKeyDown(KeyCode.R))
        {
            activateSound.Play();
            Debug.Log("Device Active");
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Drone");

            for (int i = 0; i < temp.Length; i++)
            {
                if ((temp[i].transform.position - gameObject.transform.position).magnitude < distractionRange)
                {
                    drones.Add(temp[i]);
                }
            }
            _active = true;
            // Beep beep sound shall start playing

        }
    }
    private void LifeTimeCountDown()
    {
        new WaitForSeconds(1f);
        noiseSound.Play();
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            noiseSound.Stop();  
            for (int i = 0; i < drones.Count; i++)
            {
                EnemyAgent agentScript = drones[i].GetComponent<EnemyAgent>();
                agentScript.UnDistract();
                 //TODO: replace with unDistract or something
            }
            _active = false;
            _expired = true;
            lifeTimer = 0;
            Debug.Log("Device Inactive");
            Destroy(gameObject);    
        }
    }
    private void DistractDrones()
    {
        if (!_expired)
        {
            for (int i = 0; i < drones.Count; i++)
            {
                  if ((drones[i].transform.position - gameObject.transform.position).magnitude < distractionRange)
                  {
                    EnemyAgent agentScript = drones[i].GetComponent<EnemyAgent>();
                    if (!agentScript.Distracted)
                    {
                        agentScript.Distract(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));
                    }
                }
            }
        }
    }
}