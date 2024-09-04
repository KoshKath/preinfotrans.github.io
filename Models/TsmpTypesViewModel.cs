namespace PreInfoTrans.Models
{
    public class TsmpTypesViewModel
    {
        public string? DocName { get; set; }
        public Tsmp? Tsmp { get; set; }
        public List<TsmpTypes>? TsmpTypes { get; set; }
        public List<Countries>? Countries { get; set; }
        public List<Brands>? Brands { get; set;} 
    }
}
