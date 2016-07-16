using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControls2 : MonoBehaviour
{
    //      GUN variables
    [Header("Gun information")]
    public GameObject HandModel;
    public GameObject GunModel;
    public Transform GunTipObject;
    public float Range = 50;

    private GameObject _bullet;
    private Vector3 _prevRayCastPoint;
    private LineRenderer _lineRenderer;
    private readonly WaitForSeconds _shotLength = new WaitForSeconds(.07f);
    public Light AmmoLight;

    private int ammoMax = 6;
    private float _deltaTime;
    private bool _slowMotion;
    private float _fireDelta;
    private bool _countFireDelta;

    // Variables used for raycasting and shooting
    private bool blowback;
    private bool backwards;
    private int blowbackLimit;
    private int blowbackCounter;

    // Variables used for Line of Aim Handling
    private RaycastHit _hitInfo;
    private Ray _ray;

    [Header("Particle Effects")]
    public ParticleSystem SmokeParticleSystem;

    //private WaitForSeconds shotLength = new WaitForSeconds(.07f);
    private AudioSource source;

    // Variables used for player movement 
    [Header("Player movements")]
    private Rigidbody rb;
    public int MaxMagnitude = 3;
    public int accelerationValue;
    public int MovementSpeed = 1;
    public float JumpSpeed = 500;
    public float SlowDownFactor = 0.945f;
    public int MaxMovementSpeed = 3;

    private Rigidbody _rigidBody;
    private float _maxMagnitude;
    private float _forwardSpeed = 0f;
    private float _sidewaysSpeed = 0f;
    private bool _grounded = true;

    [Header("Sounds")]
    public AudioSource jumpSound;
    public AudioSource landSound;
    public AudioSource walk1;
    public AudioSource walk2;
    public AudioSource run1;
    public AudioSource run2;
    private bool playedStep1 = false;
    private bool playedStep2 = false;
    // Variables used for player movement

    public enum RotationAxes
    {
        MouseXAndY = 0, MouseX = 1, MouseY = 2
    }

    // Variables used for player and camera rotation
    [Header("Camera Sensitivity")]
    public Transform Camera;
    public RotationAxes Axes = RotationAxes.MouseXAndY;
    private float SensitivityX = StaticVariables.sensitivityY;
    private float SensitivityY = StaticVariables.sensitivityX;
    public float MinimumX = -360F;
    public float MaximumX = 360F;
    public float MinimumY = -90F;
    public float MaximumY = 90F;
    private float _rotationY = 0f;
    private CapsuleCollider _collider;
    private float ground;

    void Start()
    {
        blowback = false;
        backwards = true;
        blowbackCounter = 0;
        blowbackLimit = 5;
        _collider = GetComponent<CapsuleCollider>();
        ground = _collider.bounds.extents.y;
        //Getting Components
        _rigidBody = GetComponent<Rigidbody>();
        // AudioGunShoot = GetComponent<AudioSource>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        PlayerMovement();
        PlayStepsOnMovement();
        PlayerAndCameraRotation();
    }

    void PlayMuzzleFlash()
    {
        if (SmokeParticleSystem != null) SmokeParticleSystem.Play();
    }

    private IEnumerator ShotEffect()
    {
        _lineRenderer.enabled = true; // for line to show up
        //AudioGunShoot.Play();
        yield return _shotLength;
        _lineRenderer.enabled = false; // for line to disapear
    }

    //use it for grenade amount
    /*public void UpdateGunGlow()
    {
        Debug.Log("Updating color of gun");
        var gunMat = GunModel.GetComponent<Renderer>().materials;
        Color newEmission = Color.white * ((float)_ammo / (float)ammoMax);//multiply by CurrentAmmo / MaxAmmo
        gunMat[0].SetColor("_EmissionColor", newEmission);

        AmmoLight.GetComponent<Light>().intensity = 1f * ((float)_ammo / (float)ammoMax);
    }*/
    private void PlayStepsOnMovement()
    {
        if (Grounded() && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                if (!walk1.isPlaying && !walk2.isPlaying)
                {
                    walk1.Play();
                }
                else if (!walk1.isPlaying)
                {
                    walk2.Play();
                }
            }
            else
            {
                if (!run1.isPlaying && !run2.isPlaying)
                {
                    run1.Play();
                }
                else if (!run1.isPlaying)
                {
                    run2.Play();
                }
            }
        }
    }
    private void PlayerMovement()
    {
        Vector3 velocity = _rigidBody.velocity;
        if (Grounded() && Input.GetKeyDown(KeyCode.Space))
        {
            jumpSound.Play();
            _rigidBody.AddRelativeForce(new Vector3(0, JumpSpeed, 0));
        }

        if (Input.GetKey(KeyCode.W))
        {
            _forwardSpeed = MovementSpeed;
            if ( !Input.GetKey(KeyCode.LeftShift))  
            {
                _forwardSpeed = 1;
            }   
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _forwardSpeed *= 2;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _forwardSpeed = -MovementSpeed;

        }
        else _forwardSpeed = 0;

        if (Input.GetKey(KeyCode.A))
        {
            _sidewaysSpeed = -MovementSpeed;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            _sidewaysSpeed = MovementSpeed;
        }
        else _sidewaysSpeed = 0;

        Vector3 direction = new Vector3(_sidewaysSpeed, 0, _forwardSpeed);

        if (direction == Vector3.zero)
            _rigidBody.velocity = _rigidBody.velocity * SlowDownFactor;

        else
        {
            if (velocity.magnitude <= MaxMovementSpeed)
            {
                _rigidBody.AddRelativeForce(direction * accelerationValue);
            }
            if (velocity.magnitude > MaxMovementSpeed)
            {
                _rigidBody.velocity = _rigidBody.velocity * SlowDownFactor;
            }
        }
    }

    void PlayerAndCameraRotation()
    {
        if (Axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * SensitivityX;

            _rotationY += Input.GetAxis("Mouse Y") * SensitivityY;
            _rotationY = Mathf.Clamp(_rotationY, MinimumY, MaximumY);

            transform.localEulerAngles = new Vector3(0, rotationX, 0);
            Camera.localEulerAngles = new Vector3(-_rotationY, 0, 0);
        }
        else if (Axes == RotationAxes.MouseX) transform.Rotate(0, Input.GetAxis("Mouse X") * SensitivityX, 0);
    }

    public bool Grounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, ground + 0.1f);
    }
}
