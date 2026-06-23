using MVC_Paciente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_Paciente.Controllers
{
    public class PacienteController : Controller
    {
        private LP2Entities db = new LP2Entities();

        // GET: Paciente
        public ActionResult Index()
        {
            return View(db.Paciente.ToList());
        }

        // GET: Paciente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET: Paciente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paciente/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Paciente,Nome,Idade,DataNasc,Genero,Doencas,Alergias,Telefone,Contato,Logradouro,Cidade,Estado")] Paciente paciente)
        {
            string dataNascRaw = Request.Form["DataNasc"];

            if (!string.IsNullOrEmpty(dataNascRaw))
            {
                try
                {
                    paciente.DataNasc = DateTime.ParseExact(dataNascRaw, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    ModelState.Remove("DataNasc");
                }
                catch (FormatException)
                {
                    ModelState.AddModelError("DataNasc", "Insira uma data válida no formato DD/MM/AAAA.");
                }
            }
            if (ModelState.IsValid)
            {
                db.Paciente.Add(paciente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paciente);
        }

        // GET: Paciente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Paciente/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Paciente,Nome,Idade,DataNasc,Genero,Doencas,Alergias,Telefone,Contato,Logradouro,Cidade,Estado")] Paciente paciente)
        {
            string dataNascRaw = Request.Form["DataNasc"];

            if (!string.IsNullOrEmpty(dataNascRaw))
            {
                try
                {
                    paciente.DataNasc = DateTime.ParseExact(dataNascRaw, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ModelState.Remove("DataNasc");
                }
                catch (FormatException)
                {
                    ModelState.AddModelError("DataNasc", "Insira uma data válida no formato DD/MM/AAAA.");
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = System.Data.Entity.EntityState.Modified; 
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paciente);
        }

        // GET: Paciente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paciente paciente = db.Paciente.Find(id);
            db.Paciente.Remove(paciente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
