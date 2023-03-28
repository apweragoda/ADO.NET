using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO.Net_MVC.DAL;
using ADO.Net_MVC.Models;

namespace ADO.Net_MVC.Controllers
{
    public class ProductController : Controller
    {

        ProductDAL productDAL = new ProductDAL();


        // GET: Product
        public ActionResult Index()
        {
            var productList = productDAL.GetAllProducts();

            if (productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently products are not available in the system.";
            }
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var products = productDAL.GetProductByID(id).FirstOrDefault();

            if (products == null)
            {
                TempData["InfoMessage"] = "Product not available with ID : " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();

        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool isInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    isInserted = productDAL.InsertProducts(product);

                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Product details saved successfully....!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product already existing or unable to save product details....!";
                    }
                }
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var products = productDAL.GetProductByID(id).FirstOrDefault();

            if (products == null)
            {
                TempData["InfoMessage"] = "Product not available with ID : " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            bool isUpdated = false;

            try
            {
                if (ModelState.IsValid)
                {
                    isUpdated = productDAL.UpdateProducts(product);

                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Product details updated successfully....!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product not available or unable to update the product details....!";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var products = productDAL.GetProductByID(id).FirstOrDefault();

                if (products == null)
                {
                    TempData["InfoMessage"] = "Product not available with ID : " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(products);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            string result = productDAL.DeleteProduct(id);
            try
            {
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
