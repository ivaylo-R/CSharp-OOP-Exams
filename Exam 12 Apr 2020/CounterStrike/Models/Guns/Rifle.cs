namespace CounterStrike.Models.Guns
{
    public class Rifle : Gun
    {
        private const int BulletsFire = 10;
        public Rifle(string name, int bulletsCount)
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
