using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MoveUp : Command
{
    public MoveUp(Entity entity) : base(entity)
    {
    }

    public override void Execute()
    {
        myEntity.transform.Translate(Vector3.up * Time.deltaTime * myEntity.speed);
    }
}
