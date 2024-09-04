namespace PreInfoTrans.Models
{
    public class EpiResultViewModel
    {
        public Result Result { get; set; }
        public int Count { get; set; }
        public double Percent { get; set; }
        public string? CssRepresentation { get; set; }
        public string? LabelRotationDeg { get; set; }
        public string? LabelTranslateX { get; set; }
        public string? LabelTranslateY { get; set; }
    }
}
