using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ViewMethod : MonoBehaviour
{
    public float speed = 5f;
    private bool goUp = true;

    void Update()
    {
        // Rotar en la direcci�n correcta
        float rotationSpeed = 15f * Time.deltaTime * speed;
        transform.Rotate(0, 0, goUp ? rotationSpeed : -rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Muro")) // Verificar si choca con un muro
        {
            goUp = !goUp; // Invertir la direcci�n
            Debug.Log("Colisi�n con: " + collision.gameObject.name);
        }
    }

}