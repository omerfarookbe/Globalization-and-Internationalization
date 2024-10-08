﻿using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly IStringLocalizer<HomeController> localizer;

    public HomeController(IStringLocalizer<HomeController> localizer)
    {
        this.localizer = localizer;
    }

    public IActionResult Index()
    {
        var strings = localizer.GetAllStrings();

        var repository = new ProductRepository();
        var products = repository.Get();

        return View(products.OrderBy(product => product.Name, StringComparer.OrdinalIgnoreCase));
    }

    [HttpPost]
    public IActionResult Add(ProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage);
            return BadRequest(errors);
        }
        return Ok();
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
