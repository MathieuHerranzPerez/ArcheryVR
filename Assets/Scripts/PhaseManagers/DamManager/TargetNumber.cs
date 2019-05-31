using System.Collections;
using UnityEngine;

public class TargetNumber : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private ParticleSystem effectPrefabForArrow = default;
    [SerializeField]
    private int num = default;

    [Header("Sound")]
    [SerializeField]
    private AudioClip audioWhenFire = default;
    [SerializeField]
    private GameObject audioPlayerPrefab = default;
    [Range(0.05f, 1f)]
    [SerializeField]
    private float volume = 0.6f;

    // ---- INTERN ----
    private float timeBetweenCollide = 1f;
    private bool canCollide = true;
    private bool hasExit = true;

    void OnTriggerEnter(Collider other)
    {
        if(canCollide && hasExit)
        {
            if (other.gameObject.tag == "Arrow")
            {
                Arrow arrow = other.GetComponent<Arrow>();
                arrow.MultiplyPower(num, effectPrefabForArrow);
                hasExit = false;
                canCollide = false;

                // invoke another gameobject to play the sound
                GameObject soundGO = (GameObject)Instantiate(audioPlayerPrefab, transform.position, transform.rotation, transform);
                AudioPlayer _audioPlayer = soundGO.GetComponent<AudioPlayer>();
                _audioPlayer.Play(audioWhenFire, volume);
                Destroy(soundGO, 1.5f);

                StartCoroutine(StartDelay());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        hasExit = true;
    }

    private IEnumerator StartDelay()
    {
        float time = 0f;
        while(time < timeBetweenCollide)
        {
            time += Time.deltaTime;
            yield return null;
        }
        canCollide = true;
    } 
}
