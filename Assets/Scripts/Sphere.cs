using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sphere : MonoBehaviour
{
    [SerializeField]
    private float delta = 10f; // if the spehre is at pos + or - this
    [SerializeField]
    private GameObject deathEffect = default;

    [Header("Setup")]
    [SerializeField]
    private Text textField = default;

    // ---- INTERN ----
    private Wave wave;
    private string data;
    private bool isCorrect;
    private float speed;
    private Transform target;


    private bool hasExplode = false;    // to be sure to not be destroy several times
    
    public bool IsCorrect()
    {
        return isCorrect;
    }

    public void InitAndStart(Wave wave, string data, bool isCorrect, float speed, Transform target)
    {
        this.wave = wave;
        this.data = data;
        textField.text = data;
        this.isCorrect = isCorrect;
        this.speed = speed;
        this.target = target;   // start the move
    }

    void Update()
    {
        if (target)
        {
            Vector3 direction = target.position - transform.position;  // get the direction of the target
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;  // rotate

            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);        // move

            // the shpere has reached the point
            if (Vector3.Distance(transform.position, target.position) <= delta)
            {
                Explode();
            }
        }
    }

    private void ExplodeWithArrow()
    {
        if (!hasExplode)
        {
            hasExplode = true;
            wave.NotifySphereExplodeWithArrow(this);
            if (deathEffect != null)
            {
                GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(effect, 3f);
            }
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        hasExplode = true;
        wave.NotifySphereExplodeEndPath();
        Destroy(gameObject);
    }
}
