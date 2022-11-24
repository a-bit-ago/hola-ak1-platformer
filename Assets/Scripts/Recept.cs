using System;
using UnityEngine;

public class Recept : MonoBehaviour
{
    // Socker kom ihåg att alltid ta rätt mängd
    public int socker = 11;
    public float kako = 11.2f;
    
    public string text = "true";
    public bool isTrue = true;
    
    private void Start()
    {
        Debug.Log("Start");
    }

    private void Awake()
    {
        Debug.Log("Awake");
    }

    private void Update()
    {
        Blanda();
    }

    public void Blanda()
    {
        int sockerOchKako = 1;
        int x = 0;

        for (int i = 0; i < sockerOchKako; i++)
        {
            Debug.Log("Update for-loop " + sockerOchKako);
            sockerOchKako = i;
        }

        while (x < sockerOchKako)
        {
            Debug.Log("Update while-loop " + sockerOchKako);
        }
    }

    private void Blanda2()
    {
        Debug.Log("Gott");
    }

    public float SummeraSmeten(float sum)
    {
        return socker + kako + sum;
    }
}

