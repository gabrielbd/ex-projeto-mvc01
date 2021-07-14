using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC02.Controllers
{
    //classe de controle para a rota /Home/
    public class HomeController : Controller
    {
        //método para abrir a página /Home/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
