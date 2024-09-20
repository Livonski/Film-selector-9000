using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuOpener menu = FindObjectOfType<MenuOpener>();
            menu.cycleMenu();
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Handle handle = hit.collider.gameObject.GetComponent<Handle>();
                if (handle != null)
                    handle.activate();
            }
        }
    }
}
