using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindEntity;
using SearchCustomerDatabase.Models;
using System.Data.Entity;
using System.Data;

namespace SearchCustomerDatabase.Controllers
{
    public class HomeController : Controller
    {
        CustomerSearch objSearch = new CustomerSearch();
        CustomerBearbeiten objEdit = new CustomerBearbeiten();
        NorthwindEntities entities = new NorthwindEntities();
        static string storage = "";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Kunde()
        {
            return View();
        }

        public ActionResult GetCustomer(string searchCustomer)
        {
            GetCustomers(searchCustomer);
            objSearch.oldCustomerSearch = searchCustomer;
            return View("Kunde", objSearch);
            
        }

        public IQueryable<Customer> GetCustomers(string searchCustomer)
        {  
            objSearch.customer = entities.Customers.Where(u => u.CompanyName.Contains(searchCustomer) || u.ContactName.Contains(searchCustomer));
            return objSearch.customer;
        }

        public IQueryable<Order> GetOrders(string id)
        {
            objSearch.orders = entities.Orders.Where(u => u.CustomerID == id);
            return objSearch.orders;
        }
        public IQueryable<Customer> GetDetails(string id)
        {
            objSearch.customer = entities.Customers.Where(u => u.CustomerID == id);
            return objSearch.customer;
        }

        public IQueryable<Order_Detail> GetOrderDetails(int id)
        {
            objSearch.orderDetails = entities.Order_Details.Where(x => x.OrderID == id);
            return objSearch.orderDetails;
        }

        public ActionResult Bestellungen(string id)
        {
            storage = id;
            GetOrders(id);
            GetDetails(id);
            objSearch.oldCustomerSearch = id;
            return View(objSearch);
        }

        public ActionResult BestellDetails(int id, string oldId)
        {
            objSearch.oldCustomerSearch = oldId;
            GetOrderDetails(id);
            GetDetails(storage);
            return View(objSearch);
        }

        public ActionResult Bearbeiten(string id, string searchCustomer)
        {
            objEdit.customer = entities.Customers.Where(u => u.CustomerID.Contains(id));
            foreach(var item in objEdit.customer)
            {
                objEdit.OldCustomerID = searchCustomer;
                objEdit.CustomerID = item.CustomerID;
                objEdit.CompanyName = item.CompanyName;
                objEdit.ContactName = item.ContactName;
                objEdit.ContactTitle = item.ContactTitle;
                objEdit.Address = item.Address;
                objEdit.City = item.City;
                objEdit.Region = item.Region;
                objEdit.PostalCode = item.PostalCode;
                objEdit.Country = item.Country;
                objEdit.Phone = item.Phone;
                objEdit.Fax = item.Fax;
            }
            return View(objEdit);
        }

        [HttpPost, ActionName("Bearbeiten")]
        [ValidateAntiForgeryToken]
        public ActionResult Bearbeiten(string id)
        {
            var customerToUdate = entities.Customers.Find(id);
            if(TryUpdateModel(customerToUdate, "", 
                new string[] {  "CompanyName", "ContactName", "ContactTitle", "Adress", "City", "Region", "PostalCode", "Country", "Phone", "Fax"}))
            {
                try
                {
                    entities.SaveChanges();
                    return RedirectToAction("Kunde"); 
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            return View(customerToUdate);
                         
        }

    }
}