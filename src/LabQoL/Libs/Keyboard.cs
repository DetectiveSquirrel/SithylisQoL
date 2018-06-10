#region Header

//-----------------------------------------------------------------
//   Class:          VirtualKeyboard
//   Description:    Keyboard control utils.
//   Author:         Stridemann, nymann        Date: 08.26.2017
//-----------------------------------------------------------------

#endregion

using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Random_Features.Libs
{
    public static class Keyboard
    {
        private const int Keyeventf_Extendedkey = 0x0001;
        private const int Keyeventf_Keyup = 0x0002;

        private const int Action_Delay = 1;

        [DllImport("user32.dll")]
        private static extern uint keybd_event(byte BVk, byte BScan, int DwFlags, int DwExtraInfo);


        public static void KeyDown(Keys Key) { keybd_event((byte) Key, 0, Keyeventf_Extendedkey | 0, 0); }

        public static void KeyUp(Keys Key)
        {
            keybd_event((byte) Key, 0, Keyeventf_Extendedkey | Keyeventf_Keyup, 0); //0x7F
        }

        public static void KeyPress(Keys Key)
        {
            KeyDown(Key);
            Thread.Sleep(Action_Delay);
            KeyUp(Key);
        }

        [DllImport("USER32.dll")]
        private static extern short GetKeyState(int NVirtKey);

        public static bool IsKeyDown(int NVirtKey) => GetKeyState(NVirtKey) < 0;
    }
}