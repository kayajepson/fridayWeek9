using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class SpecialtyController : Controller
  {

    [HttpGet("/specialties")]
    public ActionResult Index()
    {
     List<Specialty> allSpecialty = Specialty.GetAll();
    return View(allSpecialty);
    }

    [HttpPost("/specialties")]
    public ActionResult Create()
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Specialty> specialty = Specialty.GetAll();
      List<Stylist> stylists = Stylist.GetAll();
      model.Add("specialty", specialty);
      model.Add("stylists", stylists);
      return View(model);
    }

    [HttpGet("/specialties/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty selectedSpecialty = Specialty.Find(id);
      model.Add("selectedSpecialty", selectedSpecialty);
      return View(model);
    }


    [HttpGet("/specialties/new")]
    public ActionResult New(int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      return View(specialty);
    }

    [HttpGet("/stylists/{stylistId}/specialties/{specialtyId}")]
    public ActionResult Show(int stylistId, int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("specialty", specialty);
      model.Add("stylist", stylist);
      return View(model);
    }

    [HttpPost("/specialties/delete")]
    public ActionResult DeleteAll()
    {
      Specialty.ClearAll();
      return RedirectToAction("Index", "Home");
    }

    [HttpGet("/stylists/{stylistId}/specialties/{specialtyId}/edit")]
    public ActionResult Edit(int stylistId, int specialtyId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      Specialty specialty = Specialty.Find(specialtyId);
      model.Add("specialty", specialty);
      return View(model);
    }


    // [HttpPost("/stylists/{stylistId}/specialties/{specialtyId}")]
    // public ActionResult Update(int stylistId, int specialtyId, string newNameSpecialty, string newHairType)
    // {
    //   Specialty specialty = Specialty.Find(specialtyId);
    //   specialty.Edit(newNameSpecialty, newHairType);
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Stylist stylist = Stylist.Find(stylistId);
    //   model.Add("stylist", stylist);
    //   model.Add("specialty", specialty);
    //   return RedirectToAction("Index", "Specialty");
    // }

    // [HttpPost("/stylists/{stylistId}/specialties/{specialtyId}/delete-specialty")]
    // public ActionResult DeleteSpecialty(int stylistId, int specialtyId)
    // {
    //   Specialty specialty = Specialty.Find(specialtyId);
    //   specialty.Delete();
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Stylist foundStylist = Stylist.Find(stylistId);
    //   List<Specialty> stylistSpecialty = foundStylist.GetSpecialty();
    //   model.Add("specialty", stylistSpecialty);
    //   model.Add("stylist", foundStylist);
    //   return RedirectToAction("Show", "Stylists");
    //   //return RedirectToAction("actionName", "controllerName"); goes to a cshtml page in a different controller.
    // }

  }
}
