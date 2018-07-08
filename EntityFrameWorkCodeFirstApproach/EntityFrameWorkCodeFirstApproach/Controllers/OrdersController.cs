using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityFrameWorkCodeFirstApproach.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using EntityFrameWorkCodeFirstApproach.Services;
using Microsoft.AspNet.Identity;

namespace EntityFrameWorkCodeFirstApproach.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BlobStorageServices blobStorageServices = new BlobStorageServices();

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            return View(await db.Orders.ToListAsync());
        }
  
        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Order order, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {           
                    var fileUrl = Upload(file);
                    if (!string.IsNullOrEmpty(fileUrl))
                    {
                    order.FileName = file.FileName;
                    order.CreatedDate = DateTime.UtcNow;
                    order.ModifiedDate = DateTime.UtcNow;
                    order.UserId= User.Identity.GetUserId();
                    order.Url = fileUrl;
                    db.Orders.Add(order);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                
            }
            return View(order);
        }
        private string Upload(HttpPostedFileBase file)  
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    CloudBlobContainer blobContainer = blobStorageServices.GetCloudBlobContainer();
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(file.FileName);

                    // Upload content to the blob, which will create the blob if it does not already exist.  
                    blob.UploadFromStream(file.InputStream);
                    return blob.SnapshotQualifiedUri.ToString();
                }
            }
            catch (Exception ex)
            {
                CloudBlobContainer blobContainer = blobStorageServices.GetCloudBlobContainer();
                CloudBlockBlob blob2 = blobContainer.GetBlockBlobReference("myfile.txt");
                blob2.UploadText(ex.ToString());
                return string.Empty;

            }
            return string.Empty;
        }
        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,OrderDescription,OrderStatus,CreatedDate,ModifiedDate,DeliveryAddress,UserId,Url,PrintingCost,TotalPages,DeliveryDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
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
