using Sample.Models;
using Sample.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ViewModels
{
    internal class GameVsComputerWindowViewModel
    {
        private CellStatus _currentPlayerStatus;

        
        public CellStatus CurrentPlayerStatus
        {
            get => _currentPlayerStatus;
            set
            {
                _currentPlayerStatus = value;
                RaisePropertyChanged(nameof(CurrentPlayerStatus));
            }
        }

        public List<List<CellBtnVM>> AllCells { get; } = new List<List<CellBtnVM>>();
        public int _cellRowColumnCount = 3;
        private bool _isGameStopped = false; // Флаг, указывающий, что игра остановлена
        private static bool _isBotTurn = false; // Флаг, указывающий, что сейчас ход бота

        public static bool IsBotTurn => _isBotTurn;

        public GameVsComputerWindowViewModel()
        {
            CurrentPlayerStatus = CellStatus.Cross; // Крестики ходят первыми
            for (int row = 0; row < _cellRowColumnCount; row++)
            {
                var newRow = new List<CellBtnVM>();
                for (int column = 0; column < _cellRowColumnCount; column++)
                {
                    var newCell = new CellBtnVM(row, column, CheckGameStatus);
                    newRow.Add(newCell);
                }
                AllCells.Add(newRow);
            }
        }

        private void CheckGameStatus(CellBtnVM lastClickBtn)
        {
            if (_isGameStopped) return;  // Не обрабатываем клики, если игра остановлена

            // Ваша существующая логика проверки выигрыша (оставлена без изменений)
            if (AllCells.SelectMany(x => x.Where(c => c.Status == CellStatus.Empty)).Count() == 0)
            {
                StopGame();
                return;
            }
            var columnCheck = new List<CellBtnVM>();
            for (int i = 0; i <= _cellRowColumnCount / 2; i++)
            {
                int column = lastClickBtn.Column + 1 + i;
                if (column < _cellRowColumnCount)
                {
                    columnCheck.Add(AllCells[lastClickBtn.Row][column]);
                }
                else
                {
                    columnCheck.Add(AllCells[lastClickBtn.Row][column - _cellRowColumnCount]);
                }
            }
            if (columnCheck.All(x => x.Status == lastClickBtn.Status))
            {
                StopGame(lastClickBtn.Status);
                return;
            }
            var rowCheck = new List<CellBtnVM>();
            for (int i = 0; i <= _cellRowColumnCount / 2; i++)
            {
                int row = lastClickBtn.Row + 1 + i;
                if (row < _cellRowColumnCount)
                {
                    rowCheck.Add(AllCells[row][lastClickBtn.Column]);
                }
                else
                {
                    rowCheck.Add(AllCells[row - _cellRowColumnCount][lastClickBtn.Column]);
                }
            }
            if (rowCheck.All(x => x.Status == lastClickBtn.Status))
            {
                StopGame(lastClickBtn.Status);
                return;
            }
            bool checkByDiagonal = true;
            int checkCrossNum = Math.Abs(lastClickBtn.Column - lastClickBtn.Row);
            if (checkCrossNum != 0 && checkCrossNum != _cellRowColumnCount - 1)
            {
                checkByDiagonal = false;
            }
            if (checkByDiagonal && !(lastClickBtn.Row == 0 && lastClickBtn.Column == _cellRowColumnCount - 1) && !(lastClickBtn.Row == _cellRowColumnCount - 1 && lastClickBtn.Column == 0))
            {
                var crossCheck1 = new List<CellBtnVM>();
                for (int i = 0; i <= _cellRowColumnCount / 2; i++)
                {
                    int column = lastClickBtn.Column + 1 + i;
                    int row = lastClickBtn.Row + 1 + i;
                    if (column < _cellRowColumnCount)
                    {
                        crossCheck1.Add(AllCells[row][column]);
                    }
                    else
                    {
                        crossCheck1.Add(AllCells[row - _cellRowColumnCount][column - _cellRowColumnCount]);
                    }
                }
                if (crossCheck1.All(x => x.Status == lastClickBtn.Status))
                {
                    StopGame(lastClickBtn.Status);
                    return;
                }
            }
            if (checkByDiagonal)
            {
                var crossCheck2 = new List<CellBtnVM>();
                for (int i = 0; i <= _cellRowColumnCount / 2; i++)
                {
                    int column = lastClickBtn.Column + 1 + i;
                    int row = lastClickBtn.Row - (1 + i);
                    if (row < 0)
                    {
                        row = row + _cellRowColumnCount;
                    }
                    if (column >= _cellRowColumnCount)
                    {
                        column = column - _cellRowColumnCount;
                    }
                    crossCheck2.Add(AllCells[row][column]);
                }
                if (crossCheck2.All(x => x.Status == lastClickBtn.Status))
                {
                    StopGame(lastClickBtn.Status);
                    return;
                }
            }

            CurrentPlayerStatus = lastClickBtn.Status == CellStatus.Cross ? CellStatus.Circle : CellStatus.Cross;

            // Ход бота после хода игрока
            if (CurrentPlayerStatus == CellStatus.Circle) // Если сейчас ход кружков (бот)
            {
                MakeBotMove();
            }
        }


        private void StopGame()
        {
            //MessageBoxResult result = MessageBox.Show(...); // Убираем стандартный MessageBox
            bool result = MyMessageBox.Show("Игра завершена без победителя. Что вы хотите сделать?");

            if (result) // Если MyMessageBox.Show вернул true (пользователь нажал "Повторить")
            {
                Reset();
            }
            else
            {
                GoToMainMenu();
            }
        }
        private void StopGame(CellStatus winnerStatus)
        {
            bool result;
            if (winnerStatus == CellStatus.Cross)
            {
                result = MyMessageBox.Show("Победитель: Крестик. Что вы хотите сделать?");
            }
            else
            {
                result = MyMessageBox.Show($"Победитель: Круг. Что вы хотите сделать?");
            }

            if (result) // Если MyMessageBox.Show вернул true (пользователь нажал "Повторить")
            {
                Reset();
            }
            else
            {
                GoToMainMenu();
            }
        }

        private void Reset()
        {
            foreach (var row in AllCells)
            {
                foreach (var cell in row)
                {
                    cell.Status = CellStatus.Empty;
                }
            }
        }
        private void GoToMainMenu()
        {
            // Логика для возврата в главное меню
            // Например, закрытие текущего окна и открытие главного меню
            // Или переход к другому представлению в вашем приложении
            MainPageWindow mainWindow = new MainPageWindow();
            mainWindow.Show();

        }
        private async Task MakeBotMove()
        {
            _isBotTurn = true;
            await Task.Delay(100);  // Задержка для имитации обдумывания

            // Логика хода бота (случайный выбор свободной ячейки)
            var emptyCells = AllCells.SelectMany(x => x.Where(c => c.Status == CellStatus.Empty)).ToList();

            if (emptyCells.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(emptyCells.Count);
                CellBtnVM cellToMark = emptyCells[index];
                cellToMark.Status = CellStatus.Circle;  // Бот ставит кружок
                CheckGameStatus(cellToMark);
            }
            else
            {
                StopGame(); // Ничья
            }
            _isBotTurn = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
