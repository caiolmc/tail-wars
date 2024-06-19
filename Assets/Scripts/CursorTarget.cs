using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe utilizada para capturar a localização do cursor
public class CursorTarget : MonoBehaviour
{
    private GameObject gamepadCursor;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        gamepadCursor = GameObject.FindWithTag("Cursor");
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(gamepadCursor.GetComponent<GamepadCursor>().cursorTransform.anchoredPosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            transform.position = raycastHit.point;
        }
        //Debug.Log(mainCamera.ScreenToWorldPoint(gamepadCursor.GetComponent<GamepadCursor>().cursorTransform.anchoredPosition));
        //Debug.Log(gamepadCursor.GetComponent<GamepadCursor>().cursorTransform.anchoredPosition);
    }
}