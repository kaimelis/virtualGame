using UnityEngine;

namespace PickUps
{
    public class EndDoorTrigger : MonoBehaviour
    {
        public AudioSource openSound;
        public bool YellowButton;
        public bool PinkButton;
        public bool BlueButton;
        public bool OrangeButton;
        [SerializeField] private Animator _leftDoor;
        [SerializeField] private Animator _rightDoor;

        void Start()
        {
            YellowButton = false;
            PinkButton = false;
            BlueButton = false;
            OrangeButton = false;

        }

        void OnTriggerEnter(Collider other)
        {
            if (YellowButton && PinkButton && BlueButton && OrangeButton)
            {
                openSound.Play();
                _leftDoor.enabled = true;
                _rightDoor.enabled = true;
            }
        }

    }
}
