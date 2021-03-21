using System.Linq;
using UnityEngine;

namespace SchloooLib.Core
{
    /// <summary>
    /// Utility class to get and utilize colors from Unity context. 
    /// </summary>
    public class ColorUtils
    {
        /// <summary>
        /// Gets the subtractive mixed <see cref="Color"/> of all colors the <see cref="SpriteRenderer"/>'s <see cref="Sprite"/> contains.
        /// </summary>
        /// <param name="spriteRenderer">The <see cref="SpriteRenderer"/> which will be used to get the colors of its <see cref="Sprite"/>.</param>
        /// <param name="uniqueColorsOnly">Specifies whether all or only unique colored pixels will be taken into consideration.</param>
        /// <returns>The resulting mixed <see cref="Color"/>.</returns>
        public static Color GetSpriteRendererMixedColor(SpriteRenderer spriteRenderer, bool uniqueColorsOnly = false)
        {
            return GetSpriteMixedColor(spriteRenderer.sprite);
        }

        /// <summary>
        /// Gets the subtractive mixed <see cref="Color"/> of all colors the <see cref="Sprite"/> contains.
        /// </summary>
        /// <param name="sprite">The <see cref="Sprite"/> which will be used to get the colors.</param>
        /// <param name="uniqueColorsOnly">Specifies whether all or only unique colored pixels will be taken into consideration.</param>
        /// <returns>The resulting mixed <see cref="Color"/>.</returns>
        public static Color GetSpriteMixedColor(Sprite sprite, bool uniqueColorsOnly = false)
        {
            return GetMixedColor(GetSpriteColors(sprite));
        } 
        
        /// <summary>
        /// Gets all <see cref="Color"/>s a <see cref="SpriteRenderer"/>s <see cref="Sprite"/> contains.
        /// </summary>
        /// <param name="spriteRenderer">The <see cref="SpriteRenderer"/> which will be used to get the colors of its <see cref="Sprite"/>.</param>
        /// <param name="uniqueColorsOnly">Specifies whether all or only unique colored pixels will be taken into consideration.</param>
        /// <returns>Array of all colors used on the <see cref="SpriteRenderer"/>s <see cref="Sprite"/>.</returns>
        public static Color[] GetSpriteRendererColors(SpriteRenderer spriteRenderer, bool uniqueColorsOnly = false)
        {
            return GetSpriteColors(spriteRenderer.sprite, uniqueColorsOnly);
        }
        
        /// <summary>
        /// Gets all <see cref="Color"/>s a <see cref="Sprite"/> contains.
        /// </summary>
        /// <param name="sprite">The <see cref="Sprite"/> which will be used to get the <see cref="Color"/>s.</param>
        /// <param name="uniqueColorsOnly">Specifies whether all or only unique colored pixels will be taken into consideration.</param>
        /// <returns>Array of all colors used on the <see cref="Sprite"/></returns>
        public static Color[] GetSpriteColors(Sprite sprite, bool uniqueColorsOnly = false)
        {
            Color[] allColors = sprite.texture.GetPixels();

            return uniqueColorsOnly ? allColors.ToList().Distinct().ToArray() : allColors;
        }

        /// <summary>
        /// Calculates the subtractive mixed <see cref="Color"/> from a multitude of colors.
        /// </summary>
        /// <param name="colors"><see cref="Color"/>s that will be mixed.</param>
        /// <returns>The resulting mixed <see cref="Color"/>.</returns>
        public static Color GetMixedColor(params Color[] colors)
        {
            float r = colors.Average(color => color.r);
            float g = colors.Average(color => color.g);
            float b = colors.Average(color => color.b);

            return new Color(r, g, b);
        }
    }
}