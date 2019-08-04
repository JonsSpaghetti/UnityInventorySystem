using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MoveRight : Command
{
    public MoveRight(Entity entity) : base(entity)
    {
    }

    public override void Execute()
    {
        myEntity.transform.Translate(Vector3.right * Time.deltaTime * myEntity.speed);
    }
}
