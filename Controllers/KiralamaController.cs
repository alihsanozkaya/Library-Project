using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KiralamaController : Controller
    {
        private readonly IKiralamaRepository _kiralamaRepository;
        private readonly IKitapRepository _kitapRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kiralamaRepository = kiralamaRepository;
            _kitapRepository = kitapRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Kiralama> kiralamaList = _kiralamaRepository.GetAll(includeProps:"Kitap").ToList();
            return View(kiralamaList);
        }
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> kitapList = _kitapRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.KitapAdi,
                    Value = k.Id.ToString()
                });
            ViewBag.kitapList = kitapList;
            //Ekleme
            if(id == null || id == 0)
            {
                return View();
            }
            //Güncelleme
            else
            {
                Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);
                if(kiralama == null)
                {
                    return NotFound();
                }
                return View(kiralama);
            }
        }
        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama kiralama)
        {
            if(ModelState.IsValid)
            {    
                if (kiralama.Id == 0)
                {
                    _kiralamaRepository.Ekle(kiralama);
                    TempData["basarili"] = "Kiralama işlemi başarılı.";
                }
                else
                {
                    _kiralamaRepository.Guncelle(kiralama);
                    TempData["basarili"] = "Kiralama güncelleme işlemi başarılı.";
                }
                _kiralamaRepository.Kaydet();
                return RedirectToAction("Index", "Kiralama");
            }
            return View();
        }
        public IActionResult Sil(int? id)
        {
            IEnumerable<SelectListItem> kitapList = _kitapRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.KitapAdi,
                    Value = k.Id.ToString()
                });
            ViewBag.kitapList = kitapList;

            if (id == null || id == 0)
            {
                return NotFound() ;
            }
            Kiralama? kiralama = _kiralamaRepository.Get(u=> u.Id == id);
            if(kiralama == null)
            {
                return NotFound();
            }
            return View(kiralama);
        }
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPost(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Kiralama kiralama = _kiralamaRepository.Get(u => u.Id == id);
            if(kiralama == null)
            {
                return NotFound();
            }
            _kiralamaRepository.Sil(kiralama);
            _kiralamaRepository.Kaydet();
            TempData["basarili"] = "Kiralama silme işlemi başarılı.";
            return RedirectToAction("Index", "Kiralama");
        }
    }
}
