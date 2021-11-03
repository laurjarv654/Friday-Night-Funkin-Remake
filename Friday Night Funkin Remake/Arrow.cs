using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Friday_Night_Funkin_Remake
{
    class Arrow
    {
        public int x, y, arrowNum, note;
        Image image;

        public Arrow(int _x, int _y, int _arrowNum, Image _image, int _note)
        {
            x = _x;
            y = _y;
            arrowNum = _arrowNum;
            image = _image;
            note = _note;
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

