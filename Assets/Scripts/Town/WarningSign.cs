using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSign : MonoBehaviour
{
    private static readonly string sceneObjectName = "WarningSign";

    public static bool IsInScene()
    {
        return GameObject.Find(sceneObjectName) != null;
    }

    /// <summary>
    /// True is left, false is right
    /// </summary>
    /// <returns>Direction of path that is more challenging</returns>
    public static bool GetDirectionOfChallengeFromScene()
    {
        return GameObject.Find(sceneObjectName).GetComponent<WarningSign>().left;
    }

    /// <summary>
    /// Determines if the given path has increased reward and difficulty!
    /// </summary>
    public bool left = false;

}
