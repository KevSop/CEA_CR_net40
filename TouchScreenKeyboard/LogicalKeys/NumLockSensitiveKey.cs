﻿using System.Collections.Generic;
using WindowsInput;

namespace TouchScreenKeyboard.LogicalKeys
{
    public class NumLockSensitiveKey : MultiCharacterKey
    {
        public NumLockSensitiveKey(VirtualKeyCode keyCode, IList<string> keyDisplays)
            : base(keyCode, keyDisplays)
        {
        }
    }
}