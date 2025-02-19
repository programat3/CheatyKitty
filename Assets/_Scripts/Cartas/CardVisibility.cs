using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardVisibility : MonoBehaviour
{
    public GameObject Card;
    public Transform dogView1;
    public Transform dogView2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Card.SetActive(dogView1.localScale.y < 1 && dogView2.localScale.y < 1);
    }
}
