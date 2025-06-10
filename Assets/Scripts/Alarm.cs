using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmNoise;
    [SerializeField] private AlarmTriger _alarmTriger;

    private bool _isOn;

    private float _volume = 0;
    private float _maxVolume = 1;
    private float _minVolume = 0;
    private float _volumeStep = 0.1f;

    private Coroutine _volumeCorutine;

    private void OnEnable()
    {
        _alarmTriger.Trigered += ResetVolume;
    }

    private void OnDisable()
    {
        _alarmTriger.Trigered -= ResetVolume;
    }

    private IEnumerator ChangeVolume()
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();
        float targetValume = _isOn ? _maxVolume : _minVolume;

        _isOn = !_isOn;
        _alarmNoise.Play();

        while (Mathf.Approximately(targetValume, _volume) == false)
        {
            _volume = Mathf.MoveTowards(_volume, targetValume, _volumeStep * Time.deltaTime);
            _alarmNoise.volume = _volume;

            if (_volume == 0)
                _alarmNoise.Stop();

            yield return waitForEndOfFrame;
        }

        _volumeCorutine = null;
    }

    private void ResetVolume()
    {
        if (_volumeCorutine != null)
            StopCoroutine(_volumeCorutine);

        _volumeCorutine = StartCoroutine(ChangeVolume());
    }
}