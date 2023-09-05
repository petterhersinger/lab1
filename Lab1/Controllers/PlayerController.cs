using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class PlayerController : Controller
    {
        public static int playerCounter = 1;
        public static List<Player> players = new List<Player>();

        public IActionResult ShowPlayers()
        {

            return View(players);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                player.Id = playerCounter++;
                players.Add(player);
                return RedirectToAction("ShowPlayers");
            }
            return View(player);
        }
        [HttpPost]
        public IActionResult RemovePlayer(int playerId)
        {
            var playerToRemove = players.FirstOrDefault(player => player.Id == playerId);

            if (playerToRemove != null)
            {
                players.Remove(playerToRemove);
            }

            return RedirectToAction("ShowPlayers");
        }

    }

}
