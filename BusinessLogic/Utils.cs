using PreInfoTrans.Models;
using System.Globalization;

namespace PreInfoTrans.BusinessLogic
{
    public static class Utils
    {
        public static Tsmp CheckType(string selectedName, Tsmp tsmp) 
        {
            string[] CodeName = selectedName.Split(':');
            if (CodeName.Length == 2)
            {
                tsmp.TypeCode = Int32.Parse(CodeName[0]);
                tsmp.Type = CodeName[1];
            }
            return tsmp;
        }
        public static List<EpiUsersViewModel> CalculateBars(List<EpiUsersViewModel> m) 
        {
            int minEpisCount = m.Min(e => e.EpisCount);
            int maxEpisCount = m.Max(e => e.EpisCount);
            int step = 300 / (maxEpisCount - minEpisCount + 1);

            foreach (var epiUser in m)
            {
                epiUser.ColorBar =  $"#{Random.Shared.Next(0x1000000):X6}";
                if (epiUser.EpisCount == minEpisCount)
                {
                    epiUser.PixelsBar = step;
                }
                else if (epiUser.EpisCount == maxEpisCount)
                {
                    epiUser.PixelsBar = 300;
                }
                else 
                {
                    epiUser.PixelsBar = (epiUser.EpisCount - minEpisCount + 1)*step;
                }
            }
            return m;
        }
        public static List<EpiResultViewModel> CalculateCircleDiagram(List<EpiResultViewModel> m)
        { 
            var summ = m.Sum(item => item.Count);
            double currentAngle = 0;
            m.RemoveAll(item => item.Result == Result.Deleted);
            var orderedResults = new List<Result>
            {
                Result.Created,
                Result.Pending,
                Result.Canceling,
                Result.Revoked,
                Result.Registration,
                Result.Denied,
                Result.Revoking,
                Result.Release,
                Result.Refused
            };

            foreach (var result in orderedResults)
            {
                var item = m.FirstOrDefault(c => c.Result == result);
                if (item != null)
                {
                    double percent = ((double)item.Count / summ) * 100.0;
                    item.Percent = Math.Round(percent, 2);
                    // Вычисляем угол для текущего сектора
                    double sectorAngle = percent * 3.6; // 1% = 3.6 градусов

                    // Рассчитываем угол для метки (средний угол сектора)
                    double labelAngle = currentAngle + sectorAngle / 2 -90;

                    // Определяем смещение метки по X и Y (радиус 200px)
                    double radians = labelAngle * (Math.PI / 180);
                    double radius = 100; // Радиус окружности
                    double labelX = radius * Math.Cos(radians) + radius + 100; // Смещение по X
                    double labelY = radius * Math.Sin(radians) + radius + 100; // Смещение по Y

                    item.LabelRotationDeg = $"{(int)labelAngle}deg";
                    item.LabelTranslateX = $"{(int)labelX}px";
                    item.LabelTranslateY = $"{(int)labelY}px";

                    currentAngle += sectorAngle;
                }
            }
            var created = m.FirstOrDefault(c => c.Result == Result.Created).Percent;
            var pending = m.FirstOrDefault(c => c.Result == Result.Pending).Percent;
            var canceling = m.FirstOrDefault(c => c.Result == Result.Canceling).Percent;
            var revoked = m.FirstOrDefault(c => c.Result == Result.Revoked).Percent;
            var registration = m.FirstOrDefault(c => c.Result == Result.Registration).Percent;
            var denied = m.FirstOrDefault(c => c.Result == Result.Denied).Percent;
            var revoking = m.FirstOrDefault(c => c.Result == Result.Revoking).Percent;
            var release = m.FirstOrDefault(c => c.Result == Result.Release).Percent;
            var refused = m.FirstOrDefault(c => c.Result == Result.Refused).Percent;
            var cp = created + pending;
            var cpc = created + pending + canceling;
            var cpcr = created + pending + canceling + revoked;
            var r100 = 100.0 - refused;
            var rr100 = 100.0 - refused - release;
            var rrr100 = 100.0 - refused - release - revoking;
            var rrrd = 100.0 - refused - release - revoking - denied;
            m.FirstOrDefault(c => c.Result == Result.Created).CssRepresentation = $"#0ff 0% {created.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}%, ";
            m.FirstOrDefault(c => c.Result == Result.Pending).CssRepresentation = $"#0f0 {created.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}% {cp.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}%, ";
            m.FirstOrDefault(c => c.Result == Result.Canceling).CssRepresentation = $"#f00 {cp.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}% {cpc.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}%, ";
            m.FirstOrDefault(c => c.Result == Result.Revoked).CssRepresentation = $"#800 {cpc.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}% {cpcr.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}%, ";
            m.FirstOrDefault(c => c.Result == Result.Registration).CssRepresentation = $"#ff0 {cpcr.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}% {rrrd.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}%, ";
            m.FirstOrDefault(c => c.Result == Result.Denied).CssRepresentation = $"#f80 {rrrd.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}% {rrr100.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}%, ";
            m.FirstOrDefault(c => c.Result == Result.Revoking).CssRepresentation = $"#f0f {rrr100.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}% {rr100.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}%, ";
            m.FirstOrDefault(c => c.Result == Result.Release).CssRepresentation = $"#00f {rr100.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}% {r100.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}%, ";
            m.FirstOrDefault(c => c.Result == Result.Refused).CssRepresentation = $"#808 {r100.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}% 100% ";

            
            return m;
        }
    }
}
