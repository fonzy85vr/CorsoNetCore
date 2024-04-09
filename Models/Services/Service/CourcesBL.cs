using CorsoNetCore.Models.DataTypes;
using CorsoNetCore.Models.DataTypes.Enums;
using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.Service
{
    public class CoursesService : ICoursesService
    {
        public List<CourseViewModel> GetCourses()
        {
            var randPrice = new Random();
            var toRet = new List<CourseViewModel>();

            for(var i = 0; i < 5; i++){
                var price = Convert.ToDecimal(randPrice.NextDouble() * 10 + 10);
                toRet.Add(new CourseViewModel{
                    Author = "Nome Cognome",
                    Id = Guid.NewGuid(),
                    CurrentPrice = new Money{
                        Amount = price,
                        Currency = Currency.EUR
                    },
                    Price = new Money {
                        Amount = randPrice.NextDouble() > 0.5 ? price : price - 1,
                        Currency = Currency.EUR
                    },
                    ImagePath = "",
                    Rating = randPrice.NextDouble() *5,
                    Title = $"Corso {i}"
                });
            }

            return toRet;
        }
    }
}