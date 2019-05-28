using UnityEngine;
using UnityEngine.UI;

public class DamPart : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Text textPower = default;

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
        // todo gfx
        Destroy(gameObject);
    }
}
