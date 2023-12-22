using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class LevelEventChannel : ScriptableObject
{
    public UnityAction OnKeyPickedUp;

    public void RaiseOnKeyPickedUp()
    {
        OnKeyPickedUp?.Invoke();
    }
}
