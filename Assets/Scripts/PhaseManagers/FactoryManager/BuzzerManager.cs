using UnityEngine;
using UnityEngine.UI;

public class BuzzerManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Factory factory = default;
    [SerializeField]
    private Text textCurrentNumber = default;

    [Header("Reset")]
    [SerializeField]
    private ParticleSystem resetParticleSystem = default;
    [SerializeField]
    private Transform particleSystemTransform = default;
    [Header("Sound")]
    [SerializeField]
    private AudioClip audioWhenReset = default;
    [SerializeField]
    private GameObject audioPlayerPrefab = default;
    [Range(0.05f, 1f)]
    [SerializeField]
    private float volume = 0.6f;

    // ---- INTERN ----
    private int currentNumber = 0;
        
    public void NotifyBuzzerHit(int num)
    {
        currentNumber += num;
        textCurrentNumber.text = currentNumber.ToString();
        factory.DropItem(num);
    }

    public void NotifyBuzzerReset()
    {
        currentNumber = 0;
        textCurrentNumber.text = currentNumber.ToString();

        // invoke another gameobject to play the sound
        GameObject soundGO = (GameObject)Instantiate(audioPlayerPrefab, textCurrentNumber.transform.position, textCurrentNumber.transform.rotation, transform);
        AudioPlayer _audioPlayer = soundGO.GetComponent<AudioPlayer>();
        _audioPlayer.Play(audioWhenReset, volume);
        Destroy(soundGO, 1.5f);

        ParticleSystem particleSystem = (ParticleSystem)Instantiate(resetParticleSystem, particleSystemTransform.position, particleSystemTransform.rotation, transform);
        Destroy(particleSystem.gameObject, 1.5f);
    }

    public void NotifyBuzzerTry()
    {
        factory.CheckResult(currentNumber);
        currentNumber = 0;
        textCurrentNumber.text = currentNumber.ToString();
    }
}
