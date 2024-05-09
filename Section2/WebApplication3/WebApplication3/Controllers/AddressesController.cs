using System;
using System.Collections.Generic;
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
    public class AddressesController : Controller
    {
        private testproject1Entities db = new testproject1Entities();

        public ActionResult Index()
        {
            List<Address> AddressList = new List<Address>();

            using (SqlConnection connection = new SqlConnection("DBConnection"))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "ReadAddress",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var view = new Address
                    {
                        AddressID = (int)sdr["AddressID"],
                        Street = sdr["Street"].ToString(),
                        City = sdr["City"].ToString()
                    };
                    AddressList.Add(view);
                }
                sdr.Close();
            }
            return View(AddressList);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            var viewModel = new Address();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Street,City")] Address viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Street))
            {
                ModelState.AddModelError("Street", "Street is required.");
            }

            if (string.IsNullOrEmpty(viewModel.City))
            {
                ModelState.AddModelError("City", "City is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            using (SqlConnection connection = new SqlConnection("DBConnection"))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "AddAddress",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
              
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
            var viewModel = new Address
            {
                AddressID = id.Value,

            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressID,Street,City")] Address viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Street))
            {
                ModelState.AddModelError("Street", "Street is required.");
            }

            if (string.IsNullOrEmpty(viewModel.City))
            {
                ModelState.AddModelError("City", "City is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            using (SqlConnection connection = new SqlConnection("DBConnection"))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "UpdateAddress",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@AddressID", viewModel.AddressID);
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
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection("DBConnection"))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "DeleteAddress",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@AddressID", id);

                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }

            if (rowsAffected > 0)
            {
                TempData["Message"] = "Address deleted successfully.";
            }
            else
            {
                TempData["Message"] = "Address not found or unable to delete.";
            }

            return Json(new { message = TempData["Message"] });
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
