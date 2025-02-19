using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D transparentCursor;

    public Vector2 hotspot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(normalCursor, hotspot, CursorMode.ForceSoftware);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Al hacer clic
        {
            Cursor.SetCursor(transparentCursor, hotspot, CursorMode.ForceSoftware);
        }
        else if (Input.GetMouseButtonUp(0)) // Al soltar el clic
        {
            Cursor.SetCursor(normalCursor, hotspot, CursorMode.ForceSoftware);
        }
    }
}