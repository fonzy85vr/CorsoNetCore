using CorsoNetCore.Models.DataTypes;
using CorsoNetCore.Models.DataTypes.Enums;
using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.Repository
{
    public class CoursesRepositoryMockup : ICoursesRepository
    {
        public IEnumerable<CourseViewModel> GetCourses(){
            var randPrice = new Random();
            
            for(var i = 0; i < 5; i++){
                var price = Convert.ToDecimal(randPrice.NextDouble() * 10 + 10);
               yield return new CourseViewModel{
                    Author = "Nome Cognome",
                    Id = 1,
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
                };
            }
        }
    }
}