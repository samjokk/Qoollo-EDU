using System;
using System.Threading.Tasks;
using QoolloEDU.Database.models;
using QoolloEDU.Database.models.Enums;

namespace QoolloEDU.Database.Repositories.GeneratorRepository
{
    public class GeneratorRepository: IGeneratorRepository
    {
        private readonly QoolloEduDbContext _context;

        public GeneratorRepository(QoolloEduDbContext context)
        {
            _context = context;
        }

        public async Task GenerateDataAsync()
        {
            await GenerateTagsAsync();
            await GenerateEntityAsync();
            await _context.SaveChangesAsync();
        }

        public async Task ClearAllDataAsync()
        {
            foreach (var baseUser in _context.BaseUser) _context.BaseUser.Remove(baseUser);
            foreach (var comment in _context.Comment) _context.Comment.Remove(comment);
            foreach (var developer in _context.Developer) _context.Developer.Remove(developer);
            foreach (var developerProject in _context.DeveloperProject) _context.DeveloperProject.Remove(developerProject);
            foreach (var developerRating in _context.DeveloperRating) _context.DeveloperRating.Remove(developerRating);
            foreach (var developerTag in _context.DeveloperTag) _context.DeveloperTag.Remove(developerTag);
            foreach (var eEvent in _context.Event) _context.Event.Remove(eEvent);
            foreach (var eventRating in _context.EventRating) _context.EventRating.Remove(eventRating);
            foreach (var eventTag in _context.EventTag) _context.EventTag.Remove(eventTag);
            foreach (var link in _context.Link) _context.Link.Remove(link);
            foreach (var mediaContent in _context.MediaContent) _context.MediaContent.Remove(mediaContent);
            foreach (var news in _context.News) _context.News.Remove(news);
            foreach (var organizer in _context.Organizer) _context.Organizer.Remove(organizer);
            foreach (var project in _context.Project) _context.Project.Remove(project);
            foreach (var projectRating in _context.ProjectRating) _context.ProjectRating.Remove(projectRating);
            foreach (var projectTag in _context.ProjectTag) _context.ProjectTag.Remove(projectTag);
            foreach (var request in _context.Request) _context.Request.Remove(request);
            foreach (var requestTag in _context.RequestTag) _context.RequestTag.Remove(requestTag);
            foreach (var tag in _context.Tag) _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();
        }

        private async Task GenerateEntityAsync()
        {
            //baseUser
            var bu1 = await _context.AddAsync(new BaseUser()
            {
                Email = "drus@mail.ru",
                Hash = "123Aboba",
                Salt = "123",
                About = "Бэкенд-разработчик проходящий практику в Qoollo",
                Photo = "dogPhoto",
            });
            var bu2 = await _context.AddAsync(new BaseUser()
            {
                Email = "nikita@yandex.ru",
                Hash = "saltpassword",
                Salt = "salt",
                About = "Бэкенд-разработчик проходящий практику в Qoollo",
                Photo = "myPhoto",
            });
            var bu3 = await _context.AddAsync(new BaseUser()
            {
                Email = "artem@mail.ru",
                Hash = "notartem",
                Salt = "not",
                About = "Фронтенд-разработчик проходящий практику в Qoollo",
                Photo = "artemPhoto",
            });
            var bu4 = await _context.AddAsync(new BaseUser()
            {
                Email = "chris@mail.ru",
                Hash = "saltchris",
                Salt = "salt",
                About = "Крутой-разработчик проходящий практику в Qoollo",
                Photo = "chrisPhoto",
            });
            var bu = await _context.AddAsync(new BaseUser()
            {
                Email = "qoollo@qoollo.ru",
                Hash = "companyqoollo",
                Salt = "company",
                About = "Компания по разработке передовых решений",
                Photo = "qoolloPhoto",
            });
            await _context.SaveChangesAsync();
            
            //developer
            var d1 = await _context.AddAsync(new Developer()
            {
                UserId = bu1.Entity.Id,
                Name = "Андрей",
                Surname = "Чахлый",
                BirthDate = new DateTime(2000, 9, 7),
                Nickname = "psapsa"
            });
            var d2 = await _context.AddAsync(new Developer()
            {
                UserId = bu2.Entity.Id,
                Name = "Никита",
                Surname = "Гарасев",
                BirthDate = new DateTime(2000, 9, 12),
                Nickname = "stackah"
            });
            var d3 = await _context.AddAsync(new Developer()
            {
                UserId = bu3.Entity.Id,
                Name = "Артем",
                Surname = "Самойленко",
                BirthDate = new DateTime(2001, 1, 1),
                Nickname = "samjokk"
            });
            var d4 = await _context.AddAsync(new Developer()
            {
                UserId = bu4.Entity.Id,
                Name = "Кристина",
                Surname = "Ловцова",
                BirthDate = new DateTime(1999, 5, 9),
                Nickname = "chriss"
            });
            var org = await _context.AddAsync(new Organizer()
            {
                UserId = bu.Entity.Id,
                Name = "ООО Qoollo",
                Description = "Крупная it-компания",
            });
            await _context.SaveChangesAsync();
            
            //event
            var e = await _context.AddAsync( new Event()
            {
                OrganizerId = org.Entity.Id,
                Name = "QoolloSummerPractice",
                Description = "Летняя практика для студентов",
                StartDate = new DateTime(2021, 7, 1),
                EndDate = new DateTime(2021, 7, 28),
            });
            await _context.SaveChangesAsync();
            
            //project 1
            var p1 = await _context.AddAsync(new Project()
            {
                OwnerId = d2.Entity.Id,
                CreationDate = new DateTime(2021, 7, 3),
                Description = "Проект про хакатоны и турниры",
                Name = "QoolloEDU",
                EventId = e.Entity.Id,
                Markdown = "**Проект** про __хакатоны__ и турниры",
                Place = PlaceType.First,
            });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new DeveloperProject()
            {
                DeveloperId = d1.Entity.Id,
                ProjectId = p1.Entity.Id,
            });
            await _context.AddAsync(new DeveloperProject()
            {
                DeveloperId = d2.Entity.Id,
                ProjectId = p1.Entity.Id,
            });
            await _context.AddAsync(new DeveloperProject()
            {
                DeveloperId = d3.Entity.Id,
                ProjectId = p1.Entity.Id,
            });
            
            //project 2
            var p2 = await _context.AddAsync(new Project()
            {
                OwnerId = d4.Entity.Id,
                CreationDate = new DateTime(2021, 7, 4),
                Description = "Проект про комнаты и общение ",
                Name = "Meeting room booking",
                EventId = e.Entity.Id,
                Markdown = "**крутой** проект про __комнаты__",
                Place = PlaceType.Second,
            });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new DeveloperProject()
            {
                DeveloperId = d4.Entity.Id,
                ProjectId = p2.Entity.Id,
            });
            
            //project 3
            var p3 = await _context.AddAsync(new Project()
            {
                OwnerId = d1.Entity.Id,
                CreationDate = new DateTime(2021, 7, 3),
                Description = "Музыкальный голосовой помощник",
                Name = "PSAPSAbot",
                Markdown = "Плей **ауф** __мьюзик__",
            });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new DeveloperProject()
            {
                DeveloperId = d1.Entity.Id,
                ProjectId = p3.Entity.Id,
            });
            
            //news for project 1
            await _context.AddAsync( new News()
            {
                ProjectId = p1.Entity.Id,
                Date = new DateTime(2021, 7,16),
                Name = "Новость 1",
                Description = "Крутая новость",
            });
            await _context.AddAsync( new News()
            {
                ProjectId = p1.Entity.Id,
                Date = new DateTime(2021, 7,17),
                Name = "Новость 2",
                Description = "Плохая новость",
            });
            await _context.AddAsync( new News()
            {
                ProjectId = p1.Entity.Id,
                Date = new DateTime(2021, 7,18),
                Name = "Новость 3",
                Description = "Неплохая новость",
            });
            
            //save
            await _context.SaveChangesAsync();
        }

        private async Task GenerateTagsAsync()
        {
            await _context.AddAsync(new Tag() {Name = "Backend", Color = "#BB0000"});
            await _context.AddAsync(new Tag() {Name = "Frontend", Color = "#A100BB"});
            await _context.AddAsync(new Tag() {Name = "Python", Color = "#7E8100"});
            await _context.AddAsync(new Tag() {Name = "JavaScript", Color = "#0B5000"});
            await _context.AddAsync(new Tag() {Name = "Figma", Color = "#3C0088"});
            await _context.AddAsync(new Tag() {Name = "PostgreSQL", Color = "#009BA5"});
            await _context.AddAsync(new Tag() {Name = "SQL", Color = "#5FAB4C"});
            await _context.AddAsync(new Tag() {Name = "Java", Color = "#FF9900"});
            await _context.AddAsync(new Tag() {Name = "Android", Color = "#04D8B2"});
            await _context.AddAsync(new Tag() {Name = "iOS", Color = "#C38765"});
            await _context.AddAsync(new Tag() {Name = "React", Color = "#F805D1"});
            await _context.AddAsync(new Tag() {Name = "Vue", Color = "#DF9CFF"});
            await _context.AddAsync(new Tag() {Name = "PHP", Color = "#FF7575"});
            await _context.AddAsync(new Tag() {Name = "Angular", Color = "#FF5592"});
            await _context.AddAsync(new Tag() {Name = "ASP.NET", Color = "#FFE074"});
            await _context.AddAsync(new Tag() {Name = "AI", Color = "#4770FF"});
            await _context.AddAsync(new Tag() {Name = "ML", Color = "#A8FF1A"});
            await _context.AddAsync(new Tag() {Name = "MongoDB", Color = "#0C3436"});
            await _context.AddAsync(new Tag() {Name = "C#", Color = "#D8A800"});
            await _context.AddAsync(new Tag() {Name = "C++", Color = "#00D856"});
            
            await _context.SaveChangesAsync();
        }
    }
}