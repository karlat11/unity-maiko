using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelRotationManager : MonoBehaviour
{
    private bool pressedModel;
    private Vector3 hitPos;
    private float dist = 0;
    private GameObject model;

    private void Awake()
    {
        model = transform.gameObject;
    }

    private void Start()
    {
        pressedModel = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit))
            {
                if (rayHit.collider.tag == "Model")
                {
                    pressedModel = true;
                    hitPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) pressedModel = false;

        if (pressedModel)
        {
            if (dist != (hitPos.x - Input.mousePosition.x))
            {
                dist = hitPos.x - Input.mousePosition.x;
                model.transform.Rotate(0, model.transform.rotation.y + dist / 2, 0);
                hitPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            }
        }
    }
}
