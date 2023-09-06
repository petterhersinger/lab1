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

        private static List<Team> GetTeams()
        {
            var teams = new List<Team>
        {
            new Team { TeamId = 1, TeamName = "BC Luleå" },
            new Team { TeamId = 2, TeamName = "Sundsvall Dragons" },
        };

            return teams;
        }

        private Team GetTeamById(int teamId)
        {
            return teams.FirstOrDefault(team => team.TeamId == teamId);
        }

        public IActionResult ShowPlayers()
        {
            var playerTeamViewModelList = new List<PlayerTeamViewModel>();

            foreach (var player in players)
            {
                var team = GetTeamById(player.TeamId);
                var playerTeamViewModel = new PlayerTeamViewModel
                {
                    Player = player,
                    Team = team
                };
                playerTeamViewModelList.Add(playerTeamViewModel);
            }

            return View(playerTeamViewModelList);
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
            if (ModelState.IsValid)
            {
                var player = playerTeamViewModel.Player;
                player.Id = playerCounter++;
                players.Add(player);
                return RedirectToAction("ShowPlayers");
            }

            var teams = GetTeams();
            ViewBag.Teams = new SelectList(teams, "TeamId", "TeamName");
            return View(playerTeamViewModel);
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
