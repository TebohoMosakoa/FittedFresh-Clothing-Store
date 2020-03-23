using FittedFresh.DAL;
using FittedFresh.Models;
using FittedFresh.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FittedFresh.Controllers
{
    public class AdminController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCatecgories()
        {
            List<SelectListItem> categoryList = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();

            foreach(var item in cat)
            {
                categoryList.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return categoryList;
        }

        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<Tbl_Category> categories = _unitOfWork
                .GetRepositoryInstance<Tbl_Category>()
                .GetAllRecordsIQuerable()
                .Where(c => c.IsDelete == false)
                .ToList();
            return View( categories);
        }


        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProducts());
        }

        [HttpGet]
        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCatecgories();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(Tbl_Product product, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Images/pic"), pic);

                file.SaveAs(path);
            }
            product.ProductImage = file != null? pic : product.ProductImage;
            product.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(product);
            return RedirectToAction("Product");
        }

        [HttpGet]
        public ActionResult ProductAdd(int? productId)
        {
            ViewBag.CategoryList = GetCatecgories();
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Tbl_Product product, HttpPostedFileBase file)
        {
            string pic = null;
            if(file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Images/pic"), pic);

                file.SaveAs(path);
            }
            product.ProductImage = pic;
            product.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(product);
            return RedirectToAction("Product");
        }

        [HttpGet]
        public ActionResult CategoryEdit(int categoryId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId));
        }

        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category category)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(category);
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public ActionResult CategoryAdd(int? categoryId)
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult CategoryAdd(Tbl_Category category)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Add(category);
            return RedirectToAction("Categories");
        }
    }
}