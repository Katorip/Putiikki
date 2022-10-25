using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    public static event Action CloseAllPanels = delegate { };

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit)
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                clickable?.Click();
            }
        }
    }

    public static void CallCloseAllPanels()
    {
        CloseAllPanels();
    }
}
