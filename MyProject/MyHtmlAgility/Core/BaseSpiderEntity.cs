﻿using System;
using MyProject.MongoDBDal;

namespace MyProject.MyHtmlAgility.Core
{
    public class BaseSpiderEntity : BaseEntity
    {
        public int Flag { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string StyleStr { get; set; }
        public DateTime Date { get; set; }
        public int Weight { get; set; }

    }
}