using UnityEngine;
using System.Collections;

public class KeyCardPickUps : MonoBehaviour
{
    //public AudioClip PickUpSound;
    public bool doorKey;
    public AudioClip Audioclip;


    void OnTriggerEnter(Collider theCollider)
    {
        if (theCollider.tag == "Player")
        {
            StartCoroutine(PickedUp());
            StartCoroutine(Sounds());
            // AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
            doorKey = true;
        }  
    }

    void OnTriggerExit(Collider theCollider)
    {
        if (theCollider.tag == "Player")
        {
            StartCoroutine(PickedUp());
            // AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
            doorKey = false;
        }
    }

    IEnumerator Sounds()
    {
        yield return new WaitForSeconds(0.28f);
        SoundManager.instance.PlaySingle(Audioclip);
    }

    IEnumerator PickedUp()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
