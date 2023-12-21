using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float autoDestoryTime = 10f;

    void Start()
    {
        StartCoroutine(AutoDestroyRoutine());
    }

    IEnumerator AutoDestroyRoutine()
    {
        yield return new WaitForSeconds(autoDestoryTime);
        Destroy(gameObject);
    }
}
