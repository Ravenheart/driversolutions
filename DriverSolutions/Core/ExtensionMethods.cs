using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverSolutions
{
    public static class ExtensionMethods
    {
        public static uint[] GetCheckedValues(this CheckedComboBoxEdit box, object editvalue)
        {
            if (editvalue == null || string.IsNullOrWhiteSpace(editvalue.ToString()))
                return new uint[0];

            return editvalue.ToString().Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => Convert.ToUInt32(i.Trim())).ToArray();
        }
        public static uint[] GetCheckedValues(this CheckedComboBoxEdit box)
        {
            return ExtensionMethods.GetCheckedValues(box, box.EditValue);
        }

        public static void TryShowPopup(this Control box, string property)
        {
            if (string.IsNullOrWhiteSpace(property))
                return;

            var find = box.Controls.Find(property, true);
            if (find != null && find.Length > 0)
            {
                var control = find[0] as PopupBaseEdit;
                if (control != null)
                    control.ShowPopup();
                else if (find[0] is BaseEdit)
                    (find[0] as BaseEdit).Select();
            }
        }
    }
}
