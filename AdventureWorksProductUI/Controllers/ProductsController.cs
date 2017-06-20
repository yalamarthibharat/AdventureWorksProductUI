using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdventureWorksProductUI;
using PagedList;

namespace AdventureWorksProductUI.Controllers
{
    public class ProductsController : Controller
    {
        private CRUDDataModel db = new CRUDDataModel();
        
        // GET: Products
        public ActionResult Index(string SortOrder, string CurrentFilter, string SearchString, int? page)
        {
            ViewBag.CurrentSort = SortOrder;
            ViewBag.NameSortParameter = String.IsNullOrEmpty(SortOrder) ? "Name_desc" : "";
            ViewBag.NumberSortParameter = SortOrder == "Number" ? "Number_desc" : "Number";
            ViewBag.ColorSortParameter = SortOrder == "Color" ? "Color_desc" : "Color";
            ViewBag.SafetyStockLevelSortParameter = SortOrder == "SafetyStockLevel" ? "SafetyStockLevel_desc" : "SafetyStockLevel";
            ViewBag.ReorderPointSortParameter = SortOrder == "ReorderPoint" ? "ReorderPoint_desc" : "ReorderPoint";
            ViewBag.StandardCostSortParameter = SortOrder == "StandardCost" ? "StandardCost_desc" : "StandardCost";
            ViewBag.ListPriceSortParameter = SortOrder == "ListPrice" ? "ListPrice_desc" : "ListPrice";
            if (SearchString != null) page = 1;
            else SearchString = CurrentFilter;
            ViewBag.CurrentFilter = SearchString;
            var items = from s in db.Products select s;
            if (!String.IsNullOrEmpty(SearchString)) { items = items.Where(s => s.Name.Contains(SearchString)); }
            switch (SortOrder)
            {
                case "Name_desc":
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case "Number":
                    items = items.OrderBy(s => s.ProductNumber);
                    break;
                case "Number_desc":
                    items = items.OrderByDescending(s => s.ProductNumber);
                    break;
                case "Color":
                    items = items.OrderBy(s => s.Color);
                    break;
                case "Color_desc":
                    items = items.OrderByDescending(s => s.Color);
                    break;
                case "SafetyStockLevel":
                    items = items.OrderBy(s => s.SafetyStockLevel);
                    break;
                case "SafetyStockLevel_desc":
                    items = items.OrderByDescending(s => s.SafetyStockLevel);
                    break;
                case "ReorderPoint":
                    items = items.OrderBy(s => s.ReorderPoint);
                    break;
                case "ReorderPoint_desc":
                    items = items.OrderByDescending(s => s.ReorderPoint);
                    break;
                case "StandardCost":
                    items = items.OrderBy(s => s.StandardCost);
                    break;
                case "StandardCost_desc":
                    items = items.OrderByDescending(s => s.StandardCost);
                    break;
                case "ListPrice":
                    items = items.OrderBy(s => s.ListPrice);
                    break;
                case "ListPrice_desc":
                    items = items.OrderByDescending(s => s.ListPrice);
                    break;
                default:
                    items = items.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,rowguid,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,rowguid,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
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
