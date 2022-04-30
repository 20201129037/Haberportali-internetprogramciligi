using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haberciyiz.ViewModel
{
    public class HaberModel
    {
        public string HaberId { get; set; }
        public string HaberKatId { get; set; }
        public string HaberBaslik { get; set; }
        public string HaberAciklama { get; set; }
        public string EditorId { get; set; }
    }
}