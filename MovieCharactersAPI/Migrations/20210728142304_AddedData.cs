using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCharactersAPI.Migrations
{
    public partial class AddedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CharacterMovies",
                columns: new[] { "CharacterId", "MovieId" },
                values: new object[] { 4, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CharacterMovies",
                keyColumns: new[] { "CharacterId", "MovieId" },
                keyValues: new object[] { 4, 2 });
        }
    }
}
