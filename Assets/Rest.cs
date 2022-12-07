using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : GAction
{
    public override bool PrePerform()
    {
        target = GameObject.FindGameObjectWithTag("Lounge");
        return true;
    }

    public override bool PostPerform()
    {
        agentBeliefs.RemoveState("exhausted");
        return true;
    }
}
