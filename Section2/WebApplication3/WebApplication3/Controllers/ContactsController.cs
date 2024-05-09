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
    public class ContactsController : Controller
    {
        private testproject1Entities db = new testproject1Entities();

        // GET: Contacts
        public ActionResult Index()
        {
            List<ContactAddressViewModel> viewModelList = new List<ContactAddressViewModel>();

            using (SqlConnection connection = new SqlConnection("DBConnection"))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "ReadContacts",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var view = new ContactAddressViewModel
                    {
                        ContactID = (int)sdr["ContactID"],
                        ContactName = sdr["ContactName"].ToString(),
                        Email = sdr["Email"].ToString(),
                        Title = sdr["Title"].ToString(),
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
            var viewModel = new ContactAddressViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactName,Email,Title,Street,City")] ContactAddressViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            using (SqlConnection connection = new SqlConnection("DBConnection"))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "CreateContact",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ContactName", viewModel.ContactName);
                cmd.Parameters.AddWithValue("@Email", viewModel.Email);
                cmd.Parameters.AddWithValue("@Title", viewModel.Title);
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
            var viewModel = new ContactAddressViewModel
            {
                ContactID = id.Value,

            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactID,ContactName,Email,Title,Street,City")] ContactAddressViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            using (SqlConnection connection = new SqlConnection("DBConnection"))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "UpdateContact",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ContactID", viewModel.ContactID);
                cmd.Parameters.AddWithValue("@ContactName", viewModel.ContactName);
                cmd.Parameters.AddWithValue("@Email", viewModel.Email);
                cmd.Parameters.AddWithValue("@Title", viewModel.Title);
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
                    CommandText = "DeleteContact",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ContactID", id);

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
