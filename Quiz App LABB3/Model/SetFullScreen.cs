using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Quiz_App_LABB3.Model
{
    static class SetFullScreen
    {
        static bool isMax = false, isFull = false;
        static Point old_loc, default_loc;
        static Size old_size, default_size;

        public static void SetInitials(Window win)
        {
            old_size = new Size(win.Width, win.Height);
            old_loc = new Point(win.Top, win.Left);

            default_size = new Size(win.Width, win.Height);
            default_loc = new Point(win.Top, win.Left);

        }
        public static void Fullscreen(Window win)
        {
            if (isFull)
            {
                win.WindowState = WindowState.Normal;
                win.Left = old_loc.X;
                win.Top = old_loc.Y;
                win.Width = old_size.Width;
                win.Height = old_size.Height;
                isFull = false;
            }
            else
            {
                old_size = new Size(win.Width, win.Height);
                old_loc = new Point(win.Left, win.Top);
                win.WindowState = WindowState.Maximized;
                isFull = true;

            }
        }
    }
}
