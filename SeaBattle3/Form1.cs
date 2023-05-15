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

        // ������ ����
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
            // �������� ���������
            sea_user = new Redaktor();
            // ���������� ���������
            sea_user.ShowShip = ShowUserShip; // ����������, ��� ��� ����������� �������� ����� ������������ ������� ShowUserShip (��� �����)
            sea_user.ShowFight = ShowUserFight;


            sea_comp = new Redaktor();
            sea_comp.ShowShip = ShowCompShip; // ����������, ��� ��� ����������� �������� ����� ������������ ������� ShowCompShip (��� �����)
            sea_comp.ShowFight = ShowCompFight;

            // �������� ������
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

        // �������� (���������� � ������)
        // ����������� ������� ������
        private void ShowUserShip(Tochka place, int nr)
        {
            gridUser.ShowShip(place, nr);
        }

        // ����������� ������� ����������
        private void ShowCompShip(Tochka place, int nr)
        {
            if (mode == Mode.EditShips)
                gridComp.ShowShip(place, nr);
        }

        // ����������� �������� ������ 
        private void ShowUserFight(Tochka place, Status status)
        {
            gridUser.ShowFight(place, status);
        }

        // ����������� �������� �����
        private void ShowCompFight(Tochka place, Status status)
        {
            gridComp.ShowFight(place, status);
        }

        // ��������� ������� ������� ������� ����� (������� (��� ���������� ��������))
        private void grid_user_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                PlaceShip();
            grid_user.ClearSelection();
        }

        // ��� ������������ �������� ��������� �������
        private void PlaceShip()
        {
            //���� ��� ������� �����������,�� ������� �� �� ����� ��� ��� ���������� (��� ����� �� �������)
            if (mode != Mode.EditShips) return;

            Tochka[] ship = gridUser.GetSelectedCells();
            if (ship == null)
                return;
            if (ship.Length == 1)
                sea_user.OchistitTochku(ship[0]);
            sea_user.PostavitPoTochkam(ship);
            ShowUnplacedShips();
        }

        // ����������� �� ����������� �������� (������ �� ���� �����) 
        private void ShowUnplacedShips()
        {
            sea_comp.PostavitRovno();
            for (int j = 0; j < Sea.vsegoKorablei; j++)
            {
                if (!sea_user.NetKorablya(j))
                    sea_comp.UbratKorabl(j);
            }
            // ���� ��� ������� ����������, �� ������ "� ���!" ���������� �����������
            buttonStart.Enabled = (sea_user.sozdano == Sea.vsegoKorablei);
        }

        // ��������� ������� ������������ ������� � ������� ������ (Enter)
        private void grid_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PlaceShip();
            grid_user.ClearSelection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // ���������� ������� �� ������ ��� ���������� ������������ �������� 
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

        // ���������� �������� �� ���� ����������
        private void grid_comp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           grid_comp.ClearSelection();
           if (mode != Mode.PlayUser) return;
           Status status = sea_comp.Vistrel(new Tochka(e.ColumnIndex, e.RowIndex));
            // ������ ��������� ������ ��� ������� �� ������ (�����)
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
            MessageBox.Show("����������. ������ ������� �������! ��������� ����������� �������������� :)");
            
        }
        private void WinComp()
        {
            MessageBox.Show("� ��������� ������� �������� ���������. ���������� �������� ��� :(");

        }
    }
}