using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] GameObject selectedObject;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetPos = Physics2D.OverlapPoint(mousePos);

            if (targetPos && targetPos.tag == "Arrow")
            {
                ArrowController targetArrow = targetPos.GetComponent<ArrowController>();
                if (targetArrow.GetChangeOnce()) { return; }

                selectedObject = targetPos.transform.gameObject;
                targetArrow.mouseChange = true;
            }
        }

        if (selectedObject)
        {
            selectedObject.transform.position = new Vector3(mousePos.x, mousePos.y, 1);
            if (Input.GetMouseButtonUp(0))
            {
                selectedObject.GetComponent<ArrowController>().mouseChange = false;
                selectedObject.GetComponent<ArrowController>().doChange = true;
                selectedObject = null;
            }
        }

    }
}
