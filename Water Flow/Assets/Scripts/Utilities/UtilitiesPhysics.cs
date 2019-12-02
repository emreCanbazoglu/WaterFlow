using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static partial class Utilities
{
    public static bool IsObjectTouched(Collider collider, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        RaycastHit hitInfo;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 100);

        if (Physics.Raycast(ray, out hitInfo, 200f))
            return hitInfo.collider == collider;

        return false;
    }
}
