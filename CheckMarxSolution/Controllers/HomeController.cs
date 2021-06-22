using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CheckMarxSolution.Models;
using ExcelDataReader;

namespace CheckMarxSolution.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Name = "index";
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            String SearchIssue = formCollection["search_issue"];

            ItemModel searchResult = new ItemModel();
            List<ItemModel> items = new List<ItemModel>();
            var filename = "C:/Users/891147/source/repos/CheckMarxSolution/demo.xlsx";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        items.Add(new ItemModel
                        {
                            SearchIssue = reader.GetValue(0).ToString(),
                            Description = reader.GetValue(1).ToString(),
                            Solution = reader.GetValue(2).ToString()
                        });
                    }
                }
            }

            foreach(var item in items )
            {
                if(item.SearchIssue == SearchIssue)
                {
                    searchResult = item;
                }
            }
            ViewBag.Name = "index";
            return View(searchResult);
        }

        public JsonResult GetSearch(string Prefix)
        {
            Prefix = Prefix.ToLower();
            List<string> items = new List<string>();
            List<string> result = new List<string>();
            var filename = "C:/Users/891147/source/repos/CheckMarxSolution/demo.xlsx";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        items.Add(reader.GetValue(0).ToString());
                    }
                }
            }
            foreach(var i in items)
            {
                if (i.ToLower().StartsWith(Prefix))
                {
                    result.Add(i);
                }
            }
            //items = items.Where(x => x.StartsWith(Prefix)).Select(y => y).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            ViewBag.Name = "login";
            return View();
        }

        public ActionResult CreateItem()
        {
            ViewBag.Name = "CreateItem";
            return View();
        }
    }
}