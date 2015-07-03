using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AddressBookUsingJqueryUIValidation.Models;

namespace AddressBookUsingJqueryUIValidation.Controllers
{
    public class PhoneBookController : Controller
    {
        private AddressbookEntities db = new AddressbookEntities();

        // GET: /PhoneBook/
        private static int groupID;
        public ActionResult Index()
        {
            var phonebooks = db.PhoneBooks.Include(p => p.GroupName);
            ViewBag.GroupNameId = new SelectList(db.GroupNames, "GroupNameId", "Name");
            return View(phonebooks.ToList());
        }

        // <<<<<<<<<<<<<<====Insert=======>>>>>>>>>>>>>>>>>>>>>>>
        public ActionResult _CreateNewPerson()
        {
            ViewBag.GroupNameId = new SelectList(db.GroupNames, "GroupNameId", "Name");
            return PartialView();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CreateNewPerson([Bind(Include = "PhoneBookId,Number,FirstName,LastName,Email,Address,GroupNameId,DateEntry")] PhoneBook phonebook)
        {
            ViewBag.GroupNameId = new SelectList(db.GroupNames, "GroupNameId", "Name");
            if (ModelState.IsValid)
            {
                phonebook.DateEntry = DateTime.Now;
                db.PhoneBooks.Add(phonebook);
                db.SaveChanges();
                TempData["Msg"] = "Data has been created succeessfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // <<<<<<<<<<<<<<====Edit=======>>>>>>>>>>>>>>>>>>>>>>>
        [HttpGet]
        public ActionResult _EditNewPerson(int? id)
        {
            groupID = (int) id;
            PhoneBook phoneBook = db.PhoneBooks.Find(id);
            ViewBag.GroupNameId = new SelectList(db.GroupNames, "GroupNameId", "Name");
            if (phoneBook != null)
            {
                return PartialView("_EditNewPerson", phoneBook);
            }
            return RedirectToAction("Index");
        }
       [HttpPost]
        public ActionResult _EditNewPerson([Bind(Include = "PhoneBookId,Number,FirstName,LastName,Email,Address,GroupNameId,DateEntry")] PhoneBook phoneBook)
        {
            phoneBook.PhoneBookId = groupID;
            phoneBook.DateEntry = DateTime.Now;
            
           if (ModelState.IsValid)
           {
                db.Entry(phoneBook).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Msg"] = "Data has been updated succeessfully";
                return RedirectToAction("Index");
            }
           ViewBag.GroupNameId = new SelectList(db.GroupNames, "GroupNameId", "Name", phoneBook.GroupNameId);
           return RedirectToAction("Index");
        }
        // <<<<<<<<====Delete=======>>>>>>>>>>>>
        public ActionResult DeleteRecord(int? id)
        {
            PhoneBook phoneBook = db.PhoneBooks.Find(id);
            if (phoneBook != null)
            {
                try
                {
                    db.PhoneBooks.Remove(phoneBook);
                    db.SaveChanges();
                    TempData["Msg"] = "Data has been deleted succeessfully";
                }
                catch { }
            }
            return RedirectToAction("Index");
        }
        // <<<<<<<<====View Department Info=======>>>>>>>>>>>>


        public ActionResult _PersonDetails(int? id)
        {
            PhoneBook phoneBook = db.PhoneBooks.Find(id);
            if (phoneBook != null)
            {
                return PartialView("_PersonDetails", phoneBook);
            }
            return RedirectToAction("Index");

        }



        // <<<<<<<<<<<<<<====Remote validation=======>>>>>>>>>>>>>>>>>>>>>>> // 
        public JsonResult IsNumberExist(string number)
        {
            var phonebook = db.PhoneBooks.Where(p => p.Number==number).FirstOrDefault();
            if (phonebook != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
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
