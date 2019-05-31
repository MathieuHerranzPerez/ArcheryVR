using System.Collections.Generic;
using UnityEngine;


public class MultiplicationTable : MonoBehaviour
{
    public static MultiplicationTable Instance { get; private set; }

    [Header("2-12")]
    [SerializeField]
    private Multiplication[] listMultiplication = new Multiplication[11];

    void Awake()
    {
        Instance = this;
    }

    public Multiplication GetMultiplication(int num)
    {
        return listMultiplication[num - 2];
    }

    public Multiplication GetRandomMultiplicationForLevel(int level)
    {
        List<Multiplication> resList = new List<Multiplication>();

        foreach(Multiplication m in listMultiplication)
        {
            if (m.level == level)
                resList.Add(m);
        }

        return resList[Random.Range(0, resList.Count)];
    }

    public Multiplication GetRandomMultiplication()
    {
        return listMultiplication[Random.Range(0, listMultiplication.Length)];
    }
}
