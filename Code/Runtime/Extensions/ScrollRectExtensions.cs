/*
 * Copyright (c) 2018-Present Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Common.General
{
    /// <summary>
    /// An extension class for scroll rects.
    /// </summary>
    public static class ScrollRectExtensions
    {
        /// <summary>
        /// Snaps the scroll rect to a rect transform in the scroll rect so you can scroll to a point.
        /// </summary>
        /// <param name="instance">The scroll rect to modify.</param>
        /// <param name="child">The child to focus on.</param>
        /// <returns></returns>
        public static Vector2 GetSnapToPositionToBringChildIntoView(this ScrollRect instance, RectTransform child)
        {
            Canvas.ForceUpdateCanvases();
            
            Vector2 viewportLocalPosition = instance.viewport.localPosition;
            Vector2 childLocalPosition = child.localPosition;
            Vector2 result = new Vector2(
                0 - (viewportLocalPosition.x + childLocalPosition.x),
                0 - (viewportLocalPosition.y + childLocalPosition.y)
            );
            
            return result;
        }
    }
}