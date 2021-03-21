using UnityEngine;

namespace SchloooLib.Core
{
    public class ScreenUtils
    {
        public static float GetTopWorldBorder(Camera camera)
        {
            return camera.ViewportToWorldPoint(Vector2.one).y;
        }

        public static float GetBottomWorldBorder(Camera camera)
        {
            return camera.ViewportToWorldPoint(Vector2.zero).y;
        }

        public static float GetLeftWorldBorder(Camera camera)
        {
            return camera.ViewportToWorldPoint(Vector2.zero).x;
        }
        
        public static float GetRightWorldBorder(Camera camera)
        {
            return camera.ViewportToWorldPoint(Vector2.one).x;
        }

        public static Vector2 GetWorldMiddlePoint(Camera camera)
        {
            return camera.ViewportToWorldPoint(Vector2.one * 0.5f);
        }

        public static float GetVerticalWorldMiddle(Camera camera)
        {
            return GetWorldMiddlePoint(camera).y;
        }
        
        public static float GetHorizontalWorldMiddle(Camera camera)
        {
            return GetWorldMiddlePoint(camera).x;
        }
    }
}
