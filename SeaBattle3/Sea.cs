namespace SeaBattle3
{
    public delegate void deShowShip(Tochka place, int nr);
    public delegate void deShowFight(Tochka place, Status status);

    internal class Sea
    {
        public static Tochka razmerMorja = new Tochka(10, 10);
        public static int vsegoKorablei = 10;

        public deShowShip ShowShip; // означает, что у экземпляра класса моря будет доступ к функции, которая показывает корабли
        public deShowFight ShowFight; // означает, что у этого экземпляра море будет доступ к функции, которая показывает выстрелы

        protected int[,] kartaKorablei; // -1 - пусто, 0...9 номера кораблей, которые стоят 
        protected Status[,] kartaPopadani; // неизвестно, ... по мере игры изменяются значения

        protected Ship[] ship;

        public int sozdano { get; protected set; }

        public int ubito { get; protected set; }

        public Sea() {
            kartaKorablei = new int[razmerMorja.x, razmerMorja.y];
            kartaPopadani = new Status[razmerMorja.x, razmerMorja.y];
            ship = new Ship[vsegoKorablei];
        }

        public Status KartaPopadani(Tochka t)
        {
            if (NaMore(t))
            {
                return kartaPopadani[t.x, t.y];
            }
            return Status.neisvestno;
        }
        public bool NaMore(Tochka t) { 
            return (t.x >= 0 && t.x < razmerMorja.x && t.y >= 0 && t.y < razmerMorja.y);
        }
        public Status Vistrel(Tochka t) {
            if (!NaMore(t)) {
                return Status.neisvestno;
            }
            if (kartaPopadani[t.x, t.y] != Status.neisvestno) {
                return kartaPopadani[t.x, t.y];
            }
            Status status;
            if (kartaKorablei[t.x, t.y] == -1)
            {
                kartaPopadani[t.x, t.y] = Status.mimo;
                status = Status.mimo;
            }
            else {
                status = ship[kartaKorablei[t.x, t.y]].Vistrel(t);
            }
            kartaPopadani[t.x, t.y] = status;
            if (status == Status.ubil) {
                ubito++;
                if (ubito >= sozdano) {
                    status = Status.pobedil;
                }
            }
            ShowFight(t, status);
            return status;
        }
    }
}
