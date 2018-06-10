#region Header

//-----------------------------------------------------------------
//   Class:          MouseUtils
//   Description:    Mouse control utils.
//   Author:         Stridemann, nymann        Date: 08.26.2017
//-----------------------------------------------------------------

#endregion

using System;
using System.Runtime.InteropServices;
using System.Threading;
using SharpDX;

namespace Random_Features.Libs
{
    public class Mouse
    {
        public const int MouseeventfLeftdown = 0x02;
        public const int MouseeventfLeftup   = 0x04;

        public const int MouseeventfMiddown = 0x0020;
        public const int MouseeventfMidup   = 0x0040;

        public const int MouseeventfRightdown = 0x0008;
        public const int MouseeventfRightup   = 0x0010;
        public const int MouseEventWheel      = 0x800;

        // 
        private const int MovementDelay = 10;
        private const int ClickDelay    = 1;

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int DwFlags, int Dx, int Dy, int CButtons, int DwExtraInfo);


        /// <summary>
        ///     Sets the cursor position relative to the game window.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="GameWindow"></param>
        /// <returns></returns>
        public static bool SetCursorPos(int X, int Y, RectangleF GameWindow) => SetCursorPos(X + (int) GameWindow.X, Y + (int) GameWindow.Y);

        /// <summary>
        ///     Sets the cursor position to the center of a given rectangle relative to the game window
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="GameWindow"></param>
        /// <returns></returns>
        public static bool SetCurosPosToCenterOfRec(RectangleF Position, RectangleF GameWindow)
        {
            return SetCursorPos((int)(GameWindow.X + Position.Center.X), (int)(GameWindow.Y + Position.Center.Y));
        }

        /// <summary>
        ///     Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point LpPoint);

        public static SharpDX.Point GetCursorPosition()
        {
            GetCursorPos(out Point LpPoint);
            return LpPoint;
        }

        public static void LeftClick(int ExtraDelay, int StartDelay = 0)
        {
            if (StartDelay > 0)
            {
                Thread.Sleep(StartDelay);
            }
            LeftMouseDown();
            Thread.Sleep(ClickDelay / 2);
            LeftMouseUp();
            Thread.Sleep(ClickDelay);
        }

        public static void RightClick(int ExtraDelay, int StartDelay = 0)
        {
            if (StartDelay > 0)
            {
                Thread.Sleep(StartDelay);
            }
            RightMouseDown();
            Thread.Sleep(ClickDelay / 2);
            RightMouseUp();
            Thread.Sleep(ClickDelay);
        }

        public static void LeftMouseDown() { mouse_event(MouseeventfLeftdown, 0, 0, 0, 0); }

        public static void LeftMouseUp() { mouse_event(MouseeventfLeftup, 0, 0, 0, 0); }

        public static void RightMouseDown() { mouse_event(MouseeventfRightdown, 0, 0, 0, 0); }

        public static void RightMouseUp() { mouse_event(MouseeventfRightup, 0, 0, 0, 0); }

        public static void SetCursorPosAndLeftClick(Vector2 Coords, int ExtraDelay)
        {
            int PosX = (int) Coords.X;
            int PosY = (int) Coords.Y;
            SetCursorPos(PosX, PosY);
            Thread.Sleep(MovementDelay + ExtraDelay);
            mouse_event(MouseeventfLeftdown, 0, 0, 0, 0);
            Thread.Sleep(ClickDelay);
            mouse_event(MouseeventfLeftup, 0, 0, 0, 0);
        }

        public static void VerticalScroll(bool Forward, int Clicks)
        {
            if (Forward)
                mouse_event(MouseEventWheel, 0, 0, Clicks * 120, 0);
            else
                mouse_event(MouseEventWheel, 0, 0, -(Clicks * 120), 0);
        }
        ////////////////////////////////////////////////////////////


        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int X;
            public int Y;

            public static implicit operator SharpDX.Point(Point Point) => new SharpDX.Point(Point.X, Point.Y);
        }

        #region MyFix

        private static void SetCursorPosition(float X, float Y) { SetCursorPos((int) X, (int) Y); }

        public static Vector2 GetCursorPositionVector()
        {
            SharpDX.Point CurrentMousePoint = GetCursorPosition();
            return new Vector2(CurrentMousePoint.X, CurrentMousePoint.Y);
        }

        public static void SetCursorPosition(Vector2 End)
        {
            Vector2 Cursor       = GetCursorPositionVector();
            Vector2 StepVector2  = new Vector2();
            float   Step         = (float) Math.Sqrt(Vector2.Distance(Cursor, End)) * 1.618f;
            if (Step > 275) Step = 240;
            StepVector2.X        = (End.X - Cursor.X) / Step;
            StepVector2.Y        = (End.Y - Cursor.Y) / Step;
            float FX             = Cursor.X;
            float FY             = Cursor.Y;
            for (int J = 0; J < Step; J++)
            {
                FX += +StepVector2.X;
                FY += StepVector2.Y;
                SetCursorPosition(FX, FY);
                Thread.Sleep(2);
            }
        }

        public static void SetCursorPosAndLeftClickHuman(Vector2 Coords, int ExtraDelay)
        {
            SetCursorPosition(Coords);
            Thread.Sleep(MovementDelay + ExtraDelay);
            LeftMouseDown();
            Thread.Sleep(MovementDelay + ExtraDelay);
            LeftMouseUp();
        }

        public static void SetCursorPos(Vector2 Vec) { SetCursorPos((int) Vec.X, (int) Vec.Y); }

        #endregion
    }
}