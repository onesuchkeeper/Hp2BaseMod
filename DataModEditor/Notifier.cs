using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DataModEditor
{
    public class Notifier
    {
        public void Error(string message)
        {
            MessageBox.Show(message);
        }
    }
}
