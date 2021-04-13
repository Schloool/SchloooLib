using UnityEngine;

namespace SchloooLib.Core
{
    /// <summary>Utility class to access screen positions relative to the overall screen size.</summary>
    public class ScreenUtils
    {
        /// <summary>Returns the y coordinate of the upper screen border.</summary>
        /// <param name="camera">The corresponding display camera.</param>
        /// <returns>The upper screen y coordinate in world coordinates.</returns>
        public static float GetTopWorldBorder(Camera camera)
        {
            return camera.ViewportToWorldPoint(Vector2.one).y;
        }

        /// <summary>Returns the y coordinate of the lower screen border.</summary>
        /// <param name="camera">The corresponding display camera.</param>
        /// <returns>The lower screen y coordinate in world coordinates.</returns>
        public static float GetBottomWorldBorder(Camera camera)
        {
            return camera.ViewportToWorldPoint(Vector2.zero).y;
        }

        /// <summary>Returns the x coordinate of the left screen border.</summary>
        /// <param name="camera">The corresponding display camera.</param>
        /// <returns>The left screen x coordinate in world coordinates.</returns>
        public static float GetLeftWorldBorder(Camera camera)
        {
            return camera.ViewportToWorldPoint(Vector2.zero).x;
        }
        
        /// <summary>Returns the x coordinate of the right screen border.</summary>
        /// <param name="camera">The corresponding display camera.</param>
        /// <returns>The right screen x coordinate in world coordinates.</returns>
        public static float GetRightWorldBorder(Camera camera)
        {
            return camera.ViewportToWorldPoint(Vector2.one).x;
        }

        /// <summary>Returns the position coordinate of the screen's center point.</summary>
        /// <param name="camera">The corresponding display camera.</param>
        /// <returns>The screen center coordinates in world coordinates.</returns>
        public static Vector2 GetWorldMiddlePoint(Camera camera)
        {
            return camera.ViewportToWorldPoint(Vector2.one * 0.5f);
        }

        /// <summary>Returns the y coordinate of the screen center position.</summary>
        /// <param name="camera">The corresponding display camera.</param>
        /// <returns>The screen center y coordinate in world coordinates.</returns>
        public static float GetVerticalWorldMiddle(Camera camera)
        {
            return GetWorldMiddlePoint(camera).y;
        }
        
        /// <summary>Returns the x coordinate of the screen center position.</summary>
        /// <param name="camera">The corresponding display camera.</param>
        /// <returns>The screen center x coordinate in world coordinates.</returns>
        public static float GetHorizontalWorldMiddle(Camera camera)
        {
            return GetWorldMiddlePoint(camera).x;
        }
    }
}
