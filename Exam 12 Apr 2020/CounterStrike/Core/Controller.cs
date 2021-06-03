using CounterStrike.Core.Contracts;
using CounterStrike.Models.Guns;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Maps;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Repositories;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounterStrike.Core
{
    public class Controller : IController
    {
        private GunRepository gunRepo;
        private PlayerRepository playrRepo;
        private IMap map;

        public Controller()
        {
            gunRepo = new GunRepository();
            playrRepo = new PlayerRepository();
            map = new Map();
        }

        public string AddGun(string type, string name, int bulletsCount)
        {
            IGun gun = CreateGun(type, name, bulletsCount);

            this.gunRepo.Add(gun);
            return string.Format(OutputMessages.SuccessfullyAddedGun, gun.Name);
        }



        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {
            IGun gun = this.gunRepo.FindByName(gunName);

            if (gun == null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }

            IPlayer player = CreatePlayer(type, username, health, armor, gun);
            this.playrRepo.Add(player);

            return string.Format(OutputMessages.SuccessfullyAddedPlayer, player.Username);
        }


        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var player in this.playrRepo.Models.OrderBy(p => p.GetType().Name)
                .ThenByDescending(p => p.Health).ThenBy(p => p.Username))
            {
                sb.AppendLine($"{player.GetType().Name}: {player.Username}");
                sb.AppendLine($"--Health: {player.Health}");
                sb.AppendLine($"--Armor: {player.Armor}");
                sb.AppendLine($"--Gun: {player.Gun.Name}");
            }

            return sb.ToString().TrimEnd();
        }

        public string StartGame()
        {
            string result = map.Start(this.playrRepo.Models.Where(p => p.IsAlive).ToList());
            return result;
        }

        private IGun CreateGun(string type, string name, int bulletsCount)
        {
            switch (type)
            {
                case "Pistol":
                    return new Pistol(name, bulletsCount);
                case "Rifle":
                    return new Rifle(name, bulletsCount);
                default:
                    throw new ArgumentException(ExceptionMessages.InvalidGunType);
            }

        }

        private IPlayer CreatePlayer(string type, string username, int health, int armor, IGun gun)
        {
            switch (type)
            {
                case "Terrorist":
                    return new Terrorist(username, health, armor, gun);
                case "CounterTerrorist":
                    return new CounterTerrorist(username, health, armor, gun);
                default:
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerType);
            }
        }


    }
}
