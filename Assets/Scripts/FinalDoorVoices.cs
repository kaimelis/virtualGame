using UnityEngine;
using System.Collections;

public class FinalDoorVoices : MonoBehaviour {


    [SerializeField] private AudioClip finalRoomRadio;
    [SerializeField] private AudioClip blowthisthingup;
    [SerializeField] private float volume = 0.2f;
    private bool played;

    void OnTriggerEnter(Collider collider)
    {
        if (!played)
        {
            played = true;
            if (collider.tag == "Player")
                StartCoroutine(Sound());
        }
    }

    IEnumerator Sound()
    {
        SoundManager.instance.Volume(volume);
        SoundManager.instance.PlaySingle(finalRoomRadio);
        yield return new WaitForSeconds(1.8f);
        SoundManager.instance.PlaySingle(blowthisthingup);
    }
}
