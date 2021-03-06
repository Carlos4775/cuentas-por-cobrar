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
    public class TransaccionesController : Controller
    {
        private BaseDatosCxCEntities db = new BaseDatosCxCEntities();

        // GET: Transacciones
        public ActionResult Index(string Criterio = null)
        {

            return View(db.Transacciones.Where(p => Criterio == null || p.Tipo_Documentos.Descripcion.StartsWith(Criterio)
            ||p.Tipo_movimiento.StartsWith(Criterio) ||
            p.Numero_documento.ToString().StartsWith(Criterio) || p.Cliente.Nombre_cliente.StartsWith(Criterio) ||
            p.Fecha.ToString().StartsWith(Criterio)).ToList());


        }

        public ActionResult exportaExcel()
        {
            string filename = "prueba.csv";
            string filepath = @"c:\tmp\" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("Id Transaccion,Tipo Movimiento,Id Documento,Numero Documento,Fecha,Id Cliente,Monto"); //Encabezado 
            foreach (var i in db.Transacciones.ToList())
            {
                sw.WriteLine(i.Id_transaccion.ToString() + "," + i.Tipo_movimiento + "," + i.Id_tipoDocumento + "," + i.Numero_documento + "," + i.Fecha + "," + i.Id_cliente + "," + i.Monto);
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

        // GET: Transacciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccione transaccione = db.Transacciones.Find(id);
            if (transaccione == null)
            {
                return HttpNotFound();
            }
            return View(transaccione);
        }

        // GET: Transacciones/Create
        public ActionResult Create()
        {
            ViewBag.Id_cliente = new SelectList(db.Clientes, "Id_cliente", "Nombre_cliente");
            ViewBag.Id_tipoDocumento = new SelectList(db.Tipo_Documentos, "Id_tipoDocumento", "Descripcion");
            return View();
        }

        // POST: Transacciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_transaccion,Tipo_movimiento,Id_tipoDocumento,Numero_documento,Fecha,Id_cliente,Monto")] Transaccione transaccione)
        {
            if ((ModelState.IsValid) && (transaccione.Tipo_movimiento == "CR"))
            {
                var balanceCliente = db.Balances.Where(x => x.Id_cliente == transaccione.Id_cliente).FirstOrDefault();

                balanceCliente.Monto = balanceCliente.Monto - transaccione.Monto;

                db.Transacciones.Add(transaccione);

                db.Entry(balanceCliente).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else if ((ModelState.IsValid) && (transaccione.Tipo_movimiento == "DB"))
            {

                var balanceCliente = db.Balances.Where(x => x.Id_cliente == transaccione.Id_cliente).FirstOrDefault();

                balanceCliente.Monto = balanceCliente.Monto + transaccione.Monto;

                db.Transacciones.Add(transaccione);

                db.Entry(balanceCliente).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Id_cliente = new SelectList(db.Clientes, "Id_cliente", "Nombre_cliente", transaccione.Id_cliente);
            ViewBag.Id_tipoDocumento = new SelectList(db.Tipo_Documentos, "Id_tipoDocumento", "Descripcion", transaccione.Id_tipoDocumento);
            return View(transaccione);
        }

        // GET: Transacciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccione transaccione = db.Transacciones.Find(id);
            if (transaccione == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_cliente = new SelectList(db.Clientes, "Id_cliente", "Nombre_cliente", transaccione.Id_cliente);
            ViewBag.Id_tipoDocumento = new SelectList(db.Tipo_Documentos, "Id_tipoDocumento", "Descripcion", transaccione.Id_tipoDocumento);
            return View(transaccione);
        }

        // POST: Transacciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_transaccion,Tipo_movimiento,Id_tipoDocumento,Numero_documento,Fecha,Id_cliente,Monto")] Transaccione transaccione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaccione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_cliente = new SelectList(db.Clientes, "Id_cliente", "Nombre_cliente", transaccione.Id_cliente);
            ViewBag.Id_tipoDocumento = new SelectList(db.Tipo_Documentos, "Id_tipoDocumento", "Descripcion", transaccione.Id_tipoDocumento);
            return View(transaccione);
        }

        // GET: Transacciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccione transaccione = db.Transacciones.Find(id);
            if (transaccione == null)
            {
                return HttpNotFound();
            }
            return View(transaccione);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaccione transaccione = db.Transacciones.Find(id);
            db.Transacciones.Remove(transaccione);

            if ((ModelState.IsValid) && (transaccione.Tipo_movimiento == "CR"))
            {
                var balanceCliente = db.Balances.Where(x => x.Id_cliente == transaccione.Id_cliente).FirstOrDefault();

                balanceCliente.Monto = balanceCliente.Monto + transaccione.Monto;

                db.Transacciones.Add(transaccione);

                db.Entry(balanceCliente).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else if ((ModelState.IsValid) && (transaccione.Tipo_movimiento == "DB"))
            {

                var balanceCliente = db.Balances.Where(x => x.Id_cliente == transaccione.Id_cliente).FirstOrDefault();

                balanceCliente.Monto = balanceCliente.Monto - transaccione.Monto;

                db.Transacciones.Add(transaccione);

                db.Entry(balanceCliente).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }



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
