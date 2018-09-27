using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AES_UI_V1
{
    public class Updater
    {
        public static byte[] sRX { get; set; }

        public static bool F_Fan1_On()
        {

            if (sRX[0] == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void F_Fan2_On(Image fan2)
        {
            var controller = WpfAnimatedGif.ImageBehavior.GetAnimationController(fan2);


            if (sRX[1] == 0)
            {
                controller.Pause();
            }
            else if (sRX[1] == 1)
            {
                controller.Play();
            }
        }

    }
}
