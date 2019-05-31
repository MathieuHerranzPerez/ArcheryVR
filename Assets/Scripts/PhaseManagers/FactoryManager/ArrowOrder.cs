using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowOrder : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Text textOrder = default;

    public void SetOrder(int nbArrowOrdered)
    {
        textOrder.text = nbArrowOrdered.ToString();
    }
}
