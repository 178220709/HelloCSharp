﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace MyMvcDemo.Models
{
    [Serializable]
    public class BasePagerModel<T> where T : new()
    {
        public BasePagerModel()
        {
            PageSize = 10;
            PageIndex = 1;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public int Total { get; set; }

        public IList<T> Rows { get; set; }


        [JsonIgnore]
        public  int Skip
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }
    }




}