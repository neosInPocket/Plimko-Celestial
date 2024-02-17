using UnityEngine;

public static class ExtensionScript
{
    public static Vector2 ScreenSize;

    public static Vector3 GetPointsToWorld(Vector2 pos)
    {
        var camera = Camera.main.ScreenPointToRay(pos);

        var normalized = camera.direction;
        var sss = camera.origin;

        Vector3 currentNormal = new Vector3(0, 0, 1);
        Vector3 resultDow = new Vector3(0, 0, 0);

        float pr = Vector3.Dot(normalized, currentNormal);

        float vector2Magnitude = Vector3.Dot(resultDow - sss, currentNormal) / pr;

        Vector3 result = sss + vector2Magnitude * normalized;
        return result;
    }

    static ExtensionScript()
    {
        ScreenSize = GetPointsToWorld(new Vector3(Screen.width, Screen.height));
    }
}
