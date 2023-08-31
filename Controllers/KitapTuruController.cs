using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KitapTuruController : Controller
    {
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public KitapTuruController(IKitapTuruRepository kitapTuruRepository)
        {
            _kitapTuruRepository = kitapTuruRepository;
        }
        public IActionResult Index()
        {
            List<KitapTuru> kitapTurleriList = _kitapTuruRepository.GetAll().ToList();
            return View(kitapTurleriList);
        }
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru)
        {
            if(ModelState.IsValid)
            {
                _kitapTuruRepository.Ekle(kitapTuru);
                _kitapTuruRepository.Kaydet();
                TempData["basarili"] = "Kitap türü ekleme işlemi başarılı.";
                return RedirectToAction("Index", "KitapTuru");
            }
            return View();
        }
        public IActionResult Guncelle(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            KitapTuru? kitapTuru = _kitapTuruRepository.Get(u=> u.Id == id);
            if(kitapTuru == null)
            {
                return NotFound();
            }
            return View(kitapTuru);
        }
        [HttpPost]
        public IActionResult Guncelle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Guncelle(kitapTuru);
                _kitapTuruRepository.Kaydet();
                TempData["basarili"] = "Kitap türü güncelleme işlemi başarılı.";
                return RedirectToAction("Index", "KitapTuru");
            }
            return View();
        }
        //GETACTION
        public IActionResult Sil(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            KitapTuru? kitapTuru = _kitapTuruRepository.Get(u=> u.Id == id);
            if(kitapTuru == null)
            {
                return NotFound();
            }
            return View(kitapTuru);
        }
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPost(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            KitapTuru? kitapTuru = _kitapTuruRepository.Get(u => u.Id == id);
            if(kitapTuru == null)
            {
                return NotFound();
            }
            _kitapTuruRepository.Sil(kitapTuru);
            _kitapTuruRepository.Kaydet();
            TempData["basarili"] = "Kitap türü silme işlemi başarılı.";
            return RedirectToAction("Index", "KitapTuru");
        }
    }
}