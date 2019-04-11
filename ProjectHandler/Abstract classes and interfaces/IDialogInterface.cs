using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecthandler.Templates_and_interfaces
{
    interface IDialogInterface<t>
    {
        void initializeListControls();
        void InitializeDialogValues();

        event EventHandler<t> OnSaveClicked;
        event EventHandler<t> OnEditClicked;
        event EventHandler<t> OnCancelClicked;

    }
}
