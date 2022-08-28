using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crReport.Areas.User.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace crReport.Areas.User.Controllers
{
    public class UserController : Controller
    {
        //cr_reportDataContext db = new cr_reportDataContext();
        // GET: User/User
        public ActionResult Index()
        {
            List<string[]> lsParam = new List<string[]>();
            lsParam.Add(new string[] { "isCons", "Y" });
            var data = UserModel.getList(lsParam);
            return View(data);
        }

        // GET: User/User/Details/5
        public ActionResult Details(int id)
        {
            UserModel details = new UserModel();
            details.getByid(id);
            return View("Details",details.lsITDetails[0]);
        }

        // GET: User/User/Create
        public ActionResult Create()
        {

            UserModel data = new UserModel();
            data.username = "";
            data.password = "";
            data.full_name = "";
            data.email = "";

            return View("Form", data);
        }

        // POST: User/User/Create
        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            try
            {
                // TODO: Add insert logic here
                UserModel userp = new UserModel();
                userp.username = user.username;
                userp.full_name = user.full_name;
                userp.password = user.password;
                userp.email = user.email;
                userp.role = user.role;
                userp.add();

                return RedirectToAction("index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/User/Edit/5
        public ActionResult Edit(int id)
        {
            UserModel data = new UserModel();
            data.getByid(id);
            return View("Form", data.lsITDetails[0]);
        }

        // POST: User/User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserModel user)
        {
            try
            {
                // TODO: Add update logic here
                UserModel userp = new UserModel();
                userp.id = user.id;
                userp.username = user.username;
                userp.full_name = user.full_name;
                userp.password = user.password;
                userp.email = user.email;
                userp.role = user.role;
                userp.update();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/User/Delete/5
        //public ActionResult Delete(int id)
        //{
          //  return View();
        //}

        // POST: User/User/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here

                UserModel id_user = new UserModel();
                id_user.delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("index");
            }
        }
        [HttpGet]
        public ActionResult ExportCustomers()
        {
         
            List<string[]> lsParam = new List<string[]>();
            lsParam.Add(new string[] { "isCons", "Y" });
            var allCustomer = UserModel.getList(lsParam);
            //allCustomer = db.user_approvals.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReport.rpt")));

            rd.SetDataSource(allCustomer);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CustomerList.pdf");
        }
    }
}
