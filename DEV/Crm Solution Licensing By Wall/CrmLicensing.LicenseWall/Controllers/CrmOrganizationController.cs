using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrmLicensing.LicenseWall.Models;
using WebMatrix.WebData;

namespace CrmLicensing.LicenseWall.Controllers
{
    [Authorize]
    public class CrmOrganizationController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /CrmOrganization/

        public ActionResult Index()
        {
            return View(db.CrmOrganizations.Where( org => org.User.UserName == User.Identity.Name).ToList());
        }

        //
        // GET: /CrmOrganization/Details/5

        public ActionResult Details(int id = 0)
        {
            CrmOrganization crmorganization = db.CrmOrganizations.Where(org => org.User.UserName == User.Identity.Name).FirstOrDefault(org => org.Id == id);
            if (crmorganization == null)
            {
                return HttpNotFound();
            }
            return View(crmorganization);
        }

        //
        // GET: /CrmOrganization/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CrmOrganization/Create

        [HttpPost]
        public ActionResult Create(CrmOrganization crmorganization)
        {
            if (ModelState.IsValid)
            {
                crmorganization.User = db.UserProfiles.Find(WebSecurity.GetUserId(User.Identity.Name));
                db.CrmOrganizations.Add(crmorganization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(crmorganization);
        }

        //
        // GET: /CrmOrganization/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CrmOrganization crmorganization = db.CrmOrganizations.Where(org => org.User.UserName == User.Identity.Name).FirstOrDefault(org => org.Id == id);
            if (crmorganization == null)
            {
                return HttpNotFound();
            }
            return View(crmorganization);
        }

        //
        // POST: /CrmOrganization/Edit/5

        [HttpPost]
        public ActionResult Edit(CrmOrganization crmorganization)
        {
            if (ModelState.IsValid)
            {
                crmorganization.UserId = db.UserProfiles.Where(user => user.UserName == User.Identity.Name).FirstOrDefault().UserId;
                db.Entry(crmorganization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(crmorganization);
        }

        //
        // GET: /CrmOrganization/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CrmOrganization crmorganization = db.CrmOrganizations.Where(org => org.User.UserName == User.Identity.Name).FirstOrDefault(org => org.Id == id);
            if (crmorganization==null)
            {
                return HttpNotFound();
            }
            return View(crmorganization);
        }

        //
        // POST: /CrmOrganization/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CrmOrganization crmorganization = db.CrmOrganizations.Where(org => org.User.UserName == User.Identity.Name).FirstOrDefault(org => org.Id == id);
            db.CrmOrganizations.Remove(crmorganization);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}