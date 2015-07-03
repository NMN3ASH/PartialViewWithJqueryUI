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
    public class SalaryController : Controller
    {
        private PayrollEntities1 db = new PayrollEntities1();

        // GET: /Salary/
        private static int employeeID11=0 ;
        public ActionResult Index()
        {
            var salaries = db.Salaries.Include(s => s.Employee);
            return View(salaries.ToList());
        }
        // <<<<<<<<====Insert=======>>>>>>>>>>>>
        public ActionResult _CreateNewSalary()
        {
            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name");
            return PartialView();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CreateNewSalary([Bind(Include = "Salary_Id,Employee_Id,Basic_Salary,House_Rent,Medical,Conveyance,Total")] Salary salary)
        {

            if (ModelState.IsValid)
            {
                db.Salaries.Add(salary);
                db.SaveChanges();
                TempData["Msg"] = "Data has been created succeessfully";
                return RedirectToAction("Index");
            }
          
            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult _EditSalary(int? id)
        {

            Salary salary = db.Salaries.Find(id);
            employeeID11 = (int) salary.Employee_Id;
            if (salary != null)
            {
                return PartialView("_EditSalary", salary);
            }
            //ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Code");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult _EditSalary([Bind(Include = "Salary_Id,Employee_Id,Basic_Salary,House_Rent,Medical,Conveyance,Total")] Salary salary, int id)
        {
            salary.Employee_Id= employeeID11;
            if (ModelState.IsValid)
            {
                salary.Salary_Id = id;
                db.Entry(salary).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Msg"] = "Data has been updated succeessfully";
                return RedirectToAction("Index");
            }
           return RedirectToAction("Index");
        }

        // <<<<<<<<====Delete=======>>>>>>>>>>>>
        public ActionResult DeleteRecord(int? id)
        {
            Salary salary = db.Salaries.Find(id);
            if (salary != null)
            {
                try
                {
                    db.Salaries.Remove(salary);
                    db.SaveChanges();
                    TempData["Msg"] = "Data has been deleted succeessfully";
                }
                catch { }
            }
            return RedirectToAction("Index");
        }
        // <<<<<<<<====View Department Info=======>>>>>>>>>>>>


        public ActionResult _SalaryDetails(int? id)
        {
            Salary salary = db.Salaries.Find(id);
            if (salary != null)
            {
                return PartialView("_SalaryDetails", salary);
            }
            return RedirectToAction("Index");

        }
        //Get
    






















        // GET: /Salary/Create
        public ActionResult Create()
        {
            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name");
            return View();
        }

        // POST: /Salary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Salary_Id,Employee_Id,Basic_Salary,House_Rent,Medical,Conveyance,Total")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                db.Salaries.Add(salary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name", salary.Employee_Id);
            return View(salary);
        }

        // GET: /Salary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name", salary.Employee_Id);
            return View(salary);
        }

        // POST: /Salary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Salary_Id,Employee_Id,Basic_Salary,House_Rent,Medical,Conveyance,Total")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }





            ViewBag.Employee_Id = new SelectList(db.Employees, "Employee_Id", "Name", salary.Employee_Id);
            return View(salary);
        }

        // GET: /Salary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // POST: /Salary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salary salary = db.Salaries.Find(id);
            db.Salaries.Remove(salary);
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
