using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suzulme : MonoBehaviour
{
    public float fallSpeed = 2f; // D���� h�z�

    void Update()
    {
        // Karakteri s�z�lerek yere d���rmek i�in yukar�dan a�a��ya do�ru bir hareket 
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
