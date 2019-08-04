using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class DoNothing : Command
{
    public DoNothing(Entity entity) : base(entity)
    {
    }

    public override void Execute()
    {
    }
}
