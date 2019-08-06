using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "new Accessory", menuName = "Items/Accessory")]
public class Accessory : Equip
{
    public float stat;
    public float weight;

    public override void Use()
    { 
    }
}
