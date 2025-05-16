using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class AudioManager : MonoBehaviour
{
    private AudioSource _bgmSource;

    [SerializeField] private List<AudioClip> _bgmList = new();
    [SerializeField] private SFXController _sfxPrefab;

    private ObjectPool _sfxpool;


    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _bgmSource = GetComponent<AudioSource>();
        _sfxpool = new ObjectPool(transform, _sfxPrefab, 10);
    }

    public void BgmPlay(int index)
    {
        if (0 <= index && index < _bgmList.Count)
        {
            _bgmSource.Stop();
            _bgmSource.clip = _bgmList[index];
            _bgmSource.Play();
        }
    }

    public SFXController GetSFX()
    {
        PooledObject po = _sfxpool.Get();
        return po as SFXController;
    }
}
