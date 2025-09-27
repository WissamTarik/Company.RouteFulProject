using System.Diagnostics;
using System.Text;
using Company.RouteFulProject.PL.Services;
using Company.RouteFulProjectPL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.RouteFulProjectPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scopedService01;
        private readonly IScopedService scopedService02;
        private readonly ITransientService transientService01;
        private readonly ITransientService transientService02;
        private readonly ISingletonService singletonService01;
        private readonly ISingletonService singletonService02;

        public HomeController(ILogger<HomeController> logger
            ,IScopedService scopedService01
            ,IScopedService scopedService02
            ,ITransientService transientService01
            ,ITransientService transientService02
            ,ISingletonService singletonService01
            ,ISingletonService singletonService02
            
            )
        {
            _logger = logger;
            this.scopedService01 = scopedService01;
            this.scopedService02 = scopedService02;
            this.transientService01 = transientService01;
            this.transientService02 = transientService02;
            this.singletonService01 = singletonService01;
            this.singletonService02 = singletonService02;
        }

        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Scoped Service01 :{scopedService01.GetGuid()}");
            builder.AppendLine($"Scoped Service02 :{scopedService02.GetGuid()}");
            builder.AppendLine();
            builder.AppendLine($"transient Service01 :{transientService01.GetGuid()}");
            builder.AppendLine($"transient Service02 :{transientService02.GetGuid()}");
            builder.AppendLine();
            builder.AppendLine($"Singleton Service01 :{singletonService01.GetGuid()}");
            builder.AppendLine($"Singleton Service02 :{singletonService02.GetGuid()}");
            return builder.ToString();
        }
        public IActionResult Index()
        {
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
