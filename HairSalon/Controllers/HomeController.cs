using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Client> clientList = Client.GetAll();
      List<Stylist> clientStylists = Stylist.GetAll();
      model.Add("client", clientList);
      model.Add("stylist", clientStylists);

      return View("Index", model);
    }

    [HttpGet("/client/add")]
    public ActionResult AddClient()
    {
      return View();
    }

    [HttpPost("/client/list")]
    public ActionResult WriteClients()
    {
      Client newClient = new Client(Request.Form["client-name"]);
      newClient.Save();
      List<Client> allClients = Client.GetAll();

      return View("ViewClient", allClients);
    }

    [HttpGet("/client/list")]
    public ActionResult ReadClients()
    {
      List<Client> allClients = Client.GetAll();

      return View("ViewClient", allClients);
    }

    [HttpGet("/{name}/{id}/stylistlist")]
    public ActionResult ViewStylistList(int id)
    {
      // Console.WriteLine("Hewwo");

      Dictionary<string, object> model = new Dictionary<string, object>();
      // Client is selected as an object
      Client selectedClient = Client.Find(id);
      // Stylists listed
      List<Stylist> clientStylists = selectedClient.GetStylists();

      model.Add("client", selectedClient);
      model.Add("stylists", clientStylists);

      // Return the stylist list
      return View("ClientDetail", model);
    }

    [HttpGet("/{name}/{id}/stylist/add")]
    public ActionResult AddStylist(int id)
    {
      // Client is selected as an object
      Client selectedClient = Client.Find(id);

      return View(selectedClient);
    }

    [HttpPost("/{name}/{id}/stylistlist")]
    public ActionResult AddStylistViewStylistList(int id)
    {
      Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-type"], id, int.Parse(Request.Form["stylist-price"]));
      newStylist.Save();
      Dictionary<string, object> model = new Dictionary<string, object>();
      // Client is selected as an object
      Client selectedClient = Client.Find(id);
      // Stylists are displayed in a list
      List<Stylist> clientStylists = selectedClient.GetStylists();
      //Console.WriteLine(id);

      model.Add("client", selectedClient);
      model.Add("stylists", clientStylists);
      // Console.WriteLine(clientStylists[0].GetDescription());

      // Return stylist list for selected client
      return View("ClientDetail", model);
    }

    [HttpGet("/stylists/{id}/edit")]
    public ActionResult StylistEdit(int id)
    {
      Stylist thisStylist = Stylist.Find(id);

      return View(thisStylist);
    }

    [HttpPost("/stylists/{id}/edit")]
    public ActionResult StylistEditConfirm(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.UpdateName(Request.Form["new-name"]);

      return RedirectToAction("Index");
    }

    [HttpGet("/{name}/{id}/stylist/delete")]
    public ActionResult StylistDelete(int id)
    {
      // Client is selected as an object
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.DeleteStylist();

      return RedirectToAction("Index");
    }

    [HttpGet("/{name}/{id}/client/delete")]
    public ActionResult ClientDelete(int id)
    {
      // Client is selected as an object
      Client thisClient = Client.Find(id);
      thisClient.DeleteClient();

      return RedirectToAction("Index");
    }
  }
}
