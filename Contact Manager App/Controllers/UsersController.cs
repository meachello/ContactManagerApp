using Contact_Manager_App.Models;
using Contact_Manager_App.Services;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Contact_Manager_App.Controllers;

public class UsersController : Controller
{
    private readonly UserService _userService;

    public UsersController(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        _userService = new UserService(connectionString);
    }

    public async Task<IActionResult> CSVList()
    {
        var users = await _userService.GetAllUsersAsync();
        return View("CSVList", users);
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            try
            {
                // Преобразуем CSV-файл в список объектов User
                var users = csv.GetRecords<User>().ToList();

                foreach (var user in users)
                {
                    await _userService.AddUserAsync(user);
                }

                return RedirectToAction("CSVList");
            }
            catch (Exception ex)
            {
                // Обработка ошибок в случае некорректного файла
                ViewBag.ErrorMessage = $"Ошибка при обработке файла: {ex.Message}";
            }
        }

        ViewBag.ErrorMessage = "Файл не выбран или пуст.";
        return View("Error");
    }
}