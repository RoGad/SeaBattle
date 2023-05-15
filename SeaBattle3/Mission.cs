namespace SeaBattle3
{
    class Mission
    {
        Sea sea;
        Random rand;
        int[,] shape =
            {
            { 1, 2, 1, 3, 1, 2, 1, 3, 1, 2 },
            { 2, 1, 3, 1, 2, 1, 3, 1, 2, 1 },
            { 1, 3, 1, 2, 1, 3, 1, 2, 1, 3 },
            { 3, 1, 2, 1, 3, 1, 2, 1, 3, 1 },
            { 1, 2, 1, 3, 1, 2, 1, 3, 1, 2 },
            { 2, 1, 3, 1, 2, 1, 3, 1, 2, 1 },
            { 1, 3, 1, 2, 1, 3, 1, 2, 1, 3 },
            { 3, 1, 2, 1, 3, 1, 2, 1, 3, 1 },
            { 1, 2, 1, 3, 1, 2, 1, 3, 1, 2 },
            { 2, 1, 3, 1, 2, 1, 3, 1, 2, 1 }

            };
        // в лсуче, если будет равно true будет сробатывать режим добивания
        bool modeDanger;

        int[] shipLength; // сколько кораблей какой длины осталось.
        int[,] map; //0 - неизвестно
                    //1 - мимо
                    //2 - ранил
                    //3 - убил
        int[,] put;

        public Mission(Sea sea)
        {

            this.sea = sea;
            rand = new Random();
            modeDanger = false;
            shipLength = new int[5];
            map = new int[Sea.razmerMorja.x, Sea.razmerMorja.y];
            put = new int[Sea.razmerMorja.x, Sea.razmerMorja.y];
            Reset();
        }

        private void Reset() {
            shipLength[1] = 4;
            shipLength[2] = 3;
            shipLength[3] = 2;
            shipLength[4] = 1;
            for (int x = 0; x < Sea.razmerMorja.x; x++)
                for (int y = 0; y < Sea.razmerMorja.y; y++)
                    map[x, y] = 0;
            modeDanger = false;
        }

        public Status Fight (out Tochka target)
        {
            // если равно true - добиваем
            if (modeDanger)
                target = fightDanger();
            // если равно false - стреляем по шаблону
            else
                target = fightShapes();
            Status status = sea.Vistrel(target);
            switch (status) {
                case Status.mimo: map[target.x, target.y] = 1; break;
                case Status.ranil: map[target.x, target.y] = 2; modeDanger = true; break;
                case Status.ubil:
                case Status.pobedil: map[target.x, target.y] = 2; int len = markKilledShip(target); shipLength[len]--;modeDanger = false; break;
                
            }
            return status;
        }
        // стрельба по шаблону
        private Tochka fightShapes() {
            InitPut();
            for (int x = 0; x < Sea.razmerMorja.x; x++)
                for (int y = 0; y < Sea.razmerMorja.y; y++)
                    if (map[x, y] == 0) {
                        put[x, y] = shape[x, y];
                    }     
            return RandomPut();
        }
        // реализация добития. Заполняется матрица элементов, куда можно стрелять
        // прмер: если ранено 2 клетки, то направление уже должно быть известно 
        private Tochka fightDanger() {
            InitPut();
            for (int x = 0; x < Sea.razmerMorja.x; x++)
                for (int y = 0; y < Sea.razmerMorja.y; y++)
                    // 2 - ранено (от этой клетки бот начинает искать направление расположение корабля)
                    if (map[x, y] == 2) {
                        bool longer = false;
                        Tochka ship = new Tochka(x, y);
                        
                        for (int length = shipLength.Length - 1; length >= 2; length--)
                            if (longer || shipLength[length] > 0) {
                                
                                CheckShipDirection(ship, -1, 0, length);
                                CheckShipDirection(ship, 1, 0, length);
                                CheckShipDirection(ship, 0, -1, length);
                                CheckShipDirection(ship, 0, 1, length);
                                longer = true;
                            }
                    }
            return RandomPut();
        }
        private void CheckShipDirection(Tochka ship, int sx, int sy, int length)
        // проверить все клекти в указанном направлении
        {
            //текущая клетка должна быть "ранен"
            if (Map(ship.x, ship.y) != 2) return;
            // в остальных направлениях не должно быть клекток "ранен"
            if (Map(ship.x - sx, ship.y - sy) == 2) return;
            if (sx == 0) {
                if (Map(ship.x - 1, ship.y) == 2) return;
                if (Map(ship.x + 1, ship.y) == 2) return;
            }
            if (sy == 0)
            {
                if (Map(ship.x, ship.y - 1) == 2) return;
                if (Map(ship.x, ship.y + 1) == 2) return;
            }
            // может быть клетка "ранен"
            // должна быть хотя бы одна клетка "неизвестно"
            int unknown = 0;
            int unknown_j = 0;
            for (int j = 1; j < length; j++) {
                int p = Map(ship.x + j * sx, ship.y + j * sy);
                // в выбранном направлении не должно быть клеток "мимо"
                if (p == 1) return;
                if (p == -1) return;
                // должна быть хотя бы одна клетка "неизвестно"
                if (p == 0) {
                    unknown++;
                    if (unknown == 1)
                        unknown_j = j;
                }
            }
            // если нашлась такая клетка, то в put записываем значение.
            if (unknown >= 1)
                put[ship.x + unknown_j * sx, ship.y + unknown_j * sy]++;
        }

        private int Map(int x, int y) {
            if (sea.NaMore(new Tochka(x, y)))
                return map[x, y];
            return -1;
        }

        private void InitPut()
        {
            for (int x = 0; x < Sea.razmerMorja.x; x++)
                for (int y = 0; y < Sea.razmerMorja.y; y++)
                    put[x, y] = 0;
        }
        
        //находим максимальное значение в массиве, а потом из этого максимального занчения
        //выбрать из списка всех этих максимальных значений какой-то одно случайное
        private Tochka RandomPut()
        {
            int max = -1;
            int qty = 0;
            for (int x = 0; x < Sea.razmerMorja.x; x++)
                for (int y = 0; y < Sea.razmerMorja.y; y++)
                    if (put[x, y] > max) {
                        max = put[x, y];
                        qty = 1;
                    } else if (put[x, y] == max) {
                        qty++;
                    }
            int nr = rand.Next(0, qty);
            for (int x = 0; x < Sea.razmerMorja.x; x++)
                for (int y = 0; y < Sea.razmerMorja.y; y++)
                    if (put[x, y] == max) {
                        if (nr-- == 0)
                            return new Tochka(x, y);
                    }
            return new Tochka(0, 0);
        }
        private int markKilledShip(Tochka place) {
            //кефтемеее это что... рекурсия ?
            if (!sea.NaMore(place))
                return 0;
            if (map[place.x, place.y] == 2)
            {
                map[place.x, place.y] = 3;
                int x, y;
                for (x = place.x - 1; x <= place.x + 1; x++)
                    for (y = place.y - 1; y <= place.y + 1; y++)
                        if (Map(x, y) == 0)
                            map[x, y] = 1;
                int length = 1;
                // идем по кораблю
                length += markKilledShip(new Tochka(place.x - 1, place.y));
                length += markKilledShip(new Tochka(place.x + 1, place.y));
                length += markKilledShip(new Tochka(place.x, place.y - 1));
                length += markKilledShip(new Tochka(place.x, place.y + 1));
                return length;

            }
            return 0;
        }
    }
}
