using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MoveToMouse : Command
{
    LayerMask groundMask;
    public MoveToMouse(Entity entity) : base(entity)
    {

    }
    public override void Execute()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // No layer mask right now
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log(hit);
            Move(hit.point);
        }
    }

    void Move(Vector3 position)
    {
        Vector3 startPos = myEntity.transform.position;
        Vector3 endPos = position;

        myEntity.transform.position = Vector3.MoveTowards(startPos, endPos, Time.deltaTime * myEntity.speed);
    }
}
