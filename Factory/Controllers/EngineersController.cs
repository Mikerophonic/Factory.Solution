using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class EngineersController : Controller 
  {
    private readonly FactoryContext _db;
    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Engineer> model = _db.Engineers.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer)
    {
      if (!ModelState.IsValid)
      {
      return View(engineer);
      }
      else if(string.IsNullOrEmpty(engineer.Name))
      {
      ModelState.AddModelError("Name", "Name is required.");
      return View(engineer);
      }
      else
      {
      _db.Engineers.Add(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
      }
    }

    public ActionResult Show(int id)
    {
      Engineer thisEngineer = _db.Engineers
                              .Include(Engineer => Engineer.JoinEntities)
                              .ThenInclude(join => join.Machine)
                              .FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    public ActionResult AddMachine(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult AddMachine(Machine machine, int engineerId)
    {
      #nullable enable
      EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.MachineId == machine.MachineId && join.EngineerId == engineerId));
      #nullable disable
      if(joinEntity == null && engineerId != 0)
      {
        _db.EngineerMachines.Add(new EngineerMachine() { MachineId = machine.MachineId, EngineerId = engineerId });
        _db.SaveChanges();
      }
      return RedirectToAction("Show", new { id = engineerId });
    }

    public ActionResult Delete(int id)
    {
        Engineer engineerToDelete = _db.Engineers.FirstOrDefault(e => e.EngineerId == id);
        return View(engineerToDelete);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        Engineer engineerToDelete = _db.Engineers.FirstOrDefault(e => e.EngineerId == id);

        _db.Engineers.Remove(engineerToDelete);
        _db.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      EngineerMachine joinEntry = _db.EngineerMachines.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachines.Remove(joinEntry);
      _db.SaveChanges();
      int engineerId = joinEntry.EngineerId;
      return RedirectToAction("Show", new { id = engineerId });
    }
 }
}
