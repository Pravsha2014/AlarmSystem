using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarm;

    private readonly float _minAlarmVolume = 0;
    private readonly float _changeAlarmVolumeTime = 0.01f;
    private readonly float _waitingTimeInSeconds = 0.1f;
    private House _house;
    private Coroutine _coroutine;

    private void Start()
    {
        _house = GetComponent<House>();
        _house.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _house.StateChanged -= OnStateChanged;
    }

    private void OnStateChanged(bool isChanged)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        int targetValue = isChanged ? 1 : 0;

        _coroutine = StartCoroutine(ChangeVolume(targetValue));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        if (_alarm.isPlaying == false)
            _alarm.Play();

        var waitForSecond = new WaitForSeconds(_waitingTimeInSeconds);

        while (_alarm.volume != targetVolume)
        {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, targetVolume, _changeAlarmVolumeTime);

            yield return waitForSecond;
        }

        if(_alarm.volume == _minAlarmVolume)
            _alarm.Stop();
    }
}
