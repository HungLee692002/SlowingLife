using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCutable : ToolHit
{
    public override void Hit()
    {
        Destroy(gameObject);
    }

}
