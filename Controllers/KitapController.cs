using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Controllers
{
    public class KitapController : Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Admin,Ogrenci")]
        public IActionResult Index()
        {
            List<Kitap> kitapList = _kitapRepository.GetAll(includeProps:"KitapTuru").ToList();
            return View(kitapList);
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> kitapTuruList = _kitapTuruRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Ad,
                    Value = k.Id.ToString()
                });
            ViewBag.kitapTuruList = kitapTuruList;
            //Ekleme
            if(id == null || id == 0)
            {
                return View();
            }
            //Güncelleme
            else
            {
                Kitap? kitap = _kitapRepository.Get(u => u.Id == id);
                if(kitap == null)
                {
                    return NotFound();
                }
                return View(kitap);
            }
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)] 
        public IActionResult EkleGuncelle(Kitap kitap, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");

                if(file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                }
    
                if (kitap.Id == 0)
                {
                    _kitapRepository.Ekle(kitap);
                    TempData["basarili"] = "Kitap ekleme işlemi başarılı.";
                }
                else
                {
                    _kitapRepository.Guncelle(kitap);
                    TempData["basarili"] = "Kitap güncelleme işlemi başarılı.";
                }
                _kitapRepository.Kaydet();
                return RedirectToAction("Index", "Kitap");
            }
            return View();
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound() ;
            }
            Kitap? kitap = _kitapRepository.Get(u=> u.Id == id);
            if(kitap == null)
            {
                return NotFound();
            }
            return View(kitap);
        }
        [HttpPost, ActionName("Sil")]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult SilPost(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitap = _kitapRepository.Get(u => u.Id == id);
            if(kitap == null)
            {
                return NotFound();
            }
            _kitapRepository.Sil(kitap);
            _kitapRepository.Kaydet();
            TempData["basarili"] = "Kitap silme işlemi başarılı.";
            return RedirectToAction("Index", "Kitap");
        }
    }
}
