using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightLightController : MonoBehaviour
{
    [SerializeField] GameObject mark;

    GameObject currentTarget;
    public void HightLight(GameObject target)
    {
        if(currentTarget == target)
        {
            return;
        }

        Vector3 position = target.transform.position;
        position.y += 1;
        HighLight(position);
    }

    public void HighLight(Vector3 position)
    {
        mark.SetActive(true);
        mark.transform.position = position;
    }

    public void Hide() {

        currentTarget = null;
        mark.SetActive(false); 
    }
}
