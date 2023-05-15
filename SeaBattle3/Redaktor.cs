namespace SeaBattle3
{
    class Redaktor : Sea
    {
        static int[] dlinaKorablei = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
        static Random rand = new Random();

        public Redaktor() : base() // при вызове редактора сначало будет вызываться базовый конструктор (public Sea())
        {

        }
        public bool PostavitRovno()
        {
            Sbros();
            PostavitKorabl(0,
                new Tochka[] {
                    new Tochka (1, 1),
                    new Tochka (2, 1),
                    new Tochka (3, 1),
                    new Tochka (4, 1)
                });
            PostavitKorabl(1,
               new Tochka[] {
                    new Tochka (1, 3),
                    new Tochka (2, 3),
                    new Tochka (3, 3),
               });
            PostavitKorabl(2,
                new Tochka[] {
                    new Tochka (5, 3),
                    new Tochka (6, 3),
                    new Tochka (7, 3),
                });
            PostavitKorabl(3,
                new Tochka[] {
                    new Tochka (1, 5),
                    new Tochka (2, 5),
                });
            PostavitKorabl(4,
                new Tochka[] {
                    new Tochka (4, 5),
                    new Tochka (5, 5),
                });
            PostavitKorabl(5,
                new Tochka[] {
                    new Tochka (7, 5),
                    new Tochka (8, 5),
                });
            for (int nomer = 6; nomer < vsegoKorablei; nomer++)
            {
                PostavitKorabl(nomer,
                new Tochka[] {
                    new Tochka ((nomer - 5) * 2 - 1, 7)
                });
            }
            return true;
        }

        public void Sbros()
        {
            for (int x = 0; x < razmerMorja.x; x++)
                for (int y = 0; y < razmerMorja.y; y++)
                {
                    kartaKorablei[x, y] = -1;
                    ShowShip(new Tochka(x, y), -1); // исправление бага постоянной генерации кораблей
                    kartaPopadani[x, y] = Status.neisvestno;
                    ShowFight(new Tochka(x, y), Status.neisvestno);
                }
            for (int k = 0; k < vsegoKorablei; k++)
            {
                ship[k] = null;
            }
            sozdano = 0;
            ubito = 0;
        }

        public bool PostavitPoTochkam(Tochka[] paluba) {
            int dlina = paluba.Length;
            int nomer = NaitiNomer(dlina);
            if (nomer < 0)
                return false;
            Tochka lv = paluba[0]; // lv - левый верхний угол
            Tochka pn = paluba[0]; // pn - правый нижний угол 
            for (int j = 1; j < dlina; j++) {
                lv.x = Math.Min(lv.x, paluba[j].x);
                lv.y = Math.Min(lv.y, paluba[j].y);
                pn.x = Math.Max(pn.x, paluba[j].x);
                pn.y = Math.Max(pn.y, paluba[j].y);
            }
            if (lv.x == pn.x) // корабль расположен вертекально
            {
                if (pn.y - lv.y + 1 != dlina)
                    return false;
            }
            else if (lv.y == pn.y)  // корабль расположен горизонтально
            {
                if (pn.x - lv.x + 1 != dlina)
                    return false;
            }
            else
            {
                return false;
            }
            for (int j = 0; j < dlina; j++) {
                OchistitOblast(paluba[j]);
            }
            PostavitKorabl(nomer, paluba);
            return true;
        }

        protected int NaitiNomer(int dlina) // находит место для нового корабля указанной длины 
        {
            for (int j = 0; j < dlinaKorablei.Length; j++) {
                if (dlina == dlinaKorablei[j]) {
                    if (NetKorablya(j))
                        return j;
                }
            }
            return -1;
        }

        public void PostavitKorabl(int nomer, Tochka[] paluba)
        {
            if (ship[nomer] != null)
            {
                UbratKorabl(nomer);
            }
            ship[nomer] = new Ship(paluba);
            foreach (Tochka t in paluba)
            {
                kartaKorablei[t.x, t.y] = nomer;
                ShowShip(t, nomer);
            }
            sozdano++;
        }
        public void UbratKorabl(int nomer)
        {
            foreach (Tochka t in ship[nomer].paluba)
            {
                kartaKorablei[t.x, t.y] = -1;
                ShowShip(t, -1);
            }
            ship[nomer] = null;
            sozdano--;
        }
        public bool NetKorablya(int nomer)
        {
            return ship[nomer] == null;
        }
        public int KartaKorablei(Tochka t)
        {
            if (NaMore(t))
            {
                return kartaKorablei[t.x, t.y];
            }
            return -1;
        }

        protected void OchistitOblast(Tochka t) {
            Tochka p;
            for (p.x = t.x - 1; p.x <= t.x + 1; p.x++) {
                for (p.y = t.y - 1; p.y <= t.y + 1; p.y++) {
                    OchistitTochku(p);
                }
            }
        }
        public void OchistitTochku(Tochka t) {
            if (!NaMore(t)) {
                return;
            }
            if (kartaKorablei[t.x, t.y] == -1){
                return;
            }
            UbratKorabl(kartaKorablei[t.x, t.y]);
        } 
        public bool PostavitRandomno(int nomer)
        {
            int dlina = dlinaKorablei[nomer];
            Tochka nos;
            Tochka shag;
            if (rand.Next(2) == 0) // ставим горизонатльно
            { 
                nos = new Tochka(rand.Next(0, razmerMorja.x - dlina + 1), rand.Next(0, razmerMorja.y));
                shag = new Tochka(1, 0);
            }
            else // ставим вертикально
            {
                nos = new Tochka(rand.Next(0, razmerMorja.x), rand.Next(0, razmerMorja.y - dlina + 1));
                shag = new Tochka(0, 1);
            }
            Tochka[] paluba = new Tochka[dlina];
            for (int j = 0; j < dlina; j++) 
            {
                paluba[j] = new Tochka(nos.x + j * shag.x, nos.y + j * shag.y);
                OchistitOblast(paluba[j]);
            }
            
            PostavitKorabl(nomer, paluba);
            return true;
        }
        public void PostavitRandomno() {
            Sbros();
            int loop = 100;
            while (--loop > 0 && sozdano < Sea.vsegoKorablei)
            {
                for (int nomer = 0; nomer < Sea.vsegoKorablei; nomer++)
                {
                    if (NetKorablya(nomer))
                    {
                        PostavitRandomno(nomer);
                    }
                }
            }
            if (sozdano < Sea.vsegoKorablei)
            {
                Sbros();
            }
        }
    }
}
