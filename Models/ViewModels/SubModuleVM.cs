namespace WebAppTruck.Models.ViewModels
{
    public class SubModuleVM
    {
        public long SubModuleID { get; set; } = 0;
        public string SubDescription { get; set; } = "";
        public string SubAction { get; set; } = "";
        public string SubStatus { get; set; } = "";
        public int SubPosition { get; set; } = 0;
        public string SubRDate { get; set; } = "";
        public long ModuleID { get; set; } = 0;
        public long auxID { get; set; }
    }
}