﻿using System.Collections.Generic;
using System.Net;
using System.Web;
using MyProject.MongoDBDal;
using JsonSong.Spider.Core;
using MyProject.MyHtmlAgility.SpiderBase;


namespace MyMvcDemo.Models
{
    public class SpiderPagerModel : BasePagerModel<BaseSpiderEntity>
    {
        public string  Flag { get; set; }
        public string Title { get; set; }
        public int  Weight { get; set; }


        


    }

   

}