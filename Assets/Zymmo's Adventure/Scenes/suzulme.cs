using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suzulme : MonoBehaviour
{
    public float fallSpeed = 2f; // Düþüþ hýzý

    void Update()
    {
        // Karakteri süzülerek yere düþürmek için yukarýdan aþaðýya doðru bir hareket 
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
