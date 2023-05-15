namespace SeaBattle3
{
    class Grid
    {
        DataGridView grid;

        static string abc = "АБВГДЕЖЗИКЛМНОПРСТУФХЧШШЫЭЮЯ";

        Color color_back = Color.DarkSeaGreen;
        Color[] color_ship = {
            Color.Purple, // цвет для 4-х палубного корабля
            Color.Purple, Color.Purple, // цвет для 3-х палубного корабля
            Color.Purple, Color.Purple, Color.Purple, // цвет для 2-х палубного корабля
            Color.Purple, Color.Purple, Color.Purple, Color.Purple // цвет для 1-х палубного корабля
        };
        Color[] color_fight = {
            Color.DarkSeaGreen, // цвет для 4-х палубного корабля
            Color.SeaGreen, // цвет для 3-х палубного корабля
            Color.Orange, // цвет для 2-х палубного корабля
            Color.OrangeRed,
            Color.OrangeRed// цвет для 1-х палубного корабля
        };

        public Grid (DataGridView grid)
        {
            this.grid = grid;
            InitGrid();
        }

        // Создание сетки для поля игрока и компа
        private void InitGrid()
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            grid.DefaultCellStyle.BackColor = color_back;
            for (int x = 0; x < Sea.razmerMorja.x; x++)
            {
                grid.Columns.Add("col_" + x.ToString(), abc.Substring(x, 1));
            }
            for (int y = 0; y < Sea.razmerMorja.y; y++)
            {
                grid.Rows.Add();
                grid.Rows[y].HeaderCell.Value = (y + 1).ToString();
            }
            grid.Height = Sea.razmerMorja.y * grid.Rows[0].Height + grid.ColumnHeadersHeight + 2; // убрать лишние поле снизу 
            grid.ClearSelection();
        }
        // отображение кораблей на указанной сетке 
        public void ShowShip(Tochka place, int nr)
        {
            if (nr < 0)
            {
                grid[place.x, place.y].Style.BackColor = color_back;
            }
            else
            {
                grid[place.x, place.y].Style.BackColor = color_ship[nr];
            }

        }
        public void ShowFight(Tochka place, Status status)
        {
            grid[place.x, place.y].Style.BackColor = color_fight[(int)status];
        }

        // для расположение кораблей случайным образом
        public Tochka[] GetSelectedCells() {
            if (grid.SelectedCells.Count == 0)
                return null;
            Tochka[] ship = new Tochka[grid.SelectedCells.Count];
            int j = 0;
            foreach (DataGridViewCell cell in grid.SelectedCells)
                ship[j++] = new Tochka(cell.ColumnIndex, cell.RowIndex);
            grid.ClearSelection();
            return ship;
        }
    }
}
