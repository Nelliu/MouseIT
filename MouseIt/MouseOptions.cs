using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseIt
{
    public class MouseOptions
    {

        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, IntPtr pvParam, UInt32 fWinIni);

        [DllImport("user32.dll")]
        public static extern int SystemParametersInfo(int uAction, int uParam, IntPtr lpvParam, int fuWinIni);


        [DllImport("user32.dll")]
        static extern uint GetDoubleClickTime();

        

        [DllImport("kernel32.dll")]
        public static extern int GetLastError();

        public const int SPI_GETMOUSESPEED = 112;
        public const int SPI_SETMOUSESPEED = 113;
        public const int SPI_GETWHEELSCROLLLINES = 104;
        public const int SPI_SETWHEELSCROLLLINES = 105;

        private static int intDefaulSpeed = 10;
        private static int intCurrentSpeed;
        private static int intNewSpeed;


        public static int SPI_SETDOUBLECLICKTIME = 0x0020;




        public static void GetDefaults()
        {
            intCurrentSpeed = GetMouseSpeed();
        }
        public static void SetDefaults()
        {
            if (intCurrentSpeed == 20)
            {
                SetMouseSpeed(intDefaulSpeed);
            }
            else if (intCurrentSpeed < 10)
            {
                SetMouseSpeed(intDefaulSpeed);
            }
        }

        public static int GetMouseSpeed()
        {
            int intSpeed = 0;
            IntPtr ptr;
            ptr = Marshal.AllocCoTaskMem(4);
            SystemParametersInfo(SPI_GETMOUSESPEED, 0, ptr, 0);
            intSpeed = Marshal.ReadInt32(ptr);
            Marshal.FreeCoTaskMem(ptr);

            return intSpeed;
        }

        public static void SetMouseSpeed(int intSpeed)
        {
            IntPtr ptr = new IntPtr(intSpeed);
            
            int b = SystemParametersInfo(SPI_SETMOUSESPEED, 0, ptr, 0);

        }

        public static void SetDoubleClickSpeed(double value)
        {
            double clickSpeed =  value;
            SystemParametersInfo(Convert.ToUInt32(SPI_SETDOUBLECLICKTIME), Convert.ToUInt32(clickSpeed), new IntPtr(4), 0);
        }


        public static int GetDoubleClick()
        {
            uint doubleClickTime = GetDoubleClickTime();
            return Convert.ToInt32(doubleClickTime);
        }




        public static int GetScrollSpeed()
        {
            int scrollSpeed;
            IntPtr ptr;
            ptr = Marshal.AllocCoTaskMem(4);
            SystemParametersInfo(SPI_GETWHEELSCROLLLINES, 0, ptr, 0);
            scrollSpeed = Marshal.ReadInt32(ptr);
            Marshal.FreeCoTaskMem(ptr);

            return scrollSpeed;
        }

        public static void SetScrollSpeed(double value)
        {

            SystemParametersInfo(SPI_SETWHEELSCROLLLINES, Convert.ToUInt32(value), new IntPtr(0), 0);
        }

        public static void SetMouse(double clickSpeed, double mousespeed, double scrollspeed)
        {
            SetDoubleClickSpeed(clickSpeed);
            SetMouseSpeed(Convert.ToInt32(mousespeed));
            SetScrollSpeed(scrollspeed);
        }
    }


}


