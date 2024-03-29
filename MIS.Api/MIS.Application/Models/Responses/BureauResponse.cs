﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class BureauResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public Guid ServiceId { get; set; }
        public Guid DivisioneId { get; set; }
        public string ServiceName { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
    }
}
