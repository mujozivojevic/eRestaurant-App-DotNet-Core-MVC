using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EF;
using WebApplication1.Models;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication1.Helper
{

    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool zaposlenik, bool korisnik)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { zaposlenik, korisnik };
        }
    }

    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool zaposlenik, bool korisnik)
        {
            _zaposlenik = zaposlenik;
            _korisnik = korisnik;
        }
      
        private readonly bool _zaposlenik;
        private readonly bool _korisnik;

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            KorisnickiNalog k = filterContext.HttpContext.GetLogiraniKorisnik();

            if(k== null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }
                filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { @area = "" });
                return;
            }

            MyContext db = filterContext.HttpContext.RequestServices.GetService<MyContext>();

            if(_zaposlenik && db.Zaposlenik.Any(s=> s.KorisnickiNalogId == k.Id && (s.RadnoMjestoId == 1 ||  s.RadnoMjestoId == 2)))
            {
                await next();
                return;
            }

            if (_korisnik && db.Narucilac.Any(s => s.KorisnickiNalogId == k.Id))
            {
                await next();
                return;
            }
            if (filterContext.Controller is Controller c1)
            {
                c1.ViewData["error_poruka"] = "Nemate pravo pristupa";
            }
        }

        
    }

       
    
}
