﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProjectLemon.Models
{
    public class Datum
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Paging
    {
        public string next { get; set; }
    }

    public class Summary
    {
        public int total_count { get; set; }
    }

    public class RootObject
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
        public Summary summary { get; set; }
    }
}