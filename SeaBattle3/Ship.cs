namespace SeaBattle3
{
    class Ship
    {
        int popadanii;

        public Tochka[] paluba { get; private set; }
        public Ship(Tochka[] paluba)
        {
            this.paluba = paluba;
            popadanii = 0;
        }
        public Status Vistrel(Tochka t)
        {
            for (int j = 0; j < paluba.Length; j++)
            {
                if (paluba[j].x == t.x && paluba[j].y == t.y)
                {
                    popadanii++;
                    if (popadanii == paluba.Length)
                        return Status.ubil;
                    else
                        return Status.ranil;
                }
            }
            return Status.mimo;
        }
    }
}
