using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField] float secondsToDestroy;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
    }
}
