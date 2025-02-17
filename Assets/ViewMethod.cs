using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMethod : MonoBehaviour
{
    public PolygonCollider2D body;

    public bool goUp = true;
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (goUp)
        {
            transform.Rotate(new Vector3(0, 0, 15) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -15) * Time.deltaTime);
        }
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision");
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Aquí");
            goUp = !goUp;
        }
    }

}
