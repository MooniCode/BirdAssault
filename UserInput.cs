using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class UserInput
    {
        private static KeyboardState _currentKeyBoardState;
        private static KeyboardState _previousKeyboardState;

        public static bool IsKeyDown(Keys key)
        {
            return _currentKeyBoardState.IsKeyDown(key);
        }
        public static bool IsKeyUp(Keys key)
        {
            return _currentKeyBoardState.IsKeyUp(key);
        }
        public static bool WasKeyPressed(Keys key)
        {
            return _currentKeyBoardState.IsKeyUp(key) && _previousKeyboardState.IsKeyDown(key);
        }
        public static void Update()
        {
            _previousKeyboardState = _currentKeyBoardState;
            _currentKeyBoardState = Keyboard.GetState();
        }
    }
}
