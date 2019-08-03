using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Pandemonium.Classes
{
    public class ActionSwapMouseButtons : ActionBase
    {
        [DllImport("user32.dll")]
        private static extern int SwapMouseButton(int bSwap); //0 default, 1 is swapped

        public override Task DoChaos { get; set; }
        public override Task UndoChaos { get; set; }

        public ActionSwapMouseButtons()
        {
            DoChaos = new Task(() => { SwapMouseButton(1); });
            UndoChaos = new Task(() => { SwapMouseButton(0); });
        }
    }
}
