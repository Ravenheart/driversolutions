using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverSolutions
{
    public class Mess
    {
        public static DialogResult Info(string message, string caption = "Information")
        {
            return Mess.Generic(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Warning(string message, string caption = "Warning")
        {
            return Mess.Generic(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult Error(string message, string caption = "Error")
        {
            return Mess.Generic(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Question(string message, string caption = "Question")
        {
            return Mess.Generic(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult ShowException(Exception exception)
        {
            return Mess.Generic("An exception has been thrown: " + exception.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Generic(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return XtraMessageBox.Show(message, caption, buttons, icon);
        }
    }
}
