using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class IdentitySchema : Migration
    {
        private void govno_insert(MigrationBuilder migrationBuilder)
        {
            // Insert fresh one
            // BarberShops
            migrationBuilder.Sql(
                "insert into \"BarberShops\"" +
                "(\"Id\",\"Name\", \"Address\", \"Phone\", \"Description\")\r\n" +
                "values(100, 'Родинний барбершоп', '123 Paper St', " +
                "'+38(099)3456789', \r\n  " +
                "'Наш барбершоп - це сучасний заклад, де кожен клієнт " +
                "отримує персоналізований сервіс від професійних барберів. " +
                "Ми знаходимося в центрі міста і пропонуємо широкий спектр послуг, " +
                "від стрижок і гоління до догляду за бородою та вусами. " +
                "Наш колектив складається з досвідчених майстрів, " +
                "які завжди готові задовольнити ваші потреби в стилі та догляді.')",
                true);
            // RegistrationKeys
            migrationBuilder.Sql(
                "insert into \"RegistrationKeys\"(\"Id\", \"Key\", \"Timestamp\")\r\n" +
                "values(100, 'qwerty123', CURRENT_TIMESTAMP), " +
                "(101, 'qwerty123', CURRENT_TIMESTAMP)",
                true);
            // Clients
            migrationBuilder.Sql(
                "insert into \"Clients\"(\"Id\", \"Name\", \"Surname\", \"Phone\", \"Email\", \"PasswordHash\")\r\n" +
                "values(100, 'Andrew', 'Skvarko', '+38(068)8627181', 'skvarkoandriy@gmail.com', '$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi'),\r\n" +
                "(101, 'Rostyk', 'Stets', '+12(345)6789000', 'rostyk@meil.com', '$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi')",
                true);
            // Admins
            migrationBuilder.Sql(
                "insert into \"Admins\"(\"Id\", \"Name\", \"Surname\", \"Phone\", \"Email\", \"PasswordHash\")\r\n" +
                "values(100, 'El', 'Admino', '+00(000)0000000', 'eladmino@admin.com', '$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi')",
                true);
            // Barbers
            migrationBuilder.Sql(
                "insert into \"Barbers\"(\"Id\", \"Name\", \"Surname\", \"Phone\", \"Email\", \"PasswordHash\", \"PhotoUri\", \"Description\")\r\n" +
                "values(100, 'Влад', 'Ліннік', '+38(033)1231234', 'vlaDick@at.com', '$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi', 'https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=', 'Досвідчений Сеньйор Барбер, має хороші відгуки.'),\r\n" +
                "(101, 'Андрій', 'Шозрукою', '+12(345)6789876', 'sho.z.rukoyu@meil.com', '$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi', 'https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=', 'Всі його питають \"Шо з рукою?\" Відповідь вас здивує.')",
                true);
            // Reviews
            migrationBuilder.Sql(
                "insert into \"Reviews\"(\"fk_ClientId\", \"fk_BarberId\", \"Text\", \"Rating\", \"Date\")\r\n" +
                "values(100, 100, 'Постригли налисо, не сподобалось!', 1.0, CURRENT_TIMESTAMP),\r\n" +
                "(100, 100, 'Постригли під горщик, вже краще!', 2.5, CURRENT_TIMESTAMP),\r\n" +
                "(100, 101, 'Постригли, і на тому дякую!', 4.9, CURRENT_TIMESTAMP)",
                true);
            migrationBuilder.Sql(
                "insert into \"Reviews\"(\"fk_ClientId\", \"fk_BarberId\", \"Text\", \"Rating\", \"Date\")\r\n" +
                "values(101, 100, 'Постригли під 0, тепер схожий на скінхеда. Імба!', 5.0, CURRENT_TIMESTAMP),\r\n" +
                "(101, 101, 'Постригли під горщик, може бути!', 2.5, CURRENT_TIMESTAMP)",
                true);
            // Services
            migrationBuilder.Sql(
                "insert into \"Services\"(\"Id\", \"fk_BarberId\", \"Title\", \"Description\", \"Duration\", \"Price\")\r\n" +
                "values(100, 100, 'Стрижка', 'Стрижка машинкою', '01:00:00', '400'),\r\n" +
                "(101, 100, 'Стрижка бороди', 'Просто стрижка бороди, ніякого інтиму', '00:30:00', '250'),\r\n" +
                "(102, 100, 'Комплексна стрижка', 'Стрижка волосся на голові, машинкою чи ножицями, та бороди під замовлення', '01:45:00', '600'),\r\n" +
                "(103, 101, 'Стрижка одною рукою', 'Екзотична стрижка, займає трохи більше часу, зате дешевше', '00:50:00', '300'),\r\n" +
                "(104, 101, 'Стрижка з жонглюванням одною рукою', 'Назва говорить сама за себе', '01:20:00', '450')",
                true);
            // Schedules
            migrationBuilder.Sql(
                "insert into \"Schedules\"(\"Id\", \"fk_BarberId\", \"DayOfWeek\", \"StartTime\", \"EndTime\")\r\n" +
                "values(1, 100, 1, '09:00:00', '18:00:00'),\r\n" +
                "(2, 100, 2, '09:00:00', '17:00:00'),\r\n" +
                "(3, 100, 3, '10:00:00', '18:00:00'),\r\n" +
                "(4, 100, 4, '10:00:00', '19:00:00'),\r\n" +
                "(5, 100, 5, '08:30:00', '17:00:00'),\r\n" +
                "(6, 100, 6, '10:00:00', '16:00:00'),\r\n" +
                "(7, 100, 7, '11:00:00', '15:00:00'),\r\n" +
                "(8, 101, 1, '09:00:00', '18:00:00'),\r\n" +
                "(9, 101, 2, '10:00:00', '18:00:00'),\r\n" +
                "(10, 101, 3, '10:00:00', '19:00:00'),\r\n" +
                "(11, 101, 4, '10:00:00', '19:00:00'),\r\n" +
                "(12, 101, 5, '12:00:00', '18:00:00')\r\n",
                true);
            // Visits
            migrationBuilder.Sql(
                "insert into \"Visits\"(\"Id\", \"fk_ClientId\", \"fk_GuestId\", \"fk_BarberId\", \"fk_ServiceId\", \"Date\", \"Time\")\r\n" +
                "values (1, 100, null, 100, 100, CURRENT_DATE, '11:30:00'),\r\n" +
                "(2, 100, null, 100, 101, CURRENT_DATE + interval \'1 day\', '13:45:00'),\r\n" +
                "(3, 100, null, 101, 103, CURRENT_DATE, '10:00:00'),\r\n" +
                "(4, 100, null, 101, 104, CURRENT_DATE + interval '1 day', '12:20:00'),\r\n" +
                "(5, 101, null, 100, 100, CURRENT_DATE, '13:00:00'),\r\n" +
                "(6, 101, null, 100, 101, CURRENT_DATE + interval '1 day', '15:15:00'),\r\n" +
                "(7, 101, null, 100, 102, CURRENT_DATE + interval '2 days', '11:00:00'),\r\n" +
                "(8, 101, null, 101, 103, CURRENT_DATE, '12:45:00'),\r\n" +
                "(9, 100, null, 101, 104, CURRENT_DATE, '16:30:00')",
                true);
        }

        private void govno_delete(MigrationBuilder migrationBuilder)
        {
            // Clear previous data
            migrationBuilder.Sql("DELETE FROM \"Visits\"", true);
            migrationBuilder.Sql("DELETE FROM \"Reviews\"", true);
            migrationBuilder.Sql("DELETE FROM \"Schedules\"", true);
            migrationBuilder.Sql("DELETE FROM \"Services\"", true);
            migrationBuilder.Sql("DELETE FROM \"BarberShops\"", true);
            migrationBuilder.Sql("DELETE FROM \"Barbers\"", true);
            migrationBuilder.Sql("DELETE FROM \"Admins\"", true);
            migrationBuilder.Sql("DELETE FROM \"Reviews\"", true);
            migrationBuilder.Sql("DELETE FROM \"History\"", true);
            migrationBuilder.Sql("DELETE FROM \"RegistrationKeys\"", true);
            migrationBuilder.Sql("DELETE FROM \"Clients\"", true);
            migrationBuilder.Sql("DELETE FROM \"Guests\"", true);
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /// autogenerated
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Barbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PhotoUri = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PortfolioUri = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BarberShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    PhoneSecond = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhotoUri = table.Column<string>(type: "text", nullable: true),
                    SocialUri = table.Column<string>(type: "text", nullable: true),
                    SocialUriSecond = table.Column<string>(type: "text", nullable: true),
                    SocialUriThird = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarberShops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientPhone = table.Column<string>(type: "text", nullable: false),
                    BarberPhone = table.Column<string>(type: "text", nullable: false),
                    Service = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_ClientId = table.Column<int>(type: "integer", nullable: false),
                    fk_BarberId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_BarberId = table.Column<int>(type: "integer", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_BarberId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_ClientId = table.Column<int>(type: "integer", nullable: true),
                    fk_GuestId = table.Column<int>(type: "integer", nullable: true),
                    fk_BarberId = table.Column<int>(type: "integer", nullable: false),
                    fk_ServiceId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Barbers");

            migrationBuilder.DropTable(
                name: "BarberShops");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "RegistrationKeys");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
