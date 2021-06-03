using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CounterStrike.Models.Maps
{
    public class Map : IMap
    {

        public string Start(ICollection<IPlayer> players)
        {
            ICollection<IPlayer> terrorists = new List<IPlayer>();
            ICollection<IPlayer> counterTerrorists = new List<IPlayer>();

            SeparatePlayersByTerrAndCounterTerr(terrorists, counterTerrorists, players);

            while (true)
            {

                foreach (var t in terrorists)
                {
                    foreach (var c in counterTerrorists)
                    {
                        c.TakeDamage(t.Gun.Fire());

                        if (counterTerrorists.All(h => h.Health == 0))
                        {
                            return "Terrorist wins!";
                        }

                        t.TakeDamage(c.Gun.Fire());

                        if (terrorists.All(h => h.Health == 0))
                        {
                            return "Counter Terrorist wins!";
                        }

                    }

                }

            }

        }

        private void SeparatePlayersByTerrAndCounterTerr
            (ICollection<IPlayer> terrorists, ICollection<IPlayer> counterTerrorists
            , ICollection<IPlayer> players)
        {

            foreach (var player in players)
            {
                if (player.GetType().Name == "CounterTerrorist")
                {
                    counterTerrorists.Add(player);
                }

                else if (player.GetType().Name == "Terrorist")
                {
                    terrorists.Add(player);
                }

            }

        }
    }
}
