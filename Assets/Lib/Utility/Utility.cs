using UnityEngine;

class Utility : MonoBehaviour
{

    /// <summary>
    /// Utility class to get angle of rotation to the passed in worldPosition. 
    /// </summary>
    /// <param name="gObj"></param>
    /// <returns></returns>
    public static float GetRotationToWorldPos(GameObject gObj, Vector3 worldPosition, float yOffset)
    {
        //Get angle in radians
        float AngleRad = Mathf.Atan2(worldPosition.x - gObj.transform.position.x, worldPosition.y - gObj.transform.position.y + yOffset);
        //Convert to degrees - 0-360.
        if (AngleRad < 0)
        {
            AngleRad += 2 * Mathf.PI;
        }
        return (180 / Mathf.PI) * AngleRad;
    }

    /// <summary>
    /// Utility class to get world position of mouse.
    /// </summary>
    /// <returns>Vector3 representing world position of mouse.</returns>
    public static Vector3 GetWorldPositionOfMouse(GameObject gObj)
    {
        //Get point of mouse cursor (WORLD point, not screen point).
        float camDistance = Camera.main.transform.position.y - gObj.transform.position.y;
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDistance));

    }
}
