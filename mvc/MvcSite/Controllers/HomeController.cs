﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LaunchDarkly.Client;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Options;
using MvcSite.Models;

namespace MvcSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILdClient _ldClient;
        private readonly IOptions<SampleAppOptions> _sampleAppOptions;

        public HomeController(ILdClient ldClient, IOptions<SampleAppOptions> sampleAppOptions)
        {
            _ldClient = ldClient;
            _sampleAppOptions = sampleAppOptions;
        }
        
        public IActionResult Index()
        {
            Request.Cookies.TryGetValue("sampleApp.userId", out string userId);
            if (userId == null)
            {
                userId = Guid.NewGuid().ToString();
                this.Response.Cookies.Append(
                    "sampleApp.userId",
                    userId,
                    new Microsoft.AspNetCore.Http.CookieOptions()
                    {
                        Path = "/"
                    }
                );
            }
            var user = LaunchDarkly.Client.User.WithKey(userId);
            if (_ldClient.BoolVariation("new-portal", user, false))
            {
                return Redirect(_sampleAppOptions.Value.SpaUrl);
            }
            return View();
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
}
