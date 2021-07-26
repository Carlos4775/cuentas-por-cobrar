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
    public class BalancesController : Controller
    {
        private BaseDatosCxCEntities1 db = new BaseDatosCxCEntities1();
       

        // GET: Balances
        public ActionResult Index(string Criterio = null)
        {

            return View(db.Balances.Where(p => Criterio == null || p.Clientes.Nombre_cliente.StartsWith(Criterio) ||
            p.Fecha_corte.ToString().StartsWith(Criterio) || p.Antiguedad_promedio_saldos.ToString().StartsWith(Criterio) ||
           p.Monto.ToString().StartsWith(Criterio)).ToList());

  
        }

        public ActionResult exportaExcel()
        {
            string filename = "prueba.csv";
            string filepath = @"c:\tmp\" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("Id balance,Id cliente,Fecha corte,Antiguedad promedio, Monto"); //Encabezado 
            foreach (var i in db.Balances.ToList())
            {
                sw.WriteLine(i.Id_balance.ToString() + "," + i.Id_cliente + "," + i.Fecha_corte + "," + i.Antiguedad_promedio_saldos + "," + i.Monto);
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

        public ActionResult Index2(string Criterio = null)
        {
            return View(db.Clientes.Where(p => Criterio == null || p.Nombre_cliente.StartsWith(Criterio)));
        }

        // GET: Balances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Balances balance = db.Balances.Find(id);
            if (balance == null)
            {
                return HttpNotFound();
            }
            return View(balance);
        }

        // GET: Balances/Create
        public ActionResult Create()
        {
            ViewBag.Id_cliente = new SelectList(db.Clientes, "Id_cliente", "Nombre_cliente");
            return View();
        }

        // POST: Balances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_balance,Id_cliente,Fecha_corte,Antiguedad_promedio_saldos,Monto")] Balances balance)
        {
            if (ModelState.IsValid)
            {
                db.Balances.Add(balance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_cliente = new SelectList(db.Clientes, "Id_cliente", "Nombre_cliente", balance.Id_cliente);
            return View(balance);
        }

        // GET: Balances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Balances balance = db.Balances.Find(id);
            if (balance == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_cliente = new SelectList(db.Clientes, "Id_cliente", "Nombre_cliente", balance.Id_cliente);
            return View(balance);
        }

        // POST: Balances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_balance,Id_cliente,Fecha_corte,Antiguedad_promedio_saldos,Monto")] Balances balance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(balance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_cliente = new SelectList(db.Clientes, "Id_cliente", "Nombre_cliente", balance.Id_cliente);
            return View(balance);
        }

        // GET: Balances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Balances balance = db.Balances.Find(id);
            if (balance == null)
            {
                return HttpNotFound();
            }
            return View(balance);
        }

        // POST: Balances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Balances balance = db.Balances.Find(id);
            db.Balances.Remove(balance);
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
