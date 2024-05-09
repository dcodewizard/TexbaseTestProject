using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CompaniesController : Controller
    {
        private testproject1Entities db = new testproject1Entities();

        // GET: Companies
        public ActionResult Index()
        {
            List<CompayAddressViewModel> viewModelList = new List<CompayAddressViewModel>();

            using (SqlConnection connection = new SqlConnection("DBConnection"))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "ReadCompanies",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var view = new CompayAddressViewModel
                    {
                        CompanyID = (int)sdr["CompanyID"],
                        CompanyName = sdr["CompanyName"].ToString(),
                        AddressID = (int)sdr["AddressID"],
                        Street = sdr["Street"].ToString(),
                        City = sdr["City"].ToString()
                    };
                    viewModelList.Add(view);
                }
                sdr.Close();
            }
            return View(viewModelList);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            var viewModel = new CompayAddressViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyName,Street,City")] CompayAddressViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            using (SqlConnection connection = new SqlConnection("DBConnection"))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "CreateCompany",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CompanyName", viewModel.CompanyName);
                cmd.Parameters.AddWithValue("@Street", viewModel.Street);
                cmd.Parameters.AddWithValue("@City", viewModel.City);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
           var viewModel = new CompayAddressViewModel
            {
                CompanyID = id.Value, 

            }; 
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,CompanyName,Street,City")] CompayAddressViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            using (SqlConnection connection = new SqlConnection("DBConnection"))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "UpdateCompany",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CompanyID", viewModel.CompanyID);
                cmd.Parameters.AddWithValue("@CompanyName", viewModel.CompanyName);
                cmd.Parameters.AddWithValue("@Street", viewModel.Street);
                cmd.Parameters.AddWithValue("@City", viewModel.City);

                connection.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection("DBConnection"))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "DeleteCompany",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CompanyID", id);

                connection.Open();
                cmd.ExecuteNonQuery();
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
