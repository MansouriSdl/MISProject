﻿using MIS.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class ConsumptionType : AuditEntity
    {
        public string Name { get; set; }
        public List<Equipment> Equipments { get; set; }
    }
}
