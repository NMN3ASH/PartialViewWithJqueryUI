using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PayrollUsingJqueryUIWithValidation.Models;

namespace PayrollUsingJqueryUIWithValidation.Controllers
{
    public class PaymentController : Controller
    {
        private PayrollEntities1 db = new PayrollEntities1();

        // GET: /Payment/
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Employee);
            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name");
            return View(payments.ToList());
        }


        // GET: /Employee/Details/5
        // <<<<<<<<====Insert=======>>>>>>>>>>>>
        public ActionResult _CreateNewPayment()
        {
          
            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name");
            return PartialView();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CreateNewPayment([Bind(Include = "Payment_Id,Transaction_No,Transaction_Date,Employee_Id,Year_,Month_,Total_Amount")] Payment payment)
        {

            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                TempData["Msg"] = "Data has been created succeessfully";
                return RedirectToAction("Index");
            }
           
            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult _EditPayment(int? id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment != null)
            {
                Payment pay = db.Payments.Find(payment.Payment_Id);

               
                return PartialView("_EditPayment", payment);
            }
            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name");
           return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult _EditPayment([Bind(Include = "Payment_Id,Transaction_No,Transaction_Date,Employee_Id,Year_,Month_,Total_Amount")] Payment payment, int id)
        {
            Payment payment1 = db.Payments.Find(id);
            if (ModelState.IsValid)
            {
                payment1.Transaction_No = payment.Transaction_No;
                payment1.Transaction_Date = payment.Transaction_Date;
                payment1.Employee_Id = payment.Employee_Id;
                payment1.Month_ = payment.Month_;
                payment1.Year_ = payment.Year_;
                payment1.Total_Amount = payment.Total_Amount;

                db.Entry(payment1).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Msg"] = "Data has been updated succeessfully";
                return RedirectToAction("Index");

            } ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name");
            return RedirectToAction("Index");
        }
        // <<<<<<<<====Delete=======>>>>>>>>>>>>
        public ActionResult DeleteRecord(int? id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment != null)
            {
                try
                {
                    db.Payments.Remove(payment);
                    db.SaveChanges();
                    TempData["Msg"] = "Data has been deleted succeessfully";
                }
                catch { }
            }
            return RedirectToAction("Index");
        }
        // <<<<<<<<====View Department Info=======>>>>>>>>>>>>


        public ActionResult _PaymentDetails(int? id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment != null)
            {
                return PartialView("_PaymentDetails", payment);
            }
            return RedirectToAction("Index");

        }

        public ActionResult GetSalayInfo(int? employee_ID)
        {
            using (var context = new PayrollEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;
                Salary salary = context.Salaries.Where(x => x.Employee_Id == employee_ID).FirstOrDefault();
                if (salary != null)
                {
                    return Json(salary, JsonRequestBehavior.AllowGet);
                    //return PartialView("_PaymentDetails", employee);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }


        
        // GET: /Payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: /Payment/Create
        public ActionResult Create()
        {
            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name");
            return View();
        }

        // POST: /Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Payment_Id,Transaction_No,Transaction_Date,Employee_Id,Year_,Month_,Total_Amount")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name", payment.Employee_Id);
            return View(payment);
        }

        // GET: /Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name", payment.Employee_Id);
            return View(payment);
        }

        // POST: /Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Payment_Id,Transaction_No,Transaction_Date,Employee_Id,Year_,Month_,Total_Amount")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name", payment.Employee_Id);
            return View(payment);
        }

        // GET: /Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: /Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
