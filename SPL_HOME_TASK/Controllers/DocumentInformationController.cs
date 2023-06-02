using Newtonsoft.Json;
using SPL_HOME_TASK.Contexts;
using SPL_HOME_TASK.Models;
using SPL_HOME_TASK.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPL_HOME_TASK.Controllers
{
    public class DocumentInformationController : Controller
    {
        private readonly SPL_HOME_TASK_DbContext dbContext;

        public DocumentInformationController()
        {
            dbContext = new SPL_HOME_TASK_DbContext();
        }

        public ActionResult Index()
        {
            GetDocumentCategoryInfos();

            var model = TempData["ModelData"] as CreateOrEditDocumentInfo;
            if (model == null)
            {
                model = new CreateOrEditDocumentInfo();
                model.DocumentDate = DateTime.Now;
                model.MetaDataInfos = new List<CreateOrEditMetaDataInfoViewModel>();
                model.FileInfos = new List<CreateOrEditFileInfoViewModel>();
                model.MetaDataInfos.Add(new CreateOrEditMetaDataInfoViewModel());
                model.FileInfos.Add(new CreateOrEditFileInfoViewModel());
            }

            return View(model);
        }

        public ActionResult CreateOrEditDocumentInformation(CreateOrEditDocumentInfo model, List<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                var result = Json(new { Status = false, Message = "" });

                if (model.DocumentIdentity > 0)
                {
                    result = Update(model, files);
                }
                else
                {
                    result = Create(model, files);
                }

                return result;
            }
            else
            {
                return Json(new { Status = false, Message = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
            }
        }

        private JsonResult Create(CreateOrEditDocumentInfo model, List<HttpPostedFileBase> fileUploads)
        {
            try
            {
                var documentInfo = new DocumentInformation();
                documentInfo.CategoryId = model.CategoryId;
                documentInfo.DocumentReferenceName = model.DocumentReferenceName;
                documentInfo.DocumentDate = model.DocumentDate.ToLocalTime();
                documentInfo.DocumentName = model.DocumentName;
                documentInfo.DocumentNameBangla = model.DocumentNameBangla;
                documentInfo.Description = model.Description;

                dbContext.DocumentInformation.Add(documentInfo);
                var result = dbContext.SaveChanges();

                foreach (var item in model.MetaDataInfos)
                {
                    var metaData = new MetaDataInformation();
                    metaData.DocumentIdentity = documentInfo.DocumentIdentity;
                    metaData.MetaName = item.MetaName;
                    metaData.MetaNameBangla = item.MetaNameBangla;
                    metaData.Description = item.Description;

                    dbContext.MetaDataInformation.Add(metaData);
                    var metaresult = dbContext.SaveChanges();
                }

                foreach (var item in model.FileInfos)
                {
                    var fileData = new FileInformation();
                    fileData.DocumentIdentity = documentInfo.DocumentIdentity;
                    fileData.FileNo = item.FileNo;
                    fileData.FileName = item.FileName;
                    fileData.FileNameBangla = item.FileNameBangla;
                    fileData.Description = item.Description;
                    fileData.FileStatus = item.FileStatus;

                    var file = Request.Files[model.FileInfos.IndexOf(item)];
                    fileData.FilePath = UploadAttachment(file);

                    dbContext.FileInformation.Add(fileData);
                    var fileesult = dbContext.SaveChanges();
                }

                return Json(new { Status = true, Message = "Document Information Created Successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.Message });
            }
        }

        private JsonResult Update(CreateOrEditDocumentInfo model, List<HttpPostedFileBase> fileUploads)
        {
            try
            {
                var documentInfo = dbContext.DocumentInformation
                    .Where(doc => doc.DocumentIdentity == model.DocumentIdentity)
                    .FirstOrDefault();

                if (documentInfo == null)
                {
                    return Json(new { Status = false, Message = "Document Information Does Not Exists!" });
                }

                documentInfo.CategoryId = model.CategoryId;
                documentInfo.DocumentReferenceName = model.DocumentReferenceName;
                documentInfo.DocumentDate = model.DocumentDate;
                documentInfo.DocumentName = model.DocumentName;
                documentInfo.DocumentNameBangla = model.DocumentNameBangla;
                documentInfo.Description = model.Description;
                var result = dbContext.SaveChanges();

                foreach (var item in model.MetaDataInfos)
                {
                    //Update
                    if (item.MetaIdentity > 0)
                    {
                        var metaInfo = dbContext.MetaDataInformation
                        .Where(md => md.MetaIdentity == item.MetaIdentity)
                        .FirstOrDefault();

                        metaInfo.MetaName = item.MetaName;
                        metaInfo.MetaNameBangla = item.MetaNameBangla;
                        metaInfo.Description = item.Description;
                        dbContext.SaveChanges();
                    }
                    //Create
                    else
                    {
                        var metaData = new MetaDataInformation();
                        metaData.DocumentIdentity = documentInfo.DocumentIdentity;
                        metaData.MetaName = item.MetaName;
                        metaData.MetaNameBangla = item.MetaNameBangla;
                        metaData.Description = item.Description;
                        dbContext.MetaDataInformation.Add(metaData);
                        var metaresult = dbContext.SaveChanges();
                    }
                }

                foreach (var item in model.FileInfos)
                {
                    //Update
                    if (item.FileIdentity > 0)
                    {
                        var fileInfo = dbContext.FileInformation
                        .Where(md => md.FileIdentity == item.FileIdentity)
                        .FirstOrDefault();

                        fileInfo.FileNo = item.FileNo;
                        fileInfo.FileName = item.FileName;
                        fileInfo.FileNameBangla = item.FileNameBangla;
                        fileInfo.Description = item.Description;
                        fileInfo.FileStatus = item.FileStatus;

                        var file = Request.Files[model.FileInfos.IndexOf(item)];
                        fileInfo.FilePath = UploadAttachment(file);
                        var fileesult = dbContext.SaveChanges();
                    }
                    //Create
                    else
                    {
                        var fileData = new FileInformation();
                        fileData.DocumentIdentity = documentInfo.DocumentIdentity;
                        fileData.FileNo = item.FileNo;
                        fileData.FileName = item.FileName;
                        fileData.FileNameBangla = item.FileNameBangla;
                        fileData.Description = item.Description;
                        fileData.FileStatus = item.FileStatus;

                        var file = Request.Files[model.FileInfos.IndexOf(item)];
                        fileData.FilePath = UploadAttachment(file);

                        dbContext.FileInformation.Add(fileData);
                        var fileesult = dbContext.SaveChanges();
                    }
                }

                return Json(new { Status = true, Message = "Document Information Updated Successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.Message });
            }
        }

        public ViewResult DocumentInformationList()
        {
            var documentInformations = (from doc in dbContext.DocumentInformation
                                        join category in dbContext.DocumentCategoryInfo on doc.CategoryId equals category.CategoryId
                                        select new DocumentInformationsViewModel
                                        {
                                            DocumentIdentity = doc.DocumentIdentity,
                                            CategoryName = category.CategoryName,
                                            DocumentReferenceName = doc.DocumentReferenceName,
                                            DocumentDate = doc.DocumentDate,
                                            DocumentName = doc.DocumentName,
                                            DocumentNameBangla = doc.DocumentNameBangla,
                                            Description = doc.Description,
                                            Status = doc.Status,
                                        })
                         .ToList();
            return View(documentInformations);
        }

        public ViewResult DocumentSearchPage()
        {
            GetDocumentCategoryInfos();
            return View();
        }

        public ActionResult Search(SearchDocumentViewModel model)
        {
            try
            {
                var documents = (from doc in dbContext.DocumentInformation
                                 join cat in dbContext.DocumentCategoryInfo on doc.CategoryId equals cat.CategoryId
                                 join meta in dbContext.MetaDataInformation on doc.DocumentIdentity equals meta.DocumentIdentity
                                 join file in dbContext.FileInformation on doc.DocumentIdentity equals file.DocumentIdentity
                                 where model.CategoryId.HasValue && model.CategoryId > 0 ? doc.CategoryId == model.CategoryId : true
                                 where !string.IsNullOrEmpty(model.DocumentName) ? doc.DocumentName.Contains(model.DocumentName.Trim()) : true
                                 where !string.IsNullOrEmpty(model.DocumentReferenceName) ? doc.DocumentReferenceName.Contains(model.DocumentReferenceName.Trim()) : true
                                 where !string.IsNullOrEmpty(model.MetaName) ? meta.MetaName.Contains(model.MetaName.Trim()) : true
                                 where !string.IsNullOrEmpty(model.FileName) ? file.FileName.Contains(model.FileName.Trim()) : true
                                 where model.FromDate.HasValue ? DbFunctions.TruncateTime(doc.DocumentDate) >= DbFunctions.TruncateTime(model.FromDate.Value) : true
                                 where model.ToDate.HasValue ? DbFunctions.TruncateTime(doc.DocumentDate) <= DbFunctions.TruncateTime(model.ToDate.Value) : true
                                 select new SearchResultViewModel
                                 {
                                     DocumentIdentity = doc.DocumentIdentity,
                                     CategoryName = cat.CategoryName,
                                     DocumentName = doc.DocumentName,
                                     DocumentDate = doc.DocumentDate,
                                     DocumentReferenceName = doc.DocumentReferenceName,
                                     DocumentNameBangla = doc.DocumentNameBangla,
                                     Description = doc.Description
                                 })
                                 .Distinct()
                                 .ToList();

                return PartialView("_SearchResults", documents);
            }
            catch (Exception ex)
            {
                return PartialView("_SearchResults", new List<SearchResultViewModel>());
            }
        }

        public void GetDocumentCategoryInfos()
        {
            var documentCategoryInfos = dbContext.DocumentCategoryInfo.ToList();
            ViewBag.DocumentCategoryInfoList = documentCategoryInfos;
        }

        public JsonResult GetFileInfosByDocumentId(long documentId)
        {
            var fileInfos = dbContext.FileInformation
                .Where(fi => fi.DocumentIdentity == documentId)
                .ToList();

            return Json(fileInfos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NavigateToEditPage(long documentIdentity)
        {
            var model = new CreateOrEditDocumentInfo();
            var metaList = new List<CreateOrEditMetaDataInfoViewModel>();
            var fileList = new List<CreateOrEditFileInfoViewModel>();

            var documentInfo = dbContext.DocumentInformation.FirstOrDefault(item => item.DocumentIdentity == documentIdentity);
            model.DocumentIdentity = documentInfo.DocumentIdentity;
            model.CategoryId = documentInfo.CategoryId;
            model.DocumentReferenceName = documentInfo.DocumentReferenceName;
            model.DocumentDate = documentInfo.DocumentDate;
            model.DocumentName = documentInfo.DocumentName;
            model.DocumentNameBangla = documentInfo.DocumentNameBangla;
            model.Description = documentInfo.Description;

            var metaDatas = dbContext.MetaDataInformation
                .Where(md => md.DocumentIdentity == documentIdentity)
                .ToList();

            foreach (var item in metaDatas)
            {
                var metaData = new CreateOrEditMetaDataInfoViewModel
                {
                    MetaIdentity = item.MetaIdentity,
                    MetaName = item.MetaName,
                    MetaNameBangla = item.MetaNameBangla,
                    Description = item.Description,
                };

                metaList.Add(metaData);
            }

            model.MetaDataInfos = metaList;


            var fileDatas = dbContext.FileInformation
                .Where(fi => fi.DocumentIdentity == documentIdentity)
                .ToList();

            foreach (var item in fileDatas)
            {
                var fileData = new CreateOrEditFileInfoViewModel
                {
                    FileIdentity = item.FileIdentity,
                    FileNo = item.FileNo,
                    FileName = item.FileName,
                    FileNameBangla = item.FileNameBangla,
                    Description = item.Description,
                    FileStatus = item.FileStatus,
                    FilePath = item.FilePath
                };

                fileList.Add(fileData);
            }

            model.FileInfos = fileList;

            TempData["ModelData"] = model;
            return RedirectToAction("Index");
        }

        public string UploadAttachment(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var folderPath = Server.MapPath("~/Uploads/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);
                file.SaveAs(filePath);
                return filePath;
            }

            return null;
        }
    }
}