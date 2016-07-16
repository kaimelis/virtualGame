using UnityEngine;
using System.Collections;

public class lightCreator : MonoBehaviour
{

    public Lightning LightningPrefab;

    IEnumerator Start()
    {
        while (true)
        {
            Instantiate(LightningPrefab, this.transform.position, Quaternion.identity);
            Instantiate(LightningPrefab, this.transform.position, Quaternion.identity);
            Instantiate(LightningPrefab, this.transform.position, Quaternion.identity);
            yield return null;
        }
    }
}
