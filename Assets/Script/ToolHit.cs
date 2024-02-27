using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolHit : MonoBehaviour
{
    public virtual void Hit()
    {
        
    }

    public virtual bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return true;
    }
}
