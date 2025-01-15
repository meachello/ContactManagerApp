using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Contact_Manager_App.Models;
using Contact_Manager_App.Services;

namespace Contact_Manager_App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<ViewResult> CSVList()
    {
        var connectionString = "server=127.0.0.1;port=3306;username=root;password=syst3m007;database=csv_database";
        var _userService = new UserService(connectionString);
        var users = await _userService.GetAllUsersAsync();
        return View("CSVList", users);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}