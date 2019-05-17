using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;


namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {

    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }


        [HttpGet("/stylists/new")]
        public ActionResult New()
        {
          return View();
        }

        [HttpPost("/stylists")]
        public ActionResult Create(string stylistName, string stylistSpecialty)
        {
          Stylist newStylist = new Stylist(stylistName, stylistSpecialty);
          List<Stylist> allStylists = Stylist.GetAll();
          newStylist.Save();
          return RedirectToAction("Index");
        }


        [HttpGet("/stylists/{id}")]
        public ActionResult Show(int id)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Stylist selectedStylist = Stylist.Find(id);
          List<Client> stylistClients = selectedStylist.GetClients();
          model.Add("stylist", selectedStylist);
          model.Add("clients", stylistClients);
          return View(selectedStylist);
        }

        [HttpPost("/stylists/{stylistId}/clients")]
        public ActionResult Create(string nameClient, int stylistId)
        {
          Stylist foundStylist = Stylist.Find(stylistId);
          Client newClient = new Client(nameClient, stylistId);
          newClient.Save();
          foundStylist.GetClients();
          return View("Show", foundStylist);
        }

        [HttpPost("/stylists/{stylistId}/delete-stylist")]
        public ActionResult DeleteSty(int stylistId)
        {
          Stylist selectedStylist = Stylist.Find(stylistId);
          selectedStylist.DeleteStylist(stylistId);
          Dictionary<string, object> model = new Dictionary<string, object>();
          List<Client> stylistClients = selectedStylist.GetClients();
          model.Add("stylist", selectedStylist);
          return RedirectToAction("Index", "Stylists");
        }





  }
}
