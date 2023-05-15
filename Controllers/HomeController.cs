﻿using System.Diagnostics;
using BadHunter.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using BadHunter.Models;

namespace BadHunter.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICurrentUser _currentUser;

    public HomeController(ILogger<HomeController> logger,
        ICurrentUser currentUser )
    {
        _logger = logger;
        _currentUser = currentUser;
    }

    public IActionResult Index()
    {
        return View(_currentUser.IsLoggedIn());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}