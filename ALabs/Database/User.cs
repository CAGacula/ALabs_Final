using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ALabs.Database
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // User Score
        public int userScore { get; set; }
        


        // Lesson 1
        public int lesson1progress { get; set; }
        public int stateScore { get; set; }
        public int transitionScore {  get; set; }
        public int inputScore { get; set; }
        public int introQuiz { get; set; }
        public int challengeprogress { get; set; }



        public int lesson2progress { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

       
    }
}