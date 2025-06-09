using Prism.Commands;
using Prism.Mvvm;
using Sample.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace Sample.ViewModels
{
    internal class CellBtnVM : BindableBase
    {
        private CellStatus _status = CellStatus.Empty;
        public CellStatus Status
        {
            get => _status;

            set
            {
                _status = value;
                RaisePropertyChanged(nameof(Status));
            }
        }
        private readonly Action<CellBtnVM> _checkGameStatus;

        public ICommand CellClickCommand { get; }

        public DelegateCommand<object> SetStatusCommand { get; }

        private Action<CellBtnVM> _updateAppStatus;

        public int Row { get; }
        public int Column { get; }

        public CellBtnVM(int row, int column, Action<CellBtnVM> updateAppStatus)
        {
            Row = row;
            Column = column;
            _updateAppStatus = updateAppStatus;
            SetStatusCommand = new DelegateCommand<object>(SetStatus);
        }

        private void SetStatus(object status)
        {
            if (this.Status != CellStatus.Empty)
                return;

            Status = (CellStatus)status;
            _updateAppStatus?.Invoke(this);
        }
    }
}
