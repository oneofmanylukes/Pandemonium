using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pandemonium.Classes
{
    public class ActionSetRandomTime : ActionBase
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;

            public SYSTEMTIME(DateTime dt)
            {
                wYear = (ushort)dt.Year;
                wMonth = (ushort)dt.Month;
                wDayOfWeek = (ushort)dt.DayOfWeek;
                wDay = (ushort)dt.Day;
                wHour = (ushort)dt.Hour;
                wMinute = (ushort)dt.Minute;
                wSecond = (ushort)dt.Second;
                wMilliseconds = (ushort)dt.Millisecond;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetSystemTime(ref SYSTEMTIME st);

        private SYSTEMTIME _randTime;

        public override Task DoChaos { get; set; }
        public override Task UndoChaos { get; set; }

        public ActionSetRandomTime()
        {
            var random = new Random(DateTime.Now.Millisecond);
            _randTime = new SYSTEMTIME()
            {
                wYear = (ushort)random.Next(1990, 2050),
                wMonth = (ushort)random.Next(1, 12),
                wDay = (ushort)random.Next(1, 25),
                wHour = (ushort)random.Next(1, 24),
                wMinute = (ushort)random.Next(1, 60),
                wSecond = (ushort)random.Next(1, 60)
            };

            DoChaos = new Task(() => { SetSystemTime(ref _randTime); });
        }
    }
}
