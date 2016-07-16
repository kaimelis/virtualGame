using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DevicePlacerScript : MonoBehaviour
{
    public AudioSource placeSound;

    public Text devicesText;
    public GameObject camera;
    public GameObject devicePrefab;
    private GameObject _device;
    //private BoxCollider _deviceCollider;
    private DeviceScript _deviceScript;
    private bool _instantiated;
    private RaycastHit _hitInfo;
    private int ammo = 3;

    // Use this for initialization
    void Start()
    {
        //sharedAIScript = sharedAI.GetComponent<SharedEnemyAI>();
        _instantiated = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ammo > 0)
        {
            PlaceDevice();
        }
        devicesText.text = "" + ammo;
    }
    private void PlaceDevice()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 spawnPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            Quaternion spawnRot = Quaternion.LookRotation(gameObject.transform.right);

            _device = Instantiate(devicePrefab, spawnPos, spawnRot) as GameObject;

            _instantiated = true;
        }
        if (_instantiated && Input.GetKey(KeyCode.E))
        {
            _device.transform.position = (new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z) + (gameObject.transform.forward * 1));
            _device.transform.rotation = Quaternion.LookRotation(-gameObject.transform.right);

        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            _instantiated = false;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out _hitInfo))
            {
                if ((gameObject.transform.position - _hitInfo.point).magnitude < 10 && _hitInfo.collider.gameObject.layer != 4 && _hitInfo.collider.gameObject.tag != "door") // 4 = Ignore raycast
                {
                    placeSound.Play();
                    _device.transform.position = new Vector3(_hitInfo.point.x, _hitInfo.point.y, _hitInfo.point.z);
                    _device.transform.rotation = Quaternion.LookRotation(_hitInfo.normal) * Quaternion.Euler(0, 90, 0);
                    ammo -= 1;

                }
                else
                {
                    Destroy(_device.gameObject);
                }
            }


        }
    }
}
