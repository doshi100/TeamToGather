using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserInfo
    {
        /// <summary>
        /// the start of the properties
        /// </summary>

        public int ID { get; set; } // ID of the user
        public string UserName { get; set; } // the User Name
        public string Password { get; set; } // the User Password(could be hashed, not sure at this time)
        public string Email { get; set; } // the User's Email
        public DateTime Birthday { get; set; } // The User Date of Birth
        public string FirstName { get; set; } // the User first name
        public string LastName { get; set; } // the user last name
        public int NativeLang { get; set; } // the native language of the user, used later in filtering
        public int Country { get; set; } // the country the user lives in, used later in filtering
        public int Profession { get; set; } // The profession of the user, such as: Animator, Write, Musician and etc', the ID's are in the table 'Professions'
        public int WeeklyFreeTime { get; set; } // the number of free time the user has to work on collabrative projects in a week.
        public int NumRateVoters { get; set; } // the number of users who voted if the user is a beginner or etc' used in average calculations later on.
        public int UserRate { get; set; } // the user's Rate, this Rate is incremented(by 1-5 according to the rater) every time someone rates the user, there is no table for Levels, this is written by code.
        public bool IsBanned { get; set; } // determines if the user is banned from the site by an administrator, if the user is banned he can't enter the site
        public int ProjectSum { get; set; } // determines the number of projects the user is working on(ACCORDING TO THE SITE, NOT THE USER)
        public DateTime RegistrationDate { get; set; } // the date in which the user signed up on.
        public DateTime LoginDate { get; set; } // saves the last date the user was logged in on, this should be changed in the table, only if the date has changed
        public int UserType { get; set; } // the user Type: 1. Admin 2. Regular, user types are available on the UserType Table in the database

        /// The end of the properties.

        

    }
}
