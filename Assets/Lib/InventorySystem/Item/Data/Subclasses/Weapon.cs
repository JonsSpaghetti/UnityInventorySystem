using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu (fileName = "new Weapon", menuName = "Items/Weapon")]
public class Weapon : Equip
{
    public float damage;
    public float weight;

    public override void Use()
    { 
    }
}
