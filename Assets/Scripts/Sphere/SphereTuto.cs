using UnityEngine;

public class SphereTuto : MonoBehaviour
{
    [SerializeField]
    private GameObject deathEffectForGoodAnswerPrefab = default;
    [SerializeField]
    private GameObject deathEffectForWrongAnswerPrefab = default;

    [Header("Setup")]
    [SerializeField]
    private GameObject canvasTextFieldGO = default;
    public string data;
    public bool isCorrect;

    // ---- INTERN ----
    private GameObject player;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            ExplodeWithArrow();
        }
    }

    private void ExplodeWithArrow()
    {
        if (!hasExplode)
        {
            hasExplode = true;

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
