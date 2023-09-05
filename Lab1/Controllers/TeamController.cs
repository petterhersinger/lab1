using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class TeamController : Controller
    {
        public static List<Team> teams = new List<Team>();
        public IActionResult ShowTeams()
        {
            return View(teams);
        }
        public IActionResult CreateTeam()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTeam(Team team)
        {
            if (ModelState.IsValid)
            {
                teams.Add(team);
                return RedirectToAction("ShowTeams");
            }
            return View(team);
        }
        [HttpPost]
        public IActionResult RemoveTeam(int teamId)
        {
            var teamToRemove = teams.FirstOrDefault(t => t.Id == teamId);
            if (teamToRemove != null)
            {
                teams.Remove(teamToRemove);
            }
            return RedirectToAction("ShowTeams");
        }
    }
}

