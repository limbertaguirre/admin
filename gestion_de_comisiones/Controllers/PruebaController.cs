using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers
{
    public class PruebaController : Controller
    {
        // GET: PruebaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PruebaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PruebaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PruebaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PruebaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PruebaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PruebaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PruebaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
