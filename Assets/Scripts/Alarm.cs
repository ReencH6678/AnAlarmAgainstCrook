using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmNoise;

    private bool _isOn;

    private float _valume = 0;
    private float _maxValume = 1;
    private float _minValume = 0;
    private float _valumeStep = 0.1f;

    private Coroutine _valumeCorutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Grifter>(out Grifter grifter))
        {
            _alarmNoise.Play();
            _isOn = true;
            RestartValumeCorutine();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Grifter>(out Grifter grifter))
        {
            _isOn = false;
            RestartValumeCorutine();
        }
    }

    private IEnumerator ChangeValume()
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();
        float targetValume = _isOn ? _maxValume : _minValume;

        while (Mathf.Approximately(targetValume, _valume) == false)
        {
            _valume = Mathf.MoveTowards(_valume, targetValume, _valumeStep * Time.deltaTime);
            _alarmNoise.volume = _valume;

            if (_valume == 0)
                _alarmNoise.Stop();

            yield return waitForEndOfFrame;
        }

        _valumeCorutine = null;
    }

    private void RestartValumeCorutine()
    {
        if (_valumeCorutine != null)
            StopCoroutine(_valumeCorutine);

        _valumeCorutine = StartCoroutine(ChangeValume());
    }
}