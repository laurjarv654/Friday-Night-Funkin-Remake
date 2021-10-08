using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Friday_Night_Funkin_Remake
{
    class Arrow
    {
        public int x, y, arrowNum;
        Image image;

        public Arrow(int _x, int _y, int _arrowNum, Image _image)
        {
            x = _x;
            y = _y;
            arrowNum = _arrowNum;
            image = _image;
        }

        public Image getImage()
        {
            return image;
        }

        public void setImage(Image newImage)
        {
            image = newImage;
        }
    }
}

