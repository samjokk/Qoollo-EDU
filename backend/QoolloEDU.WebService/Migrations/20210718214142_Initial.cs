using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QoolloEDU.WebService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "QoolloEDU");

            migrationBuilder.CreateTable(
                name: "BaseUser",
                schema: "QoolloEDU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Hash = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false),
                    About = table.Column<string>(type: "text", nullable: true),
                    Photo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "QoolloEDU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Color = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Developer",
                schema: "QoolloEDU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Developer_BaseUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "QoolloEDU",
                        principalTable: "BaseUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizer",
                schema: "QoolloEDU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizer_BaseUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "QoolloEDU",
                        principalTable: "BaseUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperRating",
                schema: "QoolloEDU",
                columns: table => new
                {
                    DeveloperIdFrom = table.Column<int>(type: "integer", nullable: false),
                    DeveloperIdTo = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    DeveloperId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperRating", x => new { x.DeveloperIdFrom, x.DeveloperIdTo });
                    table.ForeignKey(
                        name: "FK_DeveloperRating_Developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Developer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperTag",
                schema: "QoolloEDU",
                columns: table => new
                {
                    DeveloperId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperTag", x => new { x.DeveloperId, x.TagId });
                    table.ForeignKey(
                        name: "FK_DeveloperTag_Developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Developer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeveloperTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Link",
                schema: "QoolloEDU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    URL = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    BaseUserId = table.Column<int>(type: "integer", nullable: true),
                    DeveloperId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Link", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Link_BaseUser_BaseUserId",
                        column: x => x.BaseUserId,
                        principalSchema: "QoolloEDU",
                        principalTable: "BaseUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Link_Developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Developer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                schema: "QoolloEDU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizerId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Organizer_OrganizerId",
                        column: x => x.OrganizerId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Organizer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventRating",
                schema: "QoolloEDU",
                columns: table => new
                {
                    DeveloperId = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRating", x => new { x.DeveloperId, x.EventId });
                    table.ForeignKey(
                        name: "FK_EventRating_Developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Developer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRating_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTag",
                schema: "QoolloEDU",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTag", x => new { x.TagId, x.EventId });
                    table.ForeignKey(
                        name: "FK_EventTag_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "QoolloEDU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EventId = table.Column<int>(type: "integer", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Markdown = table.Column<string>(type: "text", nullable: true),
                    Place = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperProject",
                schema: "QoolloEDU",
                columns: table => new
                {
                    DeveloperId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperProject", x => new { x.DeveloperId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_DeveloperProject_Developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Developer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeveloperProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaContent",
                schema: "QoolloEDU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    URL = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaContent_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                schema: "QoolloEDU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRating",
                schema: "QoolloEDU",
                columns: table => new
                {
                    DeveloperId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRating", x => new { x.DeveloperId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectRating_Developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Developer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectRating_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTag",
                schema: "QoolloEDU",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTag", x => new { x.ProjectId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ProjectTag_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                schema: "QoolloEDU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeveloperId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_Developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Developer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "QoolloEDU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NewsId = table.Column<int>(type: "integer", nullable: false),
                    DeveloperId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Developer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_News_NewsId",
                        column: x => x.NewsId,
                        principalSchema: "QoolloEDU",
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestTag",
                schema: "QoolloEDU",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    RequestId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTag", x => new { x.TagId, x.RequestId });
                    table.ForeignKey(
                        name: "FK_RequestTag_Request_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "QoolloEDU",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseUser_Email",
                schema: "QoolloEDU",
                table: "BaseUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_DeveloperId",
                schema: "QoolloEDU",
                table: "Comment",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_NewsId",
                schema: "QoolloEDU",
                table: "Comment",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Developer_Nickname",
                schema: "QoolloEDU",
                table: "Developer",
                column: "Nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Developer_UserId",
                schema: "QoolloEDU",
                table: "Developer",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperProject_ProjectId",
                schema: "QoolloEDU",
                table: "DeveloperProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperRating_DeveloperId",
                schema: "QoolloEDU",
                table: "DeveloperRating",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperTag_TagId",
                schema: "QoolloEDU",
                table: "DeveloperTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Name",
                schema: "QoolloEDU",
                table: "Event",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_OrganizerId",
                schema: "QoolloEDU",
                table: "Event",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRating_EventId",
                schema: "QoolloEDU",
                table: "EventRating",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTag_EventId",
                schema: "QoolloEDU",
                table: "EventTag",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Link_BaseUserId",
                schema: "QoolloEDU",
                table: "Link",
                column: "BaseUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Link_DeveloperId",
                schema: "QoolloEDU",
                table: "Link",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaContent_ProjectId",
                schema: "QoolloEDU",
                table: "MediaContent",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_News_ProjectId",
                schema: "QoolloEDU",
                table: "News",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizer_UserId",
                schema: "QoolloEDU",
                table: "Organizer",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_EventId",
                schema: "QoolloEDU",
                table: "Project",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Name",
                schema: "QoolloEDU",
                table: "Project",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRating_ProjectId",
                schema: "QoolloEDU",
                table: "ProjectRating",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTag_TagId",
                schema: "QoolloEDU",
                table: "ProjectTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_DeveloperId",
                schema: "QoolloEDU",
                table: "Request",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ProjectId",
                schema: "QoolloEDU",
                table: "Request",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTag_RequestId",
                schema: "QoolloEDU",
                table: "RequestTag",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Name",
                schema: "QoolloEDU",
                table: "Tag",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "DeveloperProject",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "DeveloperRating",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "DeveloperTag",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "EventRating",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "EventTag",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "Link",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "MediaContent",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "ProjectRating",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "ProjectTag",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "RequestTag",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "News",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "Request",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "Tag",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "Developer",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "Event",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "Organizer",
                schema: "QoolloEDU");

            migrationBuilder.DropTable(
                name: "BaseUser",
                schema: "QoolloEDU");
        }
    }
}
