using System;
using System.Windows.Forms;
using QuanLyKhachSan.Forms;

namespace QuanLyKhachSan
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmMain());
        }
    }
}