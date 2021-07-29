using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCharactersAPI.Migrations
{
    public partial class AddedMoreData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CharacterMovies",
                columns: new[] { "CharacterId", "MovieId" },
                values: new object[,]
                {
                    { 4, 3 },
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CharacterMovies",
                keyColumns: new[] { "CharacterId", "MovieId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterMovies",
                keyColumns: new[] { "CharacterId", "MovieId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterMovies",
                keyColumns: new[] { "CharacterId", "MovieId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterMovies",
                keyColumns: new[] { "CharacterId", "MovieId" },
                keyValues: new object[] { 4, 3 });
        }
    }
}
