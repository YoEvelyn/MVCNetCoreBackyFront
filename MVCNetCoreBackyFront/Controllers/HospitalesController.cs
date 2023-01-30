using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MVCNetCoreBackyFront.Models;
using MVCNetCoreBackyFront.Services;

namespace MVCNetCoreBackyFront.Controllers
{
    public class HospitalesController : Controller
    {
        private ServiceApiHospital service;
        public HospitalesController(ServiceApiHospital service)
        {
            this.service = service; 
        }

        public async Task<IActionResult> HospitalesBack()
        {
            List<Hospital> hospitales = await this.service.GetHospitallesAync();
            return View(hospitales);
        }

        //EN INDEX DIBUJAREMOS MENU PAR IR BACK O AL FRONT
        public IActionResult Index()
        {
            return View();
        }


        //VISTA PARA EL FRONT
        public IActionResult HospitalesFront()
        {
            return View();
        }
    }
}
