using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App8
{
    public class sharedInformation
    {
        public static ParseUser SahredParseUser;
        public static ParseObject SahredParsePerson;
        public static Person SahredConnectedPerson;
        public static Person sharedSelectedPerson;
        public static Person SahredConnectedFather;
        public static Person SahredConnectedMother;
        public static String SahredTypeNewLink;
        public static String SharedLastName;
        public static List<Person> sharedListU { get; set; }
        public static List<Person> sharedListP { get; set; }
        public static List<Person> sharedListB { get; set; }
        public static List<Person> sharedListC { get; set; }
        public static List<Person> sharedListO { get; set; }
        public static Person sharedSpouse { get; set; }
        public static ParseObject SahredParseUnknownPerson;
    }
}
