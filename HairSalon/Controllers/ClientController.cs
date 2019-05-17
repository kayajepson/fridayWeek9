using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {

    [HttpGet("/clients")]
    public ActionResult Index()
    {

      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Client> client = Client.GetAll();
      List<Stylist> stylists = Stylist.GetAll();
      model.Add("client", client);
      model.Add("stylists", stylists);
      return View(model);
    //  List<Client> allClients = Client.GetAll();
    // return View(allClients);
    }

    // [HttpPost("/clients")]
    // public ActionResult Create()
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   List<Client> client = Client.GetAll();
    //   List<Stylist> stylists = Stylist.GetAll();
    //   model.Add("client", client);
    //   model.Add("stylists", stylists);
    //   return View(model);
    // }

    [HttpGet("/clients/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Client selectedClient = Client.Find(id);
      List<Stylist> clientStylist = selectedClient.GetStylists();
      model.Add("selectedClient", selectedClient);
      model.Add("clientStylist", clientStylist);
      return View(model);
    }


    [HttpGet("/stylists/{stylistId}/clients/new")]
    public ActionResult New(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      return View(stylist);
    }

    [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Show(int stylistId, int clientId)
    {
      Client client = Client.Find(clientId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("client", client);
      model.Add("stylist", stylist);
      return View(model);
    }

    [HttpPost("/clients/delete")]
    public ActionResult DeleteAll()
    {
      Client.ClearAll();
      return RedirectToAction("Index", "Home");
    }

    [HttpGet("/stylists/{stylistId}/clients/{clientId}/edit")]
    public ActionResult Edit(int stylistId, int clientId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      Client client = Client.Find(clientId);
      model.Add("client", client);
      return View(model);
    }


    [HttpPost("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Update(int stylistId, int clientId, string newNameClient, string newHairType)
    {
      Client client = Client.Find(clientId);
      client.Edit(newNameClient, newHairType);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      model.Add("client", client);
      return RedirectToAction("Index", "Clients");
    }

    [HttpPost("/stylists/{stylistId}/clients/{clientId}/delete-client")]
    public ActionResult DeleteClient(int stylistId, int clientId)
    {
      Client client = Client.Find(clientId);
      client.Delete();
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist foundStylist = Stylist.Find(stylistId);
      List<Client> stylistClients = foundStylist.GetClients();
      model.Add("client", stylistClients);
      model.Add("stylist", foundStylist);
      return RedirectToAction("Show", "Stylists");
      //return RedirectToAction("actionName", "controllerName"); goes to a cshtml page in a different controller.
    }

  }
}
