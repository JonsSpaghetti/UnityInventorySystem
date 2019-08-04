using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MoveLeft : Command
{
    public MoveLeft(Entity entity) : base(entity)
    {
    }

    public override void Execute()
    {
        myEntity.transform.Translate(Vector3.left * Time.deltaTime * myEntity.speed);
    }
}
