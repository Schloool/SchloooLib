using NUnit.Framework;
using SchloooLib.Core;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests
{
    public class ColorUtilsTests
    {
        [Test]
        public void Same_Color_Mixes_To_Same_Result()
        {
            Color exampleColor = Color.blue;

            Color mixedColor = ColorUtils.GetMixedColor(exampleColor, exampleColor);
            
            Assert.AreEqual(exampleColor, mixedColor);
        }
        
        [Test]
        public void Black_And_White_Mix_Correctly()
        {
            Color mixedColor = ColorUtils.GetMixedColor(Color.black, Color.white);
            
            Color expected = new Color(0.5f, 0.5f, 0.5f);
            Assert.AreEqual(expected, mixedColor);
        }

        [Test]
        public void Red_And_Blue_Mix_To_Magenta()
        {
            Color mixedColor = ColorUtils.GetMixedColor(Color.red, Color.blue);

            Color expected = new Color(0.5f, 0f, 0.5f);
            Assert.AreEqual(expected, mixedColor);
        }
    }
}
