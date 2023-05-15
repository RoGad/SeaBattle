using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SeaBattle3
{
    public partial class Form1 : Form
    {
        Redaktor sea_user;
        Redaktor sea_comp;

        Grid gridUser;
        Grid gridComp;

        Mission mission;

        // режимы игры
        enum Mode
        {
            EditShips,
            PlayUser,
            PlayComp,
            Finish
        };

        Mode mode;

        public Form1()
        {
            InitializeComponent();
            // создание редактора
            sea_user = new Redaktor();
            // присваение дилигатов
            sea_user.ShowShip = ShowUserShip; // показывает, что для отображения кораблей нужно использовать функцию ShowUserShip (для юзера)
            sea_user.ShowFight = ShowUserFight;


            sea_comp = new Redaktor();
            sea_comp.ShowShip = ShowCompShip; // показывает, что для отображения кораблей нужно использовать функцию ShowCompShip (для компа)
            sea_comp.ShowFight = ShowCompFight;

            // создание гридов
            gridUser = new Grid(grid_user);
            gridComp = new Grid(grid_comp);
            //InitGrid(grid_user);
            //InitGrid(grid_comp);

            //sea_user.Sbros();
            //sea_comp.Sbros();
            //sea_comp.PostavitRovno();

            ReStart();
        }

        private void ReStart()
        {
            mode = Mode.EditShips;
            sea_user.Sbros();
            sea_comp.Sbros();
            sea_comp.PostavitRovno();
            buttonRandom.Visible = true;
            buttonClear.Visible = true;
            buttonStart.Visible = true;
            ShowUnplacedShips();

        }

        // дилигаты (передаются в классы)
        // отображение корабля игрока
        private void ShowUserShip(Tochka place, int nr)
        {
            gridUser.ShowShip(place, nr);
        }

        // отображение корабля компьютера
        private void ShowCompShip(Tochka place, int nr)
        {
            if (mode == Mode.EditShips)
                gridComp.ShowShip(place, nr);
        }

        // отображение выстрела игрока 
        private void ShowUserFight(Tochka place, Status status)
        {
            gridUser.ShowFight(place, status);
        }

        // отображение выстрела компа
        private void ShowCompFight(Tochka place, Status status)
        {
            gridComp.ShowFight(place, status);
        }

        // обработка события нажатия клавиши мышки (отжатие (для размещения кораблей))
        private void grid_user_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                PlaceShip();
            grid_user.ClearSelection();
        }

        // для расположение кораблей случайным образом
        private void PlaceShip()
        {
            //если все корабли расставлены,то удалить их во время боя уже невозможно (при клике на корабли)
            if (mode != Mode.EditShips) return;

            Tochka[] ship = gridUser.GetSelectedCells();
            if (ship == null)
                return;
            if (ship.Length == 1)
                sea_user.OchistitTochku(ship[0]);
            sea_user.PostavitPoTochkam(ship);
            ShowUnplacedShips();
        }

        // отображение не размещенных кораблей (справа на поле компа) 
        private void ShowUnplacedShips()
        {
            sea_comp.PostavitRovno();
            for (int j = 0; j < Sea.vsegoKorablei; j++)
            {
                if (!sea_user.NetKorablya(j))
                    sea_comp.UbratKorabl(j);
            }
            // если все корабли раставлены, то кнопка "В бой!" становится кликабельно
            buttonStart.Enabled = (sea_user.sozdano == Sea.vsegoKorablei);
        }

        // обработка события установления корабля с помощью клавиш (Enter)
        private void grid_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PlaceShip();
            grid_user.ClearSelection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // обработчик нажатия на кнопку для рандомного расположения кораблей 
        private void button2_Click(object sender, EventArgs e)
        {
            if (mode != Mode.EditShips) return;
            else
            {
                sea_user.PostavitRandomno();
                ShowUnplacedShips();
            }
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            ReStart();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (mode != Mode.EditShips) return;
            sea_user.Sbros();
            ShowUnplacedShips();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (mode != Mode.EditShips) return;
            if (sea_user.sozdano == Sea.vsegoKorablei)
            {
                mode = Mode.PlayUser;
                sea_comp.PostavitRandomno();
                mission = new Mission(sea_user);
                buttonRandom.Visible = false;
                buttonClear.Visible = false;
                buttonStart.Visible = false;
            }
        }

        // обработчки выстрела по полю компьютера
        private void grid_comp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           grid_comp.ClearSelection();
           if (mode != Mode.PlayUser) return;
           Status status = sea_comp.Vistrel(new Tochka(e.ColumnIndex, e.RowIndex));
            // убрать выделение клетки при нажатии на клетку (синий)
            switch (status) {
                case Status.neisvestno:
                case Status.mimo:
                    mode = Mode.PlayComp; break;
                case Status.ranil:
                case Status.ubil:
                    mode = Mode.PlayUser; break;
                case Status.pobedil: mode = Mode.Finish; WinUser(); break;
            }
            while (mode == Mode.PlayComp) {
                CompFight();
            }
        }
        private void CompFight() {
            Tochka point;
            Status status = mission.Fight(out point);
            switch (status)
            {
                case Status.neisvestno:
                case Status.mimo:
                    mode = Mode.PlayUser; break;
                case Status.ranil:
                case Status.ubil:
                    mode = Mode.PlayComp; break;
                case Status.pobedil: mode = Mode.Finish; WinComp(); break;
            }
        }
        private void WinUser() {
            MessageBox.Show("Поздравляю. Победу одержал человек! Восстание компьютеров приостановлено :)");
            
        }
        private void WinComp()
        {
            MessageBox.Show("К сожеление человек потерпел поражение. Компьютеры захватят мир :(");

        }
    }
}