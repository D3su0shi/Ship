using UnityEngine;

public static class ScreenUtils
{
    #region Fields

    static int screenWidth;
    static int screenHeight;

    static float screenLeft;
    static float screenRight;
    static float screenTop;
    static float screenBottom;

    #endregion

    #region Properties

    public static float ScreenLeft
    {
        get { return screenLeft; }
    }

    public static float ScreenRight
    {
        get { return screenRight; }
    }

    public static float ScreenTop
    {
        get { return screenTop; }
    }

    public static float ScreenBottom
    {
        get { return screenBottom; }
    }

    #endregion

    public static void Initialize()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        Camera camera = Camera.main;

        if (camera == null)
        {
            return;
        }

        Vector3 lowerLeftCorner = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        screenLeft = lowerLeftCorner.x;
        screenBottom = lowerLeftCorner.y;

        Vector3 upperRightCorner = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
        screenRight = upperRightCorner.x;
        screenTop = upperRightCorner.y;
    }
}