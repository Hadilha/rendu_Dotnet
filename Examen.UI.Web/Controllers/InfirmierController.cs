using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.UI.Web.Controllers
{
    public class InfirmierController : Controller
    {
        // GET: InfirmierController
        IInfirmierService si;
        ILaboratoireService sl;

        public InfirmierController(IInfirmierService si, ILaboratoireService sl)
        {
            this.si = si;
            this.sl = sl;
        }
        
        public ActionResult Index()
        {
            return View(si.GetMany());
        }

        // GET: InfirmierController/Details/5
        public ActionResult Details(int id)
        {
            return View(si.GetById(id));
        }

        // GET: InfirmierController/Create
        public ActionResult Create()
        {
            ViewBag.lsLabo = new SelectList(sl.GetMany(), "LaboratoireId", "Intitule");
            return View();
        }

        // POST: InfirmierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Infirmier collection)
        {
            try
            {
                si.Add(collection);
                si.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InfirmierController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.lsLabo = new SelectList(sl.GetMany(), "LaboratoireId", "Intitule");

            return View(si.GetById(id));
        }

        // POST: InfirmierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Infirmier collection)
        {
            try
            {
                si.Update(collection);
                si.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InfirmierController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(si.GetById(id));
        }

        // POST: InfirmierController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Infirmier collection)
        {
            try
            {
                si.Delete(si.GetById(id));
                si.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Patient(int id)
        {
            var infirmier = si.GetById(id);
            if (infirmier == null)
            {
                return NotFound();
            }

            var patients = (infirmier.Bilans ?? new List<Bilan>())
                .Select(b => b.Patient)
                .Where(p => p != null)
                .Distinct()
                .ToList();

            return View(patients);
        }
    }
}
