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
    public class DepartmentController : Controller
    {
        private PayrollEntities1 db = new PayrollEntities1();

        // GET: /Department/
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // <<<<<<<<====Remote validation=======>>>>>>>>>>>>
        public JsonResult IsCodeExist(string code)
        {
            var department = db.Departments.Where(m => m.Code == code).FirstOrDefault();
            if (department != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult IsNameExist(string name)
        {
            var department = db.Departments.Where(m => m.Name == name).FirstOrDefault();
            if (department != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }


        // <<<<<<<<====Insert=======>>>>>>>>>>>>
        public ActionResult _CreateNewDepartment()
        {
            return PartialView();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CreateNewDepartment([Bind(Include = "Department_Id,Code,Name")] Department department)
        {

            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                TempData["Msg"] = "Data has been created succeessfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult _EditDepartment(int? id)
        {
            Department department = db.Departments.Find(id);
            if (department != null)
            {
                return PartialView("_EditDepartment", department);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult _EditDepartment([Bind(Include = "Department_Id,Code,Name")] Department department, int id)
        {

            if (ModelState.IsValid)
            {
                department.Department_Id = id;
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Msg"] = "Data has been updated succeessfully";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        // <<<<<<<<====Delete=======>>>>>>>>>>>>
        public ActionResult DeleteRecord(int? id)
        {
            Department department = db.Departments.Find(id);
            if (department != null)
            {
                try
                {
                    db.Departments.Remove(department);
                    db.SaveChanges();
                    TempData["Msg"] = "Data has been deleted succeessfully";
                }
                catch { }
            }
            return RedirectToAction("Index");
        }
        // <<<<<<<<====View Department Info=======>>>>>>>>>>>>


        public ActionResult _DepartmentDetails(int? id)
        {
            Department department = db.Departments.Find(id);
            if (department != null)
            {
                return PartialView("_DepartmentDetails", department);
            }
            return RedirectToAction("Index");

        }

    }
}