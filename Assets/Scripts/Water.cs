using UnityEngine;

public class Water : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private ParticleSystem splashParticleSystem = default;
    [Header("Sound")]
    [SerializeField]
    private AudioClip audioWhenArrowSplash = default;
    [SerializeField]
    private GameObject audioPlayerPrefab = default;
    [Range(0.05f, 1f)]
    [SerializeField]
    private float volume = 0.6f;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Arrow")
        {
            // invoke another gameobject to play the sound
            GameObject soundGO = (GameObject)Instantiate(audioPlayerPrefab, other.transform.position, other.transform.rotation, transform);
            AudioPlayer _audioPlayer = soundGO.GetComponent<AudioPlayer>();
            _audioPlayer.Play(audioWhenArrowSplash, volume);
            Destroy(soundGO, 1.5f);

            ParticleSystem splashParticle = (ParticleSystem) Instantiate(splashParticleSystem, other.transform.position, Quaternion.Euler(Vector3.up), transform);
            Destroy(splashParticle, 4f);
        }
    }
}
