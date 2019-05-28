using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereMovement))]
public class Sphere : MonoBehaviour
{
    [SerializeField]
    private GameObject deathEffectForGoodAnswerPrefab = default;
    [SerializeField]
    private GameObject deathEffectForWrongAnswerPrefab = default;

    [Header("Setup")]
    [SerializeField]
    private Text textField = default;
    [SerializeField]
    private GameObject canvasTextFieldGO = default;

    // ---- INTERN ----
    private GameObject player;
    private SphereMovement sphereMovement;

    private Wave wave;
    private string data;
    private bool isCorrect;
   
    private bool hasExplode = false;    // to be sure to not be destroy several times

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector3 direction = player.transform.position - canvasTextFieldGO.transform.position;   // direction to the target
        // healthbar follows the player
        Quaternion lookRatation = Quaternion.LookRotation(-direction);
        Vector3 rotation = Quaternion.Lerp(canvasTextFieldGO.transform.rotation, lookRatation, Time.deltaTime * 10).eulerAngles;
        canvasTextFieldGO.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public bool IsCorrect()
    {
        return isCorrect;
    }

    public void InitAndStart(Wave wave, string data, bool isCorrect, float speed, PathSphere path)
    {
        sphereMovement = GetComponent<SphereMovement>();
        sphereMovement.Init(path, speed);

        this.wave = wave;
        this.data = data;
        textField.text = data;
        this.isCorrect = isCorrect;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            ExplodeWithArrow();
        }
    }

    public void Explode()
    {
        hasExplode = true;
        wave.NotifySphereExplodeEndPath(this);
        Destroy(gameObject);
    }

    private void ExplodeWithArrow()
    {
        if (!hasExplode)
        {
            hasExplode = true;
            wave.NotifySphereExplodeWithArrow(this);

            if (isCorrect)
            {
                GameObject effect = Instantiate(deathEffectForGoodAnswerPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 3f);
            }
            else
            {
                GameObject effect = Instantiate(deathEffectForWrongAnswerPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 3f);
            }

            Destroy(gameObject);
        }
    }
}
