using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum ResourceNodeType
{
    Undefine,
    Tree,
    Ore
}

[CreateAssetMenu(menuName = "Data/Tool Action/Gather Resource Node")]
public class GatherResourseNode : ToolAction
{

    [SerializeField] float sizeOfInteractableArea = 1f;
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;

    public override bool OnApply(Vector2 worldPoint)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);

        foreach (Collider2D c in collider2Ds)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                if (hit.CanBeHit(canHitNodesOfType))
                {
                    hit.Hit();
                    return true;
                }
                
            }
        }
        return false;

    }
}
