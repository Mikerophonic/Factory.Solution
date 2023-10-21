using System;
using System.Collections.Generic;

namespace Factory.Models
{
  public class EngineerMachine
  {
    public int EngineerMachineId { get; set; } // Primary Key
    public int EngineerId { get; set; } // Foreign Key to the Engineers table
    public Engineer Engineer { get; set; } // Navigation Property for an Engineer
    public int MachineId { get; set; } // Foreign Key to the Machines table
    public Machine Machine { get; set; } // Navigation Property for a Machine
  }
}