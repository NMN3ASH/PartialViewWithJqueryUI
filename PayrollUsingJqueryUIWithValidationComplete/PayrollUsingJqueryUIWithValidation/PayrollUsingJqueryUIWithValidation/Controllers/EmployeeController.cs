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
    public class EmployeeController : Controller
    {
        private PayrollEntities1 db = new PayrollEntities1();

        // GET: /Employee/
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Department);
            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Code");
            return View(employees.ToList());
        }

        // GET: /Employee/Details/5
        // <<<<<<<<====Insert=======>>>>>>>>>>>>
        public ActionResult _CreateNewEmployee()
        {
            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Code");
            return PartialView();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CreateNewEmployee([Bind(Include = "Employee_Id,Name,Email,Contact,Address1,Department_Id,Join_date")] Employee employee)
        {

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                TempData["Msg"] = "Data has been created succeessfully";
                return RedirectToAction("Index");
            }
            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Code");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult _EditEmployee(int? id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
            {
                Department dept = db.Departments.Find(employee.Department_Id);

                ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Code");
		        return PartialView("_EditEmployee", employee);
            }
            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Code");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult _EditEmployee([Bind(Include = "Employee_Id,Name,Email,Contact,Address1,Department_Id,Join_date")] Employee employee, int id)
        {
            Employee employee1 = db.Employees.Find(id);
            if (ModelState.IsValid)
            {
                ViewBag.Department_Id1 = new SelectList(db.Departments.ToList(), "Department_Id", "Code", id);

                employee1.Name = employee.Name;
                employee1.Email = employee.Email;
                employee1.Address1 = employee.Address1;
                employee1.Contact = employee.Contact;
                employee1.Join_date = employee.Join_date;
                employee1.Department_Id = employee.Department_Id;
             
                db.Entry(employee1).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Msg"] = "Data has been updated succeessfully";
                return RedirectToAction("Index");
               
            }
        return RedirectToAction("Index");
        }
        // <<<<<<<<====Delete=======>>>>>>>>>>>>
        public ActionResult DeleteRecord(int? id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
            {
                try
                {
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                    TempData["Msg"] = "Data has been deleted succeessfully";
                }
                catch { }
            }
            return RedirectToAction("Index");
        }
        // <<<<<<<<====View Department Info=======>>>>>>>>>>>>


        public ActionResult _EmployeeDetails(int? id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
            {
                return PartialView("_EmployeeDetails", employee);
            }
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



















//    public class EmployeeController : Controller
//    {
//        private PayrollEntities1 db = new PayrollEntities1();

//        // GET: /Employee/
//        public ActionResult Index()
//        {
//            var employees = db.Employees.Include(e => e.Department);
//            return View(employees.ToList());
//        }

//        // GET: /Employee/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Employee employee = db.Employees.Find(id);
//            if (employee == null)
//            {
//                return HttpNotFound();
//            }
//            return View(employee);
//        }

//        // GET: /Employee/Create
//        public ActionResult Create()
//        {
//            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Code");
//            return View();
//        }

//        // POST: /Employee/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include="Employee_Id,Name,Email,Contact,Address1,Department_Id,Join_date")] Employee employee)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Employees.Add(employee);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Code", employee.Department_Id);
//            return View(employee);
//        }

//        // GET: /Employee/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Employee employee = db.Employees.Find(id);
//            if (employee == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Code", employee.Department_Id);
//            return View(employee);
//        }

//        // POST: /Employee/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include="Employee_Id,Name,Email,Contact,Address1,Department_Id,Join_date")] Employee employee)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(employee).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.Department_Id = new SelectList(db.Departments, "Department_Id", "Code", employee.Department_Id);
//            return View(employee);
//        }

//        // GET: /Employee/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Employee employee = db.Employees.Find(id);
//            if (employee == null)
//            {
//                return HttpNotFound();
//            }
//            return View(employee);
//        }

//        // POST: /Employee/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Employee employee = db.Employees.Find(id);
//            db.Employees.Remove(employee);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
