using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberLayered.Migrations
{
    /// <inheritdoc />
    public partial class initial_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fk_CLientId",
                table: "Visits",
                newName: "fk_ClientId");

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

            // Insert fresh one
            // BarberShops
            migrationBuilder.Sql(
                "insert into \"BarberShops\"" +
                "(\"Name\", \"Address\", \"Phone\", \"Description\")\r\n" +
                "values(\r\n  'Родинний барбершоп', '123 Paper St', " +
                "'+38(099)3456789', \r\n  " +
                "'Наш барбершоп - це сучасний заклад, де кожен клієнт " +
                "отримує персоналізований сервіс від професійних барберів. " +
                "Ми знаходимося в центрі міста і пропонуємо широкий спектр послуг, " +
                "від стрижок і гоління до догляду за бородою та вусами. " +
                "Наш колектив складається з досвідчених майстрів, " +
                "які завжди готові задовольнити ваші потреби в стилі та догляді.'\r\n)"
                , true);
            // RegistrationKeys
            migrationBuilder.Sql(
                "insert into \"RegistrationKeys\"(\"Key\", \"Timestamp\")\r\n" +
                "values('qwerty123', CURRENT_TIMESTAMP), " +
                "('qwerty123', CURRENT_TIMESTAMP)"
                , true);
            // Clients
            migrationBuilder.Sql(
                "insert into \"Clients\"(\"Id\", \"Name\", \"Surname\", \"Phone\", \"Email\", \"PasswordHash\")\r\n" +
                "values(1, 'Andrew', 'Skvarko', '+38(068)8627181', 'skvarkoandriy@gmail.com', '$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi'),\r\n" +
                "(2, 'Rostyk', 'Stets', '+12(345)6789000', 'rostyk@meil.com', '$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi')",
                true);
            // Admins
            migrationBuilder.Sql(
                "insert into \"Admins\"(\"Id\", \"Name\", \"Surname\", \"Phone\", \"Email\", \"PasswordHash\")\r\n" +
                "values(1, 'El', 'Admino', '+00(000)0000000', 'eladmino@admin.com', '$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi')",
                true);
            // Barbers
            migrationBuilder.Sql(
                "insert into \"Barbers\"(\"Id\", \"Name\", \"Surname\", \"Phone\", \"Email\", \"PasswordHash\", \"PhotoUri\", \"Description\")\r\n" +
                "values(1, 'Влад', 'Ліннік', '+38(033)1231234', 'vlaDick@at.com', '$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi', 'https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=', 'Досвідчений Сеньйор Барбер, має хороші відгуки.'),\r\n" +
                "(2, 'Андрій', 'Шозрукою', '+12(345)6789876', 'sho.z.rukoyu@meil.com', '$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi', 'https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=', 'Всі його питають \"Шо з рукою?\" Відповідь вас здивує.')",
                true);
            // Reviews
            migrationBuilder.Sql(
                "insert into \"Reviews\"(\"fk_ClientId\", \"fk_BarberId\", \"Text\", \"Rating\", \"Date\")\r\n" +
                "values(1, 1, 'Постригли налисо, не сподобалось!', 1.0, CURRENT_TIMESTAMP),\r\n" +
                "(1, 1, 'Постригли під горщик, вже краще!', 2.5, CURRENT_TIMESTAMP),\r\n" +
                "(1, 2, 'Постригли, і на тому дякую!', 4.9, CURRENT_TIMESTAMP)",
                true);
            migrationBuilder.Sql(
                "insert into \"Reviews\"(\"fk_ClientId\", \"fk_BarberId\", \"Text\", \"Rating\", \"Date\")\r\n" +
                "values(2, 1, 'Постригли під 0, тепер схожий на скінхеда. Імба!', 5.0, CURRENT_TIMESTAMP),\r\n" +
                "(2, 2, 'Постригли під горщик, може бути!', 2.5, CURRENT_TIMESTAMP)",
                true);
            // Services
            migrationBuilder.Sql(
                "insert into \"Services\"(\"Id\", \"fk_BarberId\", \"Title\", \"Description\", \"Duration\", \"Price\")\r\n" +
                "values(1, 1, 'Стрижка', 'Стрижка машинкою', '01:00:00', '400'),\r\n" +
                "(2, 1, 'Стрижка бороди', 'Просто стрижка бороди, ніякого інтиму', '00:30:00', '250'),\r\n" +
                "(3, 1, 'Комплексна стрижка', 'Стрижка волосся на голові, машинкою чи ножицями, та бороди під замовлення', '01:45:00', '600'),\r\n" +
                "(4, 2, 'Стрижка одною рукою', 'Екзотична стрижка, займає трохи більше часу, зате дешевше', '00:50:00', '300'),\r\n" +
                "(5, 2, 'Стрижка з жонглюванням одною рукою', 'Назва говорить сама за себе', '01:20:00', '450')",
                true);
            // Schedules
            migrationBuilder.Sql(
                "insert into \"Schedules\"(\"Id\", \"fk_BarberId\", \"DayOfWeek\", \"StartTime\", \"EndTime\")\r\n" +
                "values(1, 1, 1, '09:00:00', '18:00:00'),\r\n" +
                "(2, 1, 2, '09:00:00', '17:00:00'),\r\n" +
                "(3, 1, 3, '10:00:00', '18:00:00'),\r\n" +
                "(4, 1, 4, '10:00:00', '19:00:00'),\r\n" +
                "(5, 1, 5, '08:30:00', '17:00:00'),\r\n" +
                "(6, 1, 6, '10:00:00', '16:00:00'),\r\n" +
                "(7, 1, 7, '11:00:00', '15:00:00'),\r\n" +
                "(8, 2, 1, '09:00:00', '18:00:00'),\r\n" +
                "(9, 2, 2, '10:00:00', '18:00:00'),\r\n" +
                "(10, 2, 3, '10:00:00', '19:00:00'),\r\n" +
                "(11, 2, 4, '10:00:00', '19:00:00'),\r\n" +
                "(12, 2, 5, '12:00:00', '18:00:00')\r\n",
                true);
            // Visits
            migrationBuilder.Sql(
                "insert into \"Visits\"(\"Id\", \"fk_CLientId\", \"fk_GuestId\", \"fk_BarberId\", \"fk_ServiceId\", \"Date\", \"Time\")\r\n" +
                "values (1, 1, null, 1, 1, CURRENT_DATE, '11:30:00'),\r\n" +
                "(2, 1, null, 1, 2, CURRENT_DATE + interval \'1 day\', '13:45:00'),\r\n" +
                "(3, 1, null, 2, 1, CURRENT_DATE, '10:00:00'),\r\n" +
                "(4, 1, null, 2, 2, CURRENT_DATE + interval '1 day', '12:20:00'),\r\n" +
                "(5, 2, null, 1, 1, CURRENT_DATE, '13:00:00'),\r\n" +
                "(6, 2, null, 1, 2, CURRENT_DATE + interval '1 day', '15:15:00'),\r\n" +
                "(7, 2, null, 1, 3, CURRENT_DATE + interval '2 days', '11:00:00'),\r\n" +
                "(8, 2 null, 2, 1, CURRENT_DATE, '12:45:00'),\r\n" +
                "(9, 1, null, 2, 2, CURRENT_DATE, '16:30:00')",
                true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "fk_ClientId",
                table: "Visits",
                newName: "fk_CLientId");
        }
    }
}
