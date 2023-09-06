using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab1.Controllers
{
    public class PlayerController : Controller
    {
        public static int playerCounter = 1;
        public static List<Player> players = new List<Player>();
        public static List<Team> teams = GetTeams();
        public static List<PlayerTeamViewModel> playerTeams = new List<PlayerTeamViewModel>();

        private static List<Team> GetTeams()
        {
            var teams = new List<Team>
        {
            new Team { TeamId = 1, TeamName = "BC Luleå" },
            new Team { TeamId = 2, TeamName = "Sundsvall Dragons" },
        };

            return teams;
        }

        public IActionResult ShowPlayers()
        {

            return View(playerTeams);
        }

        public IActionResult Create()
        {
            var teams = GetTeams();

            ViewData["Teams"] = new SelectList(teams, "TeamId", "TeamName");

            var playerTeamViewModel = new PlayerTeamViewModel();
            return View(playerTeamViewModel);
        }

        [HttpPost]
        public IActionResult Create(PlayerTeamViewModel playerTeamViewModel)
        {

            var teams = GetTeams();
            ViewData["Teams"] = new SelectList(teams, "TeamId", "TeamName");
            var player = playerTeamViewModel.Player;
            player.Id = playerCounter++;


            if (!ModelState.IsValid)
            {
                return View(playerTeamViewModel);
            }

            playerTeams.Add(playerTeamViewModel);

            return RedirectToAction("ShowPlayers");

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
