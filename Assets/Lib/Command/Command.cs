using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    // TODO Bring in the game input manager in order to handle this thing..
    protected Entity myEntity;
    public Command(Entity entity)
    {
        myEntity = entity;
    }
    public abstract void Execute();
}
