using System.Collections;
using SchloooLib.Core;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace SchloooLib.Tests
{
    public class ScreenUtilTests
    {
        private readonly Camera camera;

        public ScreenUtilTests()
        {
            camera = new GameObject().AddComponent<Camera>();
            camera.orthographic = true;
        }

        [UnityTest]
        public IEnumerator Top_World_Border_Is_Bigger_Than_Bottom()
        {
            float topBorder = ScreenUtils.GetTopWorldBorder(camera);
            float bottomBorder = ScreenUtils.GetBottomWorldBorder(camera);

            Assert.IsTrue(topBorder >= bottomBorder);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Right_World_Border_Is_Bigger_Than_Left()
        {
            float leftBorder = ScreenUtils.GetLeftWorldBorder(camera);
            float rightBorder = ScreenUtils.GetRightWorldBorder(camera);
            
            Assert.IsTrue(rightBorder >= leftBorder);
            yield return null;
        }

        [UnityTest]
        public IEnumerator World_Middle_Point_Matches_Camera_Transform()
        {
            Vector2 worldMiddlePoint = ScreenUtils.GetWorldMiddlePoint(camera);
            Vector2 cameraPosition = camera.transform.position;
            
            Assert.AreApproximatelyEqual(cameraPosition.x, worldMiddlePoint.x);
            Assert.AreApproximatelyEqual(cameraPosition.y, worldMiddlePoint.y);
            yield return null;
        }
    }
}