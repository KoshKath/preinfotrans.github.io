using Humanizer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PreInfoTrans.Models;

namespace PreInfoTrans.Data
{
    public class EpiDbContext : DbContext
    {
        public DbSet<Epi>? Epi { get; set; }
        public DbSet<Tsmp>? Tsmp { get; set; }
        public DbSet<TsmpTypes>? TsmpTypes { get; set; }
        public DbSet<Owner>? Owner { get; set; }
        public DbSet<Carrier>? Carrier { get; set; }
        public DbSet<Countries>? Countries { get; set; }
        public DbSet<Brands>? Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Определить сущность TsmpType
            modelBuilder.Entity<TsmpTypes>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired();
                entity.Property(e => e.TypeCode).IsRequired();
                entity.Property(e => e.TypeName).IsRequired();
                entity.Property(e => e.Name).IsRequired();
            });
            // Добавить данные в таблицу TsmpTypes
            modelBuilder.Entity<TsmpTypes>().HasData(
            new TsmpTypes { Id = 1, Code = 100, TypeCode = 10, TypeName = "морской/речной транспорт", Name = "водное судно" },
            new TsmpTypes { Id = 2, Code = 203, TypeCode = 20, TypeName = "железнодорожный транспорт", Name = "электропоезд" },
            new TsmpTypes { Id = 3, Code = 204, TypeCode = 20, TypeName = "железнодорожный транспорт", Name = "тепловоз" },
            new TsmpTypes { Id = 4, Code = 210, TypeCode = 20, TypeName = "железнодорожный транспорт", Name = "цистерна" },
            new TsmpTypes { Id = 5, Code = 212, TypeCode = 20, TypeName = "железнодорожный транспорт", Name = "платформа" },
            new TsmpTypes { Id = 6, Code = 298, TypeCode = 20, TypeName = "железнодорожный транспорт", Name = "прочий вагон" },
            new TsmpTypes { Id = 7, Code = 303, TypeCode = 30, TypeName = "автодорожный транспорт", Name = "грузовой автомобиль общего назначения" },
            new TsmpTypes { Id = 8, Code = 306, TypeCode = 30, TypeName = "автодорожный транспорт", Name = "автомобиль-тягач" },
            new TsmpTypes { Id = 9, Code = 307, TypeCode = 30, TypeName = "автодорожный транспорт", Name = "седельный тягач" },
            new TsmpTypes { Id = 10, Code = 312, TypeCode = 30, TypeName = "автодорожный транспорт", Name = "грузовой прицеп общего назначения" },
            new TsmpTypes { Id = 11, Code = 313, TypeCode = 30, TypeName = "автодорожный транспорт", Name = "специальный прицеп" },
            new TsmpTypes { Id = 12, Code = 319, TypeCode = 30, TypeName = "автодорожный транспорт", Name = "грузовой полуприцеп общего назначения" },
            new TsmpTypes { Id = 13, Code = 321, TypeCode = 30, TypeName = "автодорожный транспорт", Name = "автобус общего назначения" },
            new TsmpTypes { Id = 14, Code = 322, TypeCode = 30, TypeName = "автодорожный транспорт", Name = "специальный автобус" },
            new TsmpTypes { Id = 15, Code = 400, TypeCode = 40, TypeName = "воздушный транспорт", Name = "воздушное судно" },
            new TsmpTypes { Id = 16, Code = 901, TypeCode = 40, TypeName = "воздушный транспорт", Name = "контейнер" }
            );

            modelBuilder.Entity<Epi>().HasData(
                new Epi { Id = 1, DirectionIn = true, DocName = "0000001", DocDate = new DateTime(2024, 3, 15, 22, 5, 0), Targets = Targets.BackIn,
                          TsmpFormatedString = "AM65432", RegNumTDTS = "", RegDateTime = null, RegNumOutTDTS = "", RegEndDate = null,
                          Result = Result.Created, RegCompleteTDTS = "", RegComleteDateTime = null},
                new Epi { Id = 2, DirectionIn = false, DocName = "0000002", DocDate = new DateTime(2024, 3, 15, 22, 6, 0), Targets = Targets.BackOut,
                          TsmpFormatedString = "RT5432/RT543", RegNumTDTS = "", RegDateTime = null, RegNumOutTDTS = "", RegEndDate = null,
                          Result = Result.Pending, RegCompleteTDTS = "", RegComleteDateTime = null},
                new Epi { Id = 3, DirectionIn = false, DocName = "0000003", DocDate = new DateTime(2024, 3, 15, 22, 7, 0), Targets = Targets.BackOut,
                          TsmpFormatedString = "AM65432", RegNumTDTS = "", RegDateTime = null, RegNumOutTDTS = "", RegEndDate = null,
                          Result = Result.Canceling, RegCompleteTDTS = "", RegComleteDateTime = null},
                new Epi { Id = 4, DirectionIn = true, DocName = "0000004", DocDate = new DateTime(2024, 3, 15, 22, 8, 0), Targets = Targets.TemporaryIn,
                          TsmpFormatedString = "AM65432/ВВ1232", RegNumTDTS = "11206604/150324/301234567", RegDateTime = new DateTime(2024, 3, 15, 22, 10, 0), RegNumOutTDTS = "", RegEndDate = null,
                          Result = Result.Registration, RegCompleteTDTS = "", RegComleteDateTime = null},
                new Epi { Id = 5, DirectionIn = true, DocName = "0000005", DocDate = new DateTime(2024, 3, 15, 22, 9, 0), Targets = Targets.TemporaryIn,
                          TsmpFormatedString = "ВВ5555", RegNumTDTS = "", RegDateTime = new DateTime(2024, 3, 15, 22, 11, 0), RegNumOutTDTS = "", RegEndDate = null,
                          Result = Result.Denied, RegCompleteTDTS = "", RegComleteDateTime = null},
                new Epi { Id = 6, DirectionIn = false, DocName = "0000006", DocDate = new DateTime(2024, 3, 15, 22, 10, 0), Targets = Targets.TemporaryOut,
                          TsmpFormatedString = "ВЕ12345678", RegNumTDTS = "", RegDateTime = null, RegNumOutTDTS = "", RegEndDate = null,
                          Result = Result.Revoked, RegCompleteTDTS = "", RegComleteDateTime = null},
                new Epi { Id = 7, DirectionIn = false, DocName = "0000007", DocDate = new DateTime(2024, 3, 15, 22, 11, 0), Targets = Targets.TemporaryOut,
                          TsmpFormatedString = "345МС00", RegNumTDTS = "11206604/150324/307777777", RegDateTime = new DateTime(2024, 3, 15, 22, 20, 0), RegNumOutTDTS = "", RegEndDate = null,
                          Result = Result.Revoking, RegCompleteTDTS = "", RegComleteDateTime = new DateTime(2024, 3, 15, 22, 25, 0)
                },
                new Epi { Id = 8, DirectionIn = true, DocName = "0000008", DocDate = new DateTime(2024, 3, 15, 22, 13, 0), Targets = Targets.TemporaryIn,
                          TsmpFormatedString = "345МС34", RegNumTDTS = "11206604/150324/307788888", RegDateTime = new DateTime(2024, 3, 15, 22, 30, 0), RegNumOutTDTS = "", RegEndDate = null,
                          Result = Result.Refused, RegCompleteTDTS = "", RegComleteDateTime = new DateTime(2024, 3, 15, 22, 41, 0)
                },
                new Epi { Id = 9, DirectionIn = true, DocName = "0000009", DocDate = new DateTime(2024, 3, 15, 22, 13, 0), Targets = Targets.TemporaryIn,
                          TsmpFormatedString = "АА321321", RegNumTDTS = "11206604/150324/306666666", RegDateTime = new DateTime(2024, 3, 15, 22, 31, 0), RegNumOutTDTS = "16412/31255555", RegEndDate = null,
                          Result = Result.Release, RegCompleteTDTS = "", RegComleteDateTime = new DateTime(2024, 3, 15, 22, 50, 0)
                },
                new Epi { Id = 10, DirectionIn = true, DocName = "0000010", DocDate = new DateTime(2024, 3, 15, 22, 14, 0), Targets = Targets.TemporaryIn,
                          TsmpFormatedString = "АА32100/ММ65432", RegNumTDTS = "11206604/150324/306549888", RegDateTime = new DateTime(2024, 3, 15, 22, 20, 0), RegNumOutTDTS = "16412/31234567", RegEndDate = new DateTime(2024, 3, 16, 8, 0, 0),
                          Result = Result.Release, RegCompleteTDTS = "11206604/160323/301234567", RegComleteDateTime = new DateTime(2024, 3, 15, 22, 23, 0)}
                );
            modelBuilder.Entity<Tsmp>().HasData(
                new Tsmp { Id=1, EpiDocName= "0000001", Brand="SCANIA", Model="", RegCountry="",
                           RegNum= "AM65432", Type="", TypeCode=30, VinCode= "1FTCR10A4PTA70496"},
                new Tsmp { Id=2, EpiDocName= "0000002", Brand="VOLVO", Model="", RegCountry="",
                           RegNum= "RT5432", Type="", TypeCode = 30, VinCode = "1GCHK23U74F122056" },
                new Tsmp { Id=3, EpiDocName= "0000002", Brand="LADA", Model="", RegCountry="",
                           RegNum= "RT543", Type="", TypeCode = 30, VinCode = "JHLRM3H73CC097070"},
                new Tsmp { Id=4, EpiDocName= "0000003", Brand="SCANIA", Model="", RegCountry="",
                           RegNum= "AM65432", Type="", TypeCode = 30, VinCode = "1FTCR10A4PTA70496" },
                new Tsmp { Id=5, EpiDocName= "0000004", Brand="SCANIA", Model="", RegCountry="",
                           RegNum= "AM65432", Type="", TypeCode = 30, VinCode = "1FTCR10A4PTA70496"},
                new Tsmp { Id=6, EpiDocName= "0000004", Brand="RENAULT", Model="", RegCountry="",
                           RegNum= "ВВ1232", Type="", TypeCode = 30, VinCode = "1N4AL3AP7EN305245"},
                new Tsmp { Id=7, EpiDocName= "0000005", Brand="МАЗ", Model="", RegCountry="",
                           RegNum= "ВВ5555", Type="", TypeCode = 30, VinCode = "2C4GP54L35R273796"},
                new Tsmp { Id=8, EpiDocName= "0000006", Brand="BELGEE", Model="", RegCountry="",
                           RegNum= "ВЕ12345678", Type="", TypeCode = 30, VinCode = "2CNDL13F166195441"},
                new Tsmp { Id=9, EpiDocName= "0000007", Brand="DONGFENG", Model="", RegCountry="",
                           RegNum= "345МС00", Type="", TypeCode = 30, VinCode = "2B3KA43G47H859864"},
                new Tsmp { Id=10, EpiDocName= "0000008", Brand="NISSAN", Model="", RegCountry="",
                           RegNum= "345МС34", Type="", TypeCode = 30, VinCode = "2HSCESBR15C086125"},
                new Tsmp { Id=11, EpiDocName= "0000009", Brand="LADA", Model="", RegCountry="",
                           RegNum= "АА321321", Type="", TypeCode = 30, VinCode = "1HGCR2F59FA048733"},
                new Tsmp { Id=12, EpiDocName= "0000010", Brand="LADA", Model="", RegCountry="",
                           RegNum= "АА32100", Type="", TypeCode = 30, VinCode = "1J8HG58276C176514"},
                new Tsmp { Id=13, EpiDocName= "0000010", Brand="КАМАЗ", Model="", RegCountry="",
                           RegNum= "ММ65432", Type="", TypeCode = 30, VinCode = "2C4RDGCG9FR690732"}

            );
            modelBuilder.Entity<Countries>().HasData(
                new Countries { Id = 1, CountryCode = "AM", CountryName = "АРМЕНИЯ"},
                new Countries { Id = 2, CountryCode = "AT", CountryName = "АВСТРИЯ" },
                new Countries { Id = 3, CountryCode = "AZ", CountryName = "АЗЕРБАЙДЖАН" },
                new Countries { Id = 4, CountryCode = "BG", CountryName = "БОЛГАРИЯ" },
                new Countries { Id = 5, CountryCode = "BY", CountryName = "БЕЛАРУСЬ" },
                new Countries { Id = 6, CountryCode = "CH", CountryName = "ШВЕЙЦАРИЯ" },
                new Countries { Id = 7, CountryCode = "CN", CountryName = "КИТАЙ" },
                new Countries { Id = 8, CountryCode = "CZ", CountryName = "ЧЕХИЯ" },
                new Countries { Id = 9, CountryCode = "DE", CountryName = "ГЕРМАНИЯ" },
                new Countries { Id = 10, CountryCode = "EE", CountryName = "ЭСТОНИЯ" },
                new Countries { Id = 11, CountryCode = "FR", CountryName = "ФРАНЦИЯ" },
                new Countries { Id = 12, CountryCode = "GE", CountryName = "ГРУЗИЯ" },
                new Countries { Id = 13, CountryCode = "HU", CountryName = "ВЕНГРИЯ" },
                new Countries { Id = 14, CountryCode = "IT", CountryName = "ИТАЛИЯ" },
                new Countries { Id = 15, CountryCode = "JP", CountryName = "ЯПОНИЯ" },
                new Countries { Id = 16, CountryCode = "KG", CountryName = "КЫРГЫЗСТАН" },
                new Countries { Id = 17, CountryCode = "KZ", CountryName = "КАЗАХСТАН" },
                new Countries { Id = 18, CountryCode = "LT", CountryName = "ЛИТВА" },
                new Countries { Id = 19, CountryCode = "LV", CountryName = "ЛАТВИЯ" },
                new Countries { Id = 20, CountryCode = "MD", CountryName = "МОЛДОВА, РЕСПУБЛИКА" },
                new Countries { Id = 21, CountryCode = "PL", CountryName = "ПОЛЬША" },
                new Countries { Id = 22, CountryCode = "PT", CountryName = "ПОРТУГАЛИЯ" },
                new Countries { Id = 23, CountryCode = "RU", CountryName = "РОССИЯ" },
                new Countries { Id = 24, CountryCode = "TM", CountryName = "ТУРКМЕНИСТАН" },
                new Countries { Id = 25, CountryCode = "TR", CountryName = "ТУРЦИЯ" },
                new Countries { Id = 26, CountryCode = "UA", CountryName = "УКРАИНА" }
                );
            modelBuilder.Entity<Brands>().HasData(
                new Brands { Id = 1, BrandCode = "58", BrandName = "BELAZ" },
                new Brands { Id = 2, BrandCode = "70", BrandName = "BMW" },
                new Brands { Id = 3, BrandCode = "80", BrandName = "BRIAB" },
                new Brands { Id = 4, BrandCode = "128", BrandName = "CITROEN" },
                new Brands { Id = 5, BrandCode = "137", BrandName = "DAF" },
                new Brands { Id = 6, BrandCode = "138", BrandName = "DAIHATSU" },
                new Brands { Id = 7, BrandCode = "139", BrandName = "DAIMLER" },
                new Brands { Id = 8, BrandCode = "178", BrandName = "FIAT" },
                new Brands { Id = 9, BrandCode = "186", BrandName = "FORD" },
                new Brands { Id = 10, BrandCode = "200", BrandName = "GEELY" },
                new Brands { Id = 11, BrandCode = "201", BrandName = "GENERAL MOTORS" },
                new Brands { Id = 12, BrandCode = "202", BrandName = "GENERIC" },
                new Brands { Id = 13, BrandCode = "257", BrandName = "HONDA" },
                new Brands { Id = 14, BrandCode = "272", BrandName = "HYUNDAI" },
                new Brands { Id = 15, BrandCode = "274", BrandName = "IKARBUS" },
                new Brands { Id = 16, BrandCode = "291", BrandName = "IVECO" },
                new Brands { Id = 17, BrandCode = "298", BrandName = "JEEP" },
                new Brands { Id = 18, BrandCode = "329", BrandName = "KIA" },
                new Brands { Id = 19, BrandCode = "346", BrandName = "LADA" },
                new Brands { Id = 20, BrandCode = "370", BrandName = "LIFAN" },
                new Brands { Id = 21, BrandCode = "406", BrandName = "MAZ" },
                new Brands { Id = 22, BrandCode = "407", BrandName = "MAZDA" },
                new Brands { Id = 23, BrandCode = "455", BrandName = "NISSAN" },
                new Brands { Id = 24, BrandCode = "483", BrandName = "PEUGEOT" }


                );
        }
        public EpiDbContext(DbContextOptions<EpiDbContext> options)
            : base(options)
        
        {
            
        }
    }
}
