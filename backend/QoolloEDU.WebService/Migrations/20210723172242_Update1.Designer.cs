// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using QoolloEDU.Database;

namespace QoolloEDU.WebService.Migrations
{
    [DbContext(typeof(QoolloEduDbContext))]
    [Migration("20210723172242_Update1")]
    partial class Update1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("QoolloEDU")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("QoolloEDU.Database.models.BaseUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("BaseUser");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DeveloperId")
                        .HasColumnType("integer");

                    b.Property<int>("NewsId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("NewsId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Developer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Nickname")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Developer");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.DeveloperProject", b =>
                {
                    b.Property<int>("DeveloperId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("DeveloperId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("DeveloperProject");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.DeveloperRating", b =>
                {
                    b.Property<int>("DeveloperIdFrom")
                        .HasColumnType("integer");

                    b.Property<int>("DeveloperIdTo")
                        .HasColumnType("integer");

                    b.Property<int?>("DeveloperId")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("DeveloperIdFrom", "DeveloperIdTo");

                    b.HasIndex("DeveloperId");

                    b.ToTable("DeveloperRating");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.DeveloperTag", b =>
                {
                    b.Property<int>("DeveloperId")
                        .HasColumnType("integer");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.HasKey("DeveloperId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("DeveloperTag");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OrganizerId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.EventRating", b =>
                {
                    b.Property<int>("DeveloperId")
                        .HasColumnType("integer");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("DeveloperId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventRating");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.EventTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.HasKey("TagId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventTag");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BaseUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("URL")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BaseUserId");

                    b.ToTable("Link");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.MediaContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("URL")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("MediaContent");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Organizer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Organizer");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("EventId")
                        .HasColumnType("integer");

                    b.Property<string>("Markdown")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<int?>("Place")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Project");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.ProjectRating", b =>
                {
                    b.Property<int>("DeveloperId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("DeveloperId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectRating");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.ProjectTag", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.HasKey("ProjectId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ProjectTag");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DeveloperId")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.RequestTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.Property<int>("RequestId")
                        .HasColumnType("integer");

                    b.HasKey("TagId", "RequestId");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestTag");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Comment", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Developer", "Developer")
                        .WithMany("Comments")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QoolloEDU.Database.models.News", "News")
                        .WithMany("Comments")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("News");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Developer", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.BaseUser", "BaseUser")
                        .WithOne("Developer")
                        .HasForeignKey("QoolloEDU.Database.models.Developer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseUser");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.DeveloperProject", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Developer", "Developer")
                        .WithMany("DeveloperProjects")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QoolloEDU.Database.models.Project", "Project")
                        .WithMany("DeveloperProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.DeveloperRating", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Developer", "Developer")
                        .WithMany("DeveloperRatings")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Developer");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.DeveloperTag", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Developer", "Developer")
                        .WithMany("DeveloperTags")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QoolloEDU.Database.models.Tag", "Tag")
                        .WithMany("DeveloperTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Event", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Organizer", "Organizer")
                        .WithMany("Events")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.EventRating", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Developer", "Developer")
                        .WithMany("EventRatings")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QoolloEDU.Database.models.Event", "Event")
                        .WithMany("EventRatings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.EventTag", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Event", "Event")
                        .WithMany("EventTags")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QoolloEDU.Database.models.Tag", "Tag")
                        .WithMany("EventTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Link", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.BaseUser", "BaseUser")
                        .WithMany("Links")
                        .HasForeignKey("BaseUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseUser");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.MediaContent", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Project", "Project")
                        .WithMany("MediaContents")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.News", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Project", "Project")
                        .WithMany("News")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Organizer", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.BaseUser", "BaseUser")
                        .WithOne("Organizer")
                        .HasForeignKey("QoolloEDU.Database.models.Organizer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseUser");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Project", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Event", "Event")
                        .WithMany("Projects")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Event");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.ProjectRating", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Developer", "Developer")
                        .WithMany("ProjectRatings")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QoolloEDU.Database.models.Project", "Project")
                        .WithMany("ProjectRatings")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.ProjectTag", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Project", "Project")
                        .WithMany("ProjectTags")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QoolloEDU.Database.models.Tag", "Tag")
                        .WithMany("ProjectTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Request", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Developer", "Developer")
                        .WithMany("Requests")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QoolloEDU.Database.models.Project", "Project")
                        .WithMany("Requests")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.RequestTag", b =>
                {
                    b.HasOne("QoolloEDU.Database.models.Request", "Request")
                        .WithMany("RequestTags")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QoolloEDU.Database.models.Tag", "Tag")
                        .WithMany("RequestTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.BaseUser", b =>
                {
                    b.Navigation("Developer");

                    b.Navigation("Links");

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Developer", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("DeveloperProjects");

                    b.Navigation("DeveloperRatings");

                    b.Navigation("DeveloperTags");

                    b.Navigation("EventRatings");

                    b.Navigation("ProjectRatings");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Event", b =>
                {
                    b.Navigation("EventRatings");

                    b.Navigation("EventTags");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.News", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Organizer", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Project", b =>
                {
                    b.Navigation("DeveloperProjects");

                    b.Navigation("MediaContents");

                    b.Navigation("News");

                    b.Navigation("ProjectRatings");

                    b.Navigation("ProjectTags");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Request", b =>
                {
                    b.Navigation("RequestTags");
                });

            modelBuilder.Entity("QoolloEDU.Database.models.Tag", b =>
                {
                    b.Navigation("DeveloperTags");

                    b.Navigation("EventTags");

                    b.Navigation("ProjectTags");

                    b.Navigation("RequestTags");
                });
#pragma warning restore 612, 618
        }
    }
}
