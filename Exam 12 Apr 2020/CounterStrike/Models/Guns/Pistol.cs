namespace CounterStrike.Models.Guns
{
    public class Pistol : Gun
    {
        private const int BulletsFire = 1;
        public Pistol(string name, int bulletsCount)
            : base(name, bulletsCount)
        {
        }

        public override int Fire()
        {
            if (this.BulletsCount < BulletsFire)
            {
                return 0;
            }

            this.BulletsCount -= BulletsFire;
            return BulletsFire;
        }
    }
}
