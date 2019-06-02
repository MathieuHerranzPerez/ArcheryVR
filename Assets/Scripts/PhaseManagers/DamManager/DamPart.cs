using UnityEngine;
using UnityEngine.UI;

public class DamPart : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Text textPower = default;
    [SerializeField]
    private ParticleSystem particleSystemExplosion = default;
    [Header("Sound")]
    [SerializeField]
    private AudioClip audioWhenDestroy = default;
    [SerializeField]
    private GameObject audioPlayerPrefab = default;
    [Range(0.05f, 1f)]
    [SerializeField]
    private float volume = 0.6f;
    
    // ---- INTERN ----
    private Dam dam;
    private int powerToDestroy;

    public void InitWithPowerAndDam(int powerToDestroy, Dam dam)
    {
        this.dam = dam;
        this.powerToDestroy = powerToDestroy;

        GetComponent<MeshCollider>().enabled = true;
        textPower.text = powerToDestroy.ToString();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Arrow")
        {
            Arrow arrow = other.gameObject.GetComponent<Arrow>();
            Debug.Log("Collision with arrow : " + arrow.power + " " + powerToDestroy);
            if (arrow.power == powerToDestroy)
            {
                Destroy();
            }
        }
    }

    private void Destroy()
    {
        dam.NotifyDestruction(this);
        ParticleSystem ps = (ParticleSystem) Instantiate(particleSystemExplosion, transform.position, transform.rotation);
        Destroy(ps, 2f);

        // invoke another gameobject to play the sound
        GameObject soundGO = (GameObject)Instantiate(audioPlayerPrefab, transform.position, transform.rotation);
        AudioPlayer _audioPlayer = soundGO.GetComponent<AudioPlayer>();
        _audioPlayer.Play(audioWhenDestroy, volume);
        Destroy(soundGO, 5f);

        Destroy(gameObject);
    }
}
