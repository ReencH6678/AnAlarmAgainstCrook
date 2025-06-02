using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmNoise;

    private bool _isOn;

    private float _valume = 0;
    private float _maxValume = 1;
    private float _minValume = 0;
    private float _valumeStep = 0.1f;

    private void Update()
    {
        float targetValume = _isOn ? _maxValume : _minValume;

        _valume = Mathf.MoveTowards(_valume, targetValume, _valumeStep * Time.deltaTime);
        _alarmNoise.volume = _valume;

        if(_valume == 0)
            _alarmNoise.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        _alarmNoise.Play();
        _isOn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isOn = false;
    }
}