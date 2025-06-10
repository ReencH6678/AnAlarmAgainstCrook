using System;
using UnityEngine;

public class AlarmTriger : MonoBehaviour
{
    public event Action Trigered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Grifter>(out Grifter grifter))
        {
            Trigered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Grifter>(out Grifter grifter))
        {
            Trigered?.Invoke();
        }
    }
}
