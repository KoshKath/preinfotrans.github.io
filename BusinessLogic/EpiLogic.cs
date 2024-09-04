using Microsoft.EntityFrameworkCore;
using PreInfoTrans.Data;
using PreInfoTrans.Models;
using System.Diagnostics;

namespace PreInfoTrans.BusinessLogic
{
    public static class EpiLogic
    {
        private static List<string> countriesToCheck = new List<string> { "БЕЛАРУСЬ", "РОССИЯ", "КАЗАХСТАН", "АРМЕНИЯ", "КЫРГЫЗСТАН" };
 
        public static bool CheckAllTargetsOk(List<Tsmp> tfslist) 
        {
            // Проверяем, содержат ли все элементы страну из списка countriesToCheck
            bool allContain = tfslist.All(t => countriesToCheck.Contains(t.RegCountry));
            // Проверяем, не содержит ли ни один элемент страну из списка countriesToCheck
            bool noneContain = tfslist.All(t => !countriesToCheck.Contains(t.RegCountry));

            // Возвращаем true, если либо все содержат страну из списка, либо ни один не содержит
            return allContain || noneContain;
        }
        public static Targets CheckTargets(List<Tsmp> tfslist, bool directionIn) 
        {
            Targets result;
            bool isEAES = tfslist.Any(t => countriesToCheck.Contains(t.RegCountry));
            result = directionIn
                            ? (isEAES ? Targets.BackIn : Targets.TemporaryIn)
                            : (isEAES ? Targets.TemporaryOut : Targets.BackOut);
            return result;
        }
        public static string GetTsmpFormatedString(List<Tsmp> tfslist) 
        {
            return string.Join("/", tfslist.Select(c => c.RegNum));
        }
    }
}

