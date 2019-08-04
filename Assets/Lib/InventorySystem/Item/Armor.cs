using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu (fileName = "new Armor", menuName = "Items/Armor")]
public class Armor : Equip
{
    public float mitigation;
    public float weight;

    public override void Use()
    { 
    }
}
