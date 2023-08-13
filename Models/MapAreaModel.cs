using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelToSQL.Models
{
    public class MapAreaModel
    {
        public string mapId { get; set; }
        public string empId { get; set; }
        public string locId { get; set; }
        public string lugar { get; set; }
        public string area { get; set; }
        public List<MapAreaModel> MapList { get; set; }
        public MapAreaModel()
        {
            MapList = new List<MapAreaModel>();
        }
    }
}