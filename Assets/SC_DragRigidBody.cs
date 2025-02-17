using Unity.VisualScripting;
using UnityEngine;

public class SC_DragRigidbody2D : MonoBehaviour
{
    public float forceAmount = 500f;

    Rigidbody2D selectedRigidbody;
    Camera targetCamera;
    Vector2 originalScreenTargetPosition;
    Vector2 originalRigidbodyPos;
    void Start()
    {
        targetCamera = Camera.main;
    }

    void Update()
    {
        if (!targetCamera)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if(GetRigidbodyFromMouseClick().CompareTag("Player")){
                selectedRigidbody = GetRigidbodyFromMouseClick();
            }
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedRigidbody.velocity = Vector2.zero;
            selectedRigidbody = null;
        }
    }

    void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            Vector2 mousePosition = targetCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePositionOffset = mousePosition - originalScreenTargetPosition;
            Vector2 vel = (originalRigidbodyPos +mousePositionOffset - (Vector2)selectedRigidbody.transform.position) *forceAmount * Time.deltaTime;
            selectedRigidbody.velocity = vel;

        }
    }

    Rigidbody2D GetRigidbodyFromMouseClick()
    {
        Vector2 mousePosition = targetCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitInfo = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.attachedRigidbody != null)
            {
                originalScreenTargetPosition = mousePosition;
                originalRigidbodyPos = hitInfo.collider.transform.position;
                return hitInfo.collider.attachedRigidbody;
            }
        }

        return null;
    }
}
