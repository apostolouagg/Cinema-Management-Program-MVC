using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCERGASIA.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    cinemaid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cinemaname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    cinema_3d = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.cinemaid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    adminid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adminname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    AdminsUsername = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.adminid);
                    table.ForeignKey(
                        name: "FK_Admins_Users_AdminsUsername",
                        column: x => x.AdminsUsername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentAdmins",
                columns: table => new
                {
                    contentadminid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contentadminname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    ContentAdminUsername = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentAdmins", x => x.contentadminid);
                    table.ForeignKey(
                        name: "FK_ContentAdmins_Users_ContentAdminUsername",
                        column: x => x.ContentAdminUsername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customerid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customername = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CustomerUsername = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customerid);
                    table.ForeignKey(
                        name: "FK_Customers_Users_CustomerUsername",
                        column: x => x.CustomerUsername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    moviesid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    moviename = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    moviecontent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    movielength = table.Column<int>(type: "int", nullable: false),
                    movietype = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    moviesummary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    moviedirector = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    movieseats = table.Column<int>(type: "int", nullable: false),
                    ContentAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.moviesid);
                    table.ForeignKey(
                        name: "FK_Movies_ContentAdmins_ContentAdminId",
                        column: x => x.ContentAdminId,
                        principalTable: "ContentAdmins",
                        principalColumn: "contentadminid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provoles",
                columns: table => new
                {
                    provolesid = table.Column<int>(type: "int", maxLength: 45, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvolesMovieId = table.Column<int>(type: "int", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CinemaId = table.Column<int>(type: "int", nullable: false),
                    provoles_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContentAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provoles", x => x.provolesid);
                    table.ForeignKey(
                        name: "FK_Provoles_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "cinemaid");
                    table.ForeignKey(
                        name: "FK_Provoles_ContentAdmins_ContentAdminId",
                        column: x => x.ContentAdminId,
                        principalTable: "ContentAdmins",
                        principalColumn: "contentadminid");
                    table.ForeignKey(
                        name: "FK_Provoles_Movies_ProvolesMovieId",
                        column: x => x.ProvolesMovieId,
                        principalTable: "Movies",
                        principalColumn: "moviesid");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationsMovieId = table.Column<int>(type: "int", nullable: false),
                    ReservationsCinemaId = table.Column<int>(type: "int", nullable: false),
                    CustomersId = table.Column<int>(type: "int", nullable: false),
                    ReservationsMovieName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    reservations_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    number_of_seats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationsId);
                    table.ForeignKey(
                        name: "FK_Reservations_Cinemas_ReservationsCinemaId",
                        column: x => x.ReservationsCinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "cinemaid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Movies_ReservationsMovieId",
                        column: x => x.ReservationsMovieId,
                        principalTable: "Movies",
                        principalColumn: "moviesid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AdminsUsername",
                table: "Admins",
                column: "AdminsUsername");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAdmins_ContentAdminUsername",
                table: "ContentAdmins",
                column: "ContentAdminUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerUsername",
                table: "Customers",
                column: "CustomerUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ContentAdminId",
                table: "Movies",
                column: "ContentAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Provoles_CinemaId",
                table: "Provoles",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Provoles_ContentAdminId",
                table: "Provoles",
                column: "ContentAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Provoles_ProvolesMovieId",
                table: "Provoles",
                column: "ProvolesMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomersId",
                table: "Reservations",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationsCinemaId",
                table: "Reservations",
                column: "ReservationsCinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationsMovieId",
                table: "Reservations",
                column: "ReservationsMovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Provoles");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Cinemas");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "ContentAdmins");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
