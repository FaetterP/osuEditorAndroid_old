using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Element
{
    class SliderPoint
    {
        public int x, y;
        private bool isLocked;

        public SliderPoint(int x, int y)
        {
            this.x = x; this.y = y; this.isLocked = false;
        }

        public void SwitchLocked()
        {
            if (isLocked) { isLocked = false; }
            else { isLocked = true; }
        }

        public bool GetIsLocked()
        {
            return isLocked;
        }
    }
}
