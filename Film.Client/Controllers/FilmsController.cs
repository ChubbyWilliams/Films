using Film.Client.Models;
using Film.Client.Models.DomainModels;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Film.Client.ViewModels;

namespace Film.Client.Controllers
{
    public class FilmsController : Controller
    {
        private FilmContext db = new FilmContext();


        [AllowAnonymous]
        public async Task<ActionResult> Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(db.FilmModels.OrderBy(f => f.Title).ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmModel filmModel = await db.FilmModels.FindAsync(id);
            if (filmModel == null)
            {
                return HttpNotFound();
            }
            return View(filmModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FilmCreate filmModel)
        {

            if (ModelState.IsValid)
            {
                FilmModel filmDomain = new FilmModel
                {
                    FilmId = Guid.NewGuid(),
                    Title = filmModel.Title,
                    Description = filmModel.Description,
                    ProductionYear = filmModel.ProductionYear,
                    Director = filmModel.Director,
                    PosterUri = filmModel.PosterUri,
                    UserId = User.Identity.GetUserId()
                };
                
                
                
                db.FilmModels.Add(filmDomain);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(filmModel);
        }

        [Authorize]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmModel filmModel = await db.FilmModels.FindAsync(id);
            if (filmModel == null)
            {
                return HttpNotFound();
            }
            return View(filmModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FilmId,Title,Description,ProductionYear,Director,PosterUri,UserId")] FilmModel filmModel)
        {
            if (filmModel.UserId == User.Identity.GetUserId())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(filmModel).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(filmModel);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [Authorize]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmModel filmModel = await db.FilmModels.FindAsync(id);
            if (filmModel == null)
            {
                return HttpNotFound();
            }
            return View(filmModel);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            FilmModel filmModel = await db.FilmModels.FindAsync(id);
            db.FilmModels.Remove(filmModel);
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
