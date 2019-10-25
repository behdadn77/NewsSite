using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace newsSite.Controllers
{
    [Authorize]
    public class ChatRoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}