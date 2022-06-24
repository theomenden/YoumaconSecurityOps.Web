using System;
using System.Collections.Generic;

namespace YSecOps.Data.EfCore.Models
{
    public partial class RoomSchedule
    {
        public Guid Id { get; set; }
        public bool IsCurrentlyOccupied { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public string Name { get; set; }
        public int Keys { get; set; }
        public int ProvidedKeys { get; set; }
        public Guid Location_Id { get; set; }
    }
}
