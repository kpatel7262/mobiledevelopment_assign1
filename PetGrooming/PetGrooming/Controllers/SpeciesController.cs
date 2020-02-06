using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        private PetGroomingContext db = new PetGroomingContext();
        // GET: Species
        public ActionResult Index()
        {
            return View();
        }

        //TODO: Each line should be a separate method in this class
        // List
        public ActionResult List()
        {
            //what data do we need?
            List<Species> myspecies = db.Species.SqlQuery("Select * from species").ToList();

            return View(myspecies);
        }

        // Show
        public ActionResult Show(int id)
        {

            string query = "select * from species where SpeciesID = @id";
            SqlParameter param = new SqlParameter("@id", id);

            Species selectedspecies = db.Species.SqlQuery(query,param).FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Species species = db.Species.SqlQuery("select * from species where speciesid = @SpeciesID", new SqlParameter("@SpeciesID", id)).FirstOrDefault();
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }
        
        // [HttpPost] Add
        [HttpPost]
        public ActionResult Add(string SpeciesName)
        {
            string query = "insert into species(Name) values (@SpeciesName)";
            SqlParameter param = new SqlParameter("@SpeciesName", SpeciesName);
            
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("List");
        }

        // Add
        public ActionResult Add()
        {
            return View();
        }

        // Update
        public ActionResult Update(int id)
        {
            /*
            Pet selectedpet = db.Pets.SqlQuery("select * from pets where petid = @id", new SqlParameter("@id", id)).FirstOrDefault();

            string query = "select * from species";
            List<Species> selectedspecies = db.Species.SqlQuery(query).ToList();

            UpdatePet viewModel = new UpdatePet();
            viewModel.pet = selectedpet;
            viewModel.species = selectedspecies;

            return View(viewModel);*/

            string query = "select * from species where SpeciesID = @id";
            SqlParameter param = new SqlParameter("@id", id);


            Species selectdspecies = db.Species.SqlQuery(query, param).FirstOrDefault();
                
            return View(selectdspecies);
                
            
        }
        // [HttpPost] Update
        [HttpPost]
        public ActionResult Update(int id, string Name)
        {
            string query = "update species set Name=@SpeciesName where SpeciesID=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@SpeciesName", Name);
            sqlparams[1] = new SqlParameter("@id",id);
            Debug.WriteLine("I am trying to edit a species name to "+ Name + "");
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //logic for updating the pet in the database goes here
            return RedirectToAction("List");
        }
        // (optional) delete
        //public ActionResult Delete(int id) {
            //string query = "delete from species where SpeciesID=@id";
            //SqlParameter sqlparam = new SqlParameter()
            // [HttpPost] Delete

           // return View();
        //

        public ActionResult Delete(int id)
        {
            string query = "Delete from species where SpeciesID=@id";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
    }
}