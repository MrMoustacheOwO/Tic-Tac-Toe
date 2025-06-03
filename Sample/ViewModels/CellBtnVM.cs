using Prism.Commands;
using Prism.Mvvm;
using Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ViewModels
{
    internal class CellBtnVM: BindableBase
    {
        private CellStatus _status=CellStatus.Empty;
        
        public CellStatus Status
        {
            get { return _status; }
            set 
            {
                _status = value; 
                RaisePropertyChanged(nameof(Status));
            }
        }

        public DelegateCommand<CellStatus> SetStatusCommand { get;  }

        private Action<CellBtnVM> _updateAppStatus;

        public int Row { get; }
        public int Column { get; }

        public CellBtnVM(int row, int column, Action<CellBtnVM> updateAppStatus)
        {
            Row = row;
            Column = column;
            _updateAppStatus = updateAppStatus;
            SetStatusCommand=new DelegateCommand<CellStatus>(SetStatus);
        }

        private void SetStatus(CellStatus status)
        {
            if (this.Status != CellStatus.Empty) return;
            Status = status;
            _updateAppStatus?.Invoke(this);
        }
    }
}
