using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMethod : MonoBehaviour
{
    public PolygonCollider2D body;
    public GameObject fondoObj;
    public GameObject topObj;
    public GameObject bottomObj;
    BoxCollider2D fondo;
    BoxCollider2D top;
    BoxCollider2D bottom;
    public bool goUp = true;
    // Start is called before the first frame update
    void Start()
    {
        fondo = fondoObj.GetComponent<BoxCollider2D>();
        top = topObj.GetComponent<BoxCollider2D>();
        bottom = bottomObj.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (body.IsTouching(top))
        {
            goUp = true;
        }
        else if (body.IsTouching(bottom))
        {
            goUp = false;
        }
        if (goUp)
        {
            transform.Rotate(new Vector3(0, 0, 15) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -15) * Time.deltaTime);
        }
       
    }

}
