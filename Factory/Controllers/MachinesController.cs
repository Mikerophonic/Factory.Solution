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
      return RedirectToAction("Index", "Home");
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

      public ActionResult Delete(int id)
    {
        Machine machineToDelete = _db.Machines.FirstOrDefault(e => e.MachineId == id);
        return View(machineToDelete);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        Machine machineToDelete = _db.Machines.FirstOrDefault(e => e.MachineId == id);

        _db.Machines.Remove(machineToDelete);
        _db.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      EngineerMachine joinEntry = _db.EngineerMachines.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachines.Remove(joinEntry);
      _db.SaveChanges();
      int machineId = joinEntry.MachineId;
      return RedirectToAction("Show", new { id = machineId });
    }

    public ActionResult Update(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Update(Machine machine)
    {
      _db.Machines.Update(machine);
      _db.SaveChanges();
      return RedirectToAction("Show", "Machines", new { id = machine.MachineId });

    }
 }
}
