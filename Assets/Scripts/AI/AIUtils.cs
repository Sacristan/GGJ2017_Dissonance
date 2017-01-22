using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIUtils
{
    public static Vector3 FindSuitableRandomPosition(Vector3 origin, float radius)
    {
        Vector3 result = origin;

        result.x += Random.Range(-radius, radius);
        result.y += 1f;
        result.z += Random.Range(-radius, radius);

        return result;
    }
}
