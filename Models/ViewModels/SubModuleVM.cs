namespace WebAppTruck.Models.ViewModels
{
    public class SubModuleVM
    {
        public long SubModuleID { get; set; } = 0;
        public string SubDescription { get; set; } = "";
        public string SubAction { get; set; } = "";
        public string SubStatus { get; set; } = "";
        public int SubPosition { get; set; } = 0;
        public DateTime? SubRDate { get; set; }
        public long ModuleID { get; set; } = 0;
        public string MoDescription { get;set; } = "";
        public string MoController { get;set; } = "";
        public long auxID { get; set; }
        public int IsPermissionAply { get; set; }
    }
}