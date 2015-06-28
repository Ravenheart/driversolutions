using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSolutions.BOL.Core
{
    public interface IReportProgress
    {
        event EventHandler<ProgressChangedEventArgs> OnProgressChanged;
        event EventHandler<RunWorkerCompletedEventArgs> OnComplete;
    }
}
