using SPL_HOME_TASK.Contexts;
using SPL_HOME_TASK.Models;
using SPL_HOME_TASK.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SPL_HOME_TASK.Controllers
{
    public class DocumentCategoryInfoController : Controller
    {
        private readonly SPL_HOME_TASK_DbContext dbContext;

        public DocumentCategoryInfoController()
        {
            dbContext = new SPL_HOME_TASK_DbContext();
        }

        public ActionResult Index()
        {
            this.GetDocumentCategoryInfos();
            return View();
        }
        public JsonResult CreateDocumentCategoryInfo(CreateOrEditDocumentCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = Json(new { Status = false, Message = ""});

                if (model.CategoryId > 0)
                {
                    result = Update(model);
                }
                else
                {
                    result = Create(model);
                }

                return result;
            }
            else
            {
                return Json(new { Status = false, Message = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
            }
        }

        private JsonResult Create(CreateOrEditDocumentCategoryViewModel model)
        {
            try
            {
                var newCat = new DocumentCategoryInfo();
                newCat.CategoryName = model.CategoryName;
                newCat.CategoryNameBangla = model.CategoryNameBangla;
                newCat.Description = model.Description;

                dbContext.DocumentCategoryInfo.Add(newCat);
                var result = dbContext.SaveChanges();
                return Json(new { Status = true, Message = "Document Category Info Created Successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.Message });
            }
        }

        private JsonResult Update(CreateOrEditDocumentCategoryViewModel model)
        {
            try
            {
                var docCatInfo = dbContext.DocumentCategoryInfo.FirstOrDefault(item => item.CategoryId == model.CategoryId);

                docCatInfo.CategoryName = model.CategoryName;
                docCatInfo.CategoryNameBangla = model.CategoryNameBangla;
                docCatInfo.Description = model.Description;

                var result = dbContext.SaveChanges();
                return Json(new { Status = true, Message = "Document Category Info Updated Successfully!" });
            }
            catch(Exception ex)
            {
                return Json(new { Status = false, Message = ex.Message });
            }
        }

        public void GetDocumentCategoryInfos()
        {
            var documentCategoryInfos = dbContext.DocumentCategoryInfo.ToList();
            ViewBag.DocumentCategoryInfoList = documentCategoryInfos;
        }
    }
}