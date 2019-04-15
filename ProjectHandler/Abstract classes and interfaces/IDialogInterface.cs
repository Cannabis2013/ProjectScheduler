using System;

namespace Templates
{
    interface IDialogInterface<t>
    {
        void InitializeControls();
        void InitializeDialogValues();

        event EventHandler<t> OnSaveClicked;
        event EventHandler<t> OnEditClicked;
        event EventHandler<t> OnCancelClicked;

    }
}
