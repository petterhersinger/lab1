using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Lab1.Controllers
{
    public class PlayerController : Controller
    {

        //sessionsvariabel
        private const string PlayerTeamSessionKey = "PlayerTeam";

        public static int playerCounter = 1;
        public static List<Player> players = new List<Player>();
        public static List<Team> teams = GetTeams();
        //public static List<PlayerTeamViewModel> playerTeams = new List<PlayerTeamViewModel>();

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
            var serializedPlayerTeams = HttpContext.Session.GetString(PlayerTeamSessionKey);
            List<PlayerTeamViewModel> playerTeams;
            if (!string.IsNullOrEmpty(serializedPlayerTeams))
            {
                playerTeams = JsonConvert.DeserializeObject<List<PlayerTeamViewModel>>(serializedPlayerTeams);
            }
            else
            {
                playerTeams = new List<PlayerTeamViewModel>();
            }
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
            //SESSIONS
            var serializedPlayerTeams = HttpContext.Session.GetString(PlayerTeamSessionKey);
            List<PlayerTeamViewModel> playerTeams;
            if (!string.IsNullOrEmpty(serializedPlayerTeams))
            {
                playerTeams = JsonConvert.DeserializeObject<List<PlayerTeamViewModel>>(serializedPlayerTeams);
            }
            else
            {
                playerTeams = new List<PlayerTeamViewModel>();
            }
            //SESSIONS

            var teams = GetTeams();
            ViewData["Teams"] = new SelectList(teams, "TeamId", "TeamName");
            var player = playerTeamViewModel.Player;
            player.Id = playerCounter++;


            if (!ModelState.IsValid)
            {
                return View(playerTeamViewModel);
            }

            playerTeams.Add(playerTeamViewModel);

            //
            HttpContext.Session.SetString(PlayerTeamSessionKey, JsonConvert.SerializeObject(playerTeams));
            //

            return RedirectToAction("ShowPlayers");

        }

        [HttpPost]
        public IActionResult RemovePlayer(int playerId)
        {
            var serializedPlayerTeams = HttpContext.Session.GetString(PlayerTeamSessionKey);
            List<PlayerTeamViewModel> playerTeams;
            if (!string.IsNullOrEmpty(serializedPlayerTeams))
            {
                playerTeams = JsonConvert.DeserializeObject<List<PlayerTeamViewModel>>(serializedPlayerTeams);
            }
            else
            {
                playerTeams = new List<PlayerTeamViewModel>();
            }

            var playerTeamToRemove = playerTeams.FirstOrDefault(pt => pt.Player.Id == playerId);

            if (playerTeamToRemove != null)
            {
                playerTeams.Remove(playerTeamToRemove);
            }
            //
            HttpContext.Session.SetString(PlayerTeamSessionKey, JsonConvert.SerializeObject(playerTeams));
            //

            return RedirectToAction("ShowPlayers");
        }

    }

}
