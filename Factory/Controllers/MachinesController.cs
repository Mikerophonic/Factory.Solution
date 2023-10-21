using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
 public class MachinesController: Controller
 {
  private readonly FactoryContext _db;

  public MachinesController(FactoryContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    List<Machine> model = _db.Machines.ToList();
    return View(model);
  }
  
  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Machine machine)
  {
    if (!ModelState.IsValid)
    {
      return View(machine);
    }
    else if(string.IsNullOrEmpty(machine.Name))
    {
      ModelState.AddModelError("Name", "Name is required.");
      return View(machine);
    }
    else 
    {
      
      _db.Machines.Add(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }

    public ActionResult Show(int id)
  {
    // Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
    Machine thisMachine = _db.Machines
                             .Include(machine => machine.JoinEntities)
                             .ThenInclude(join => join.Engineer)
                             .FirstOrDefault(machine => machine.MachineId == id);
    return View(thisMachine);
  }

  public ActionResult AddEngineer(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(thisMachine);
    }

  [HttpPost]
  public ActionResult AddEngineer(Engineer engineer, int machineId)
  {
    #nullable enable
    EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.EngineerId == engineer.EngineerId && join.MachineId == machineId));
    #nullable disable
    if(joinEntity == null && machineId != 0)
    {
      _db.EngineerMachines.Add(new EngineerMachine() { EngineerId = engineer.EngineerId, MachineId = machineId });
      _db.SaveChanges();
    }
    return RedirectToAction("Show", new { id = machineId });
  }
 }
}
