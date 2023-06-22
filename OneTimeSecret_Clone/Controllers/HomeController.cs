using OneTimeSecret_Clone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OneTimeSecret_Clone.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowSecret(string id)
        {
            try
            {

            
            secretTable secreto = new secretTable();

            using (One_Time_Secret_CloneEntities db = new One_Time_Secret_CloneEntities())
            {
                var resultado = (from d in db.secretTable
                           where d.hash == id
                           select d).First();

                secreto.secret = resultado.secret;

                db.secretTable.Remove(resultado);
                db.SaveChanges();

            }

                return View(secreto);
            }
            catch 
            {
                return View("SecretoNotFound");
            }

        }

        [HttpPost]
        public ActionResult CreateSecret(secretTable model)
        {
            secretTable nuevosecreto = new secretTable();
            if (!ModelState.IsValid)
            {
                return View("Index",model);
            }
            using (var db = new One_Time_Secret_CloneEntities())
            {
                
                nuevosecreto.hash = secretTable.HashGenerator(model.hash);
                nuevosecreto.secret = model.secret;
                db.secretTable.Add(nuevosecreto);
                db.SaveChanges();

            }
               

            return View("CreateLink", nuevosecreto);
        }

        public ActionResult CreateLink(secretTable model)
        {
            return View(model);
        }
    }
}