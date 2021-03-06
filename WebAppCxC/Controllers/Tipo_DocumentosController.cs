using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppCxC;

namespace WebAppCxC.Controllers
{
    public class Tipo_DocumentosController : Controller
    {
        private BaseDatosCxCEntities db = new BaseDatosCxCEntities();

        // GET: Tipo_Documentos
        public ActionResult Index(string Criterio = null)
        {

            return View(db.Tipo_Documentos.Where(p => Criterio == null || p.Descripcion.StartsWith(Criterio) ||
            p.Cuenta_contable.StartsWith(Criterio) || p.Estado.StartsWith(Criterio)).ToList());


        }

        public ActionResult exportaExcel()
        {
            string filename = "prueba.csv";
            string filepath = @"c:\tmp\" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("Id tipo documento,Descripcion,Cuenta,Estado"); //Encabezado 
            foreach (var i in db.Tipo_Documentos.ToList())
            {
                sw.WriteLine(i.Id_tipoDocumento.ToString() + "," + i.Descripcion + "," + i.Cuenta_contable + "," + i.Estado);
            }
            sw.Close();

            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = false,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }

        // GET: Tipo_Documentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Documentos tipo_Documentos = db.Tipo_Documentos.Find(id);
            if (tipo_Documentos == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Documentos);
        }

        // GET: Tipo_Documentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Documentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_tipoDocumento,Descripcion,Cuenta_contable,Estado")] Tipo_Documentos tipo_Documentos)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Documentos.Add(tipo_Documentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Documentos);
        }

        // GET: Tipo_Documentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Documentos tipo_Documentos = db.Tipo_Documentos.Find(id);
            if (tipo_Documentos == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Documentos);
        }

        // POST: Tipo_Documentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_tipoDocumento,Descripcion,Cuenta_contable,Estado")] Tipo_Documentos tipo_Documentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Documentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Documentos);
        }

        // GET: Tipo_Documentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Documentos tipo_Documentos = db.Tipo_Documentos.Find(id);
            if (tipo_Documentos == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Documentos);
        }

        // POST: Tipo_Documentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Documentos tipo_Documentos = db.Tipo_Documentos.Find(id);
            db.Tipo_Documentos.Remove(tipo_Documentos);
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
