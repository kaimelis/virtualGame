using UnityEngine;
using System.Collections;

public class VoiceAnswering : MonoBehaviour
{

    [SerializeField] private AudioClip firstInstruction;
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
        SoundManager.instance.PlaySingle(firstInstruction);
        yield return new WaitForSeconds(1f);
    }
}
