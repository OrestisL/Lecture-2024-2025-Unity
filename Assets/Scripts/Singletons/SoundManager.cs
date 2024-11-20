using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : GenericSingleton<SoundManager>
{
    [SerializeField]
    private AudioClip[] SFX;

    private AudioSource _bgmSource;

    public override void Awake()
    {
        base.Awake();

        _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmSource.loop = true;
        _bgmSource.playOnAwake = true;
        _bgmSource.volume = 0.65f;
        _bgmSource.spatialBlend = 0f;


        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeMusic(scene, 1.0f));
    }

    private IEnumerator FadeMusic(Scene scene, float duration)
    {
        float startDuration = duration;
        float targetVolume = _bgmSource.volume;

        while (duration >= 0)
        {
            duration -= Time.deltaTime;
            _bgmSource.volume -= Time.deltaTime;
            yield return null;
        }
        AudioClip bgm = Resources.Load(Path.Combine("BGM", scene.name), typeof(AudioClip)) as AudioClip;

        _bgmSource.clip = bgm;
        _bgmSource.Play();

        while (duration <= startDuration)
        {
            duration += Time.deltaTime;
            _bgmSource.volume += Time.deltaTime * targetVolume;
            yield return null;
        }
    }

    public AudioClip GetSFX(string name)
    {
        return SFX.Where(x => x.name == name).FirstOrDefault();       
    }

    public AudioClip GetSFX(int index)
    {
        return SFX[index];
    }
}
