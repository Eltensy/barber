using Microsoft.AspNetCore.Mvc;
public class BarberInformationController : Controller
{
    private readonly List<BarberLayered.Models.Review> _reviews;

    public BarberInformationController()
    {
        _reviews = new List<BarberLayered.Models.Review>
        {
            new BarberLayered.Models.Review { Id = 1, fk_ClientId = 1, fk_BarberId = 1, Text = "Чудовий сервіс, рекомендую!", Rating = 4.5f, Date = new DateTime(2024, 3, 20) },
            new BarberLayered.Models.Review { Id = 2, fk_ClientId = 2, fk_BarberId = 1, Text = "Дуже талановитий барбер, завжди задоволений стрижкою.", Rating = 5.0f, Date = new DateTime(2023, 3, 18) },
            new BarberLayered.Models.Review { Id = 3, fk_ClientId = 3, fk_BarberId = 1, Text = "Професіонал своєї справи, повернусь обов'язково!", Rating = 4.7f, Date = new DateTime(2024, 3, 15) },
            new BarberLayered.Models.Review { Id = 4, fk_ClientId = 1, fk_BarberId = 2, Text = "Вражений результатом, обов'язково прийду знову!", Rating = 4.8f, Date = new DateTime(2024, 3, 25) },
            new BarberLayered.Models.Review { Id = 5, fk_ClientId = 2, fk_BarberId = 2, Text = "Чудовий барбер, завжди робить все на найвищому рівні.", Rating = 4.9f, Date = new DateTime(2024, 3, 22) },
            new BarberLayered.Models.Review { Id = 6, fk_ClientId = 3, fk_BarberId = 2, Text = "Відмінний сервіс, рекомендую всім знайомим.", Rating = 5.0f, Date = new DateTime(2024, 3, 20) },
            new BarberLayered.Models.Review { Id = 7, fk_ClientId = 4, fk_BarberId = 1, Text = "Прийняли пізніше ніж мало б бути", Rating = 3.0f, Date = new DateTime(2024, 3, 10) },
            new BarberLayered.Models.Review { Id = 7, fk_ClientId = 5, fk_BarberId = 1, Text = "Результат як завжди сподобався, неодмінно ходитиму лише до Олега!", Rating = 5.0f, Date = new DateTime(2024, 4, 7) }
            
        };
    }

    public IActionResult Index()
    {
        var barber = new BarberLayered.Models.Barber
        {
            Id = 1,
            Name = "Олег",
            Surname = "Леськів",
            Phone = "0504567890",
            Email = "olegles@example.com",
            PasswordHash = "password",
            PhotoUri =
                "https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=",
            Description =
                "Досвідчений барбер з 10-річним стажем роботи. Спеціалізується в класичних та сучасних стрижках. Завжди готовий надати найкращий сервіс.",
            PortfolioUri = "portfolio/john"
        };

        var BarberReviews = _reviews.FindAll(review => review.fk_BarberId == 1);

        var barber2 = new BarberLayered.Models.Barber
        {
            Id = 2,
            Name = "Роман",
            Surname = "Мигота",
            Phone = "0987654321",
            Email = "ivanpetrov@example.com",
            PasswordHash = "password",
            PhotoUri =
                "https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=",
            Description = "Талановитий барбер з великим досвідом. Спеціалізується на стильних чоловічих зачісках.",
            PortfolioUri = "portfolio/ivan"
        };

        var barber2Reviews = _reviews.FindAll(review => review.fk_BarberId == 2);

        ViewBag.Barber = barber;
        ViewBag.Reviews = BarberReviews;
        ViewBag.Barber2 = barber2;
        ViewBag.Barber2Reviews = barber2Reviews;

        return View();
    }

    public IActionResult MyReviews()
        {
            var barberId = 1; 
            var barberReviews = _reviews.FindAll(review => review.fk_BarberId == barberId);

            return View(barberReviews);
        }

    
}
