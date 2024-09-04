using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreInfoTrans.Migrations
{
    public partial class EpiInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carrier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unp = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EpiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Epi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DirectionIn = table.Column<bool>(type: "bit", nullable: false),
                    TransportationType = table.Column<int>(type: "int", nullable: true),
                    DocName = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    DocDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegNumTDTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegNumOutTDTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<int>(type: "int", nullable: true),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPassengers = table.Column<int>(type: "int", nullable: false),
                    IsCrew = table.Column<int>(type: "int", nullable: false),
                    IsSupplies = table.Column<bool>(type: "bit", nullable: false),
                    IsGoods = table.Column<bool>(type: "bit", nullable: false),
                    Targets = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegCompleteTDTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegComleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TemporaryInDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SpareParts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TsmpFormatedString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    DateIssue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EpiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TsmpTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    TypeCode = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TsmpTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tsmp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeCode = table.Column<int>(type: "int", nullable: false),
                    RegNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VinCode = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true),
                    EpiDocName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EpiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tsmp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tsmp_Epi_EpiId",
                        column: x => x.EpiId,
                        principalTable: "Epi",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandCode", "BrandName" },
                values: new object[,]
                {
                    { 1, "58", "BELAZ" },
                    { 2, "70", "BMW" },
                    { 3, "80", "BRIAB" },
                    { 4, "128", "CITROEN" },
                    { 5, "137", "DAF" },
                    { 6, "138", "DAIHATSU" },
                    { 7, "139", "DAIMLER" },
                    { 8, "178", "FIAT" },
                    { 9, "186", "FORD" },
                    { 10, "200", "GEELY" },
                    { 11, "201", "GENERAL MOTORS" },
                    { 12, "202", "GENERIC" },
                    { 13, "257", "HONDA" },
                    { 14, "272", "HYUNDAI" },
                    { 15, "274", "IKARBUS" },
                    { 16, "291", "IVECO" },
                    { 17, "298", "JEEP" },
                    { 18, "329", "KIA" },
                    { 19, "346", "LADA" },
                    { 20, "370", "LIFAN" },
                    { 21, "406", "MAZ" },
                    { 22, "407", "MAZDA" },
                    { 23, "455", "NISSAN" },
                    { 24, "483", "PEUGEOT" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryCode", "CountryName" },
                values: new object[,]
                {
                    { 1, "AM", "АРМЕНИЯ" },
                    { 2, "AT", "АВСТРИЯ" },
                    { 3, "AZ", "АЗЕРБАЙДЖАН" },
                    { 4, "BG", "БОЛГАРИЯ" },
                    { 5, "BY", "БЕЛАРУСЬ" },
                    { 6, "CH", "ШВЕЙЦАРИЯ" },
                    { 7, "CN", "КИТАЙ" },
                    { 8, "CZ", "ЧЕХИЯ" },
                    { 9, "DE", "ГЕРМАНИЯ" },
                    { 10, "EE", "ЭСТОНИЯ" },
                    { 11, "FR", "ФРАНЦИЯ" },
                    { 12, "GE", "ГРУЗИЯ" },
                    { 13, "HU", "ВЕНГРИЯ" },
                    { 14, "IT", "ИТАЛИЯ" },
                    { 15, "JP", "ЯПОНИЯ" },
                    { 16, "KG", "КЫРГЫЗСТАН" },
                    { 17, "KZ", "КАЗАХСТАН" },
                    { 18, "LT", "ЛИТВА" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryCode", "CountryName" },
                values: new object[,]
                {
                    { 19, "LV", "ЛАТВИЯ" },
                    { 20, "MD", "МОЛДОВА, РЕСПУБЛИКА" },
                    { 21, "PL", "ПОЛЬША" },
                    { 22, "PT", "ПОРТУГАЛИЯ" },
                    { 23, "RU", "РОССИЯ" },
                    { 24, "TM", "ТУРКМЕНИСТАН" },
                    { 25, "TR", "ТУРЦИЯ" },
                    { 26, "UA", "УКРАИНА" }
                });

            migrationBuilder.InsertData(
                table: "Epi",
                columns: new[] { "Id", "CancelReason", "CreatorId", "Description", "DirectionIn", "DocDate", "DocName", "IsCrew", "IsDeleted", "IsGoods", "IsPassengers", "IsSupplies", "RegComleteDateTime", "RegCompleteTDTS", "RegDateTime", "RegEndDate", "RegNumOutTDTS", "RegNumTDTS", "Result", "Route", "RouteCountry", "SpareParts", "Targets", "TemporaryInDate", "TransportationType", "TsmpFormatedString" },
                values: new object[,]
                {
                    { 1, null, null, null, true, new DateTime(2024, 3, 15, 22, 5, 0, 0, DateTimeKind.Unspecified), "0000001", 0, false, false, 0, false, null, "", null, null, "", "", 1, null, null, null, 2, null, null, "AM65432" },
                    { 2, null, null, null, false, new DateTime(2024, 3, 15, 22, 6, 0, 0, DateTimeKind.Unspecified), "0000002", 0, false, false, 0, false, null, "", null, null, "", "", 2, null, null, null, 3, null, null, "RT5432/RT543" },
                    { 3, null, null, null, false, new DateTime(2024, 3, 15, 22, 7, 0, 0, DateTimeKind.Unspecified), "0000003", 0, false, false, 0, false, null, "", null, null, "", "", 3, null, null, null, 3, null, null, "AM65432" },
                    { 4, null, null, null, true, new DateTime(2024, 3, 15, 22, 8, 0, 0, DateTimeKind.Unspecified), "0000004", 0, false, false, 0, false, null, "", new DateTime(2024, 3, 15, 22, 10, 0, 0, DateTimeKind.Unspecified), null, "", "11206604/150324/301234567", 5, null, null, null, 0, null, null, "AM65432/ВВ1232" },
                    { 5, null, null, null, true, new DateTime(2024, 3, 15, 22, 9, 0, 0, DateTimeKind.Unspecified), "0000005", 0, false, false, 0, false, null, "", new DateTime(2024, 3, 15, 22, 11, 0, 0, DateTimeKind.Unspecified), null, "", "", 6, null, null, null, 0, null, null, "ВВ5555" },
                    { 6, null, null, null, false, new DateTime(2024, 3, 15, 22, 10, 0, 0, DateTimeKind.Unspecified), "0000006", 0, false, false, 0, false, null, "", null, null, "", "", 4, null, null, null, 1, null, null, "ВЕ12345678" },
                    { 7, null, null, null, false, new DateTime(2024, 3, 15, 22, 11, 0, 0, DateTimeKind.Unspecified), "0000007", 0, false, false, 0, false, new DateTime(2024, 3, 15, 22, 25, 0, 0, DateTimeKind.Unspecified), "", new DateTime(2024, 3, 15, 22, 20, 0, 0, DateTimeKind.Unspecified), null, "", "11206604/150324/307777777", 7, null, null, null, 1, null, null, "345МС00" },
                    { 8, null, null, null, true, new DateTime(2024, 3, 15, 22, 13, 0, 0, DateTimeKind.Unspecified), "0000008", 0, false, false, 0, false, new DateTime(2024, 3, 15, 22, 41, 0, 0, DateTimeKind.Unspecified), "", new DateTime(2024, 3, 15, 22, 30, 0, 0, DateTimeKind.Unspecified), null, "", "11206604/150324/307788888", 9, null, null, null, 0, null, null, "345МС34" },
                    { 9, null, null, null, true, new DateTime(2024, 3, 15, 22, 13, 0, 0, DateTimeKind.Unspecified), "0000009", 0, false, false, 0, false, new DateTime(2024, 3, 15, 22, 50, 0, 0, DateTimeKind.Unspecified), "", new DateTime(2024, 3, 15, 22, 31, 0, 0, DateTimeKind.Unspecified), null, "16412/31255555", "11206604/150324/306666666", 8, null, null, null, 0, null, null, "АА321321" },
                    { 10, null, null, null, true, new DateTime(2024, 3, 15, 22, 14, 0, 0, DateTimeKind.Unspecified), "0000010", 0, false, false, 0, false, new DateTime(2024, 3, 15, 22, 23, 0, 0, DateTimeKind.Unspecified), "11206604/160323/301234567", new DateTime(2024, 3, 15, 22, 20, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 8, 0, 0, 0, DateTimeKind.Unspecified), "16412/31234567", "11206604/150324/306549888", 8, null, null, null, 0, null, null, "АА32100/ММ65432" }
                });

            migrationBuilder.InsertData(
                table: "Tsmp",
                columns: new[] { "Id", "Brand", "EpiDocName", "EpiId", "Model", "RegCountry", "RegNum", "Type", "TypeCode", "VinCode" },
                values: new object[,]
                {
                    { 1, "SCANIA", "0000001", null, "", "", "AM65432", "", 30, "1FTCR10A4PTA70496" },
                    { 2, "VOLVO", "0000002", null, "", "", "RT5432", "", 30, "1GCHK23U74F122056" },
                    { 3, "LADA", "0000002", null, "", "", "RT543", "", 30, "JHLRM3H73CC097070" },
                    { 4, "SCANIA", "0000003", null, "", "", "AM65432", "", 30, "1FTCR10A4PTA70496" },
                    { 5, "SCANIA", "0000004", null, "", "", "AM65432", "", 30, "1FTCR10A4PTA70496" },
                    { 6, "RENAULT", "0000004", null, "", "", "ВВ1232", "", 30, "1N4AL3AP7EN305245" },
                    { 7, "МАЗ", "0000005", null, "", "", "ВВ5555", "", 30, "2C4GP54L35R273796" },
                    { 8, "BELGEE", "0000006", null, "", "", "ВЕ12345678", "", 30, "2CNDL13F166195441" },
                    { 9, "DONGFENG", "0000007", null, "", "", "345МС00", "", 30, "2B3KA43G47H859864" },
                    { 10, "NISSAN", "0000008", null, "", "", "345МС34", "", 30, "2HSCESBR15C086125" },
                    { 11, "LADA", "0000009", null, "", "", "АА321321", "", 30, "1HGCR2F59FA048733" },
                    { 12, "LADA", "0000010", null, "", "", "АА32100", "", 30, "1J8HG58276C176514" },
                    { 13, "КАМАЗ", "0000010", null, "", "", "ММ65432", "", 30, "2C4RDGCG9FR690732" }
                });

            migrationBuilder.InsertData(
                table: "TsmpTypes",
                columns: new[] { "Id", "Code", "Name", "TypeCode", "TypeName" },
                values: new object[,]
                {
                    { 1, 100, "водное судно", 10, "морской/речной транспорт" },
                    { 2, 203, "электропоезд", 20, "железнодорожный транспорт" },
                    { 3, 204, "тепловоз", 20, "железнодорожный транспорт" },
                    { 4, 210, "цистерна", 20, "железнодорожный транспорт" },
                    { 5, 212, "платформа", 20, "железнодорожный транспорт" },
                    { 6, 298, "прочий вагон", 20, "железнодорожный транспорт" },
                    { 7, 303, "грузовой автомобиль общего назначения", 30, "автодорожный транспорт" },
                    { 8, 306, "автомобиль-тягач", 30, "автодорожный транспорт" },
                    { 9, 307, "седельный тягач", 30, "автодорожный транспорт" },
                    { 10, 312, "грузовой прицеп общего назначения", 30, "автодорожный транспорт" },
                    { 11, 313, "специальный прицеп", 30, "автодорожный транспорт" }
                });

            migrationBuilder.InsertData(
                table: "TsmpTypes",
                columns: new[] { "Id", "Code", "Name", "TypeCode", "TypeName" },
                values: new object[,]
                {
                    { 12, 319, "грузовой полуприцеп общего назначения", 30, "автодорожный транспорт" },
                    { 13, 321, "автобус общего назначения", 30, "автодорожный транспорт" },
                    { 14, 322, "специальный автобус", 30, "автодорожный транспорт" },
                    { 15, 400, "воздушное судно", 40, "воздушный транспорт" },
                    { 16, 901, "контейнер", 40, "воздушный транспорт" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tsmp_EpiId",
                table: "Tsmp",
                column: "EpiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Carrier");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "Tsmp");

            migrationBuilder.DropTable(
                name: "TsmpTypes");

            migrationBuilder.DropTable(
                name: "Epi");
        }
    }
}
