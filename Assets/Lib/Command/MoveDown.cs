using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MoveDown : Command
{
    public MoveDown(Entity entity) : base(entity)
    {
    }

    public override void Execute()
    {
        myEntity.transform.Translate(Vector3.down * Time.deltaTime * myEntity.speed);
    }
}
