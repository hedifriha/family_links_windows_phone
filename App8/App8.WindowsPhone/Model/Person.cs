using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace App8
{
    public class Person
    {
        public String belongs { get; set; }
        public String relation { get; set; }
        public ParseUser User { get; set; }
        public String personId { get; set; }
        public BitmapImage Imagel { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String Dob { get; set; }
        public String Job { get; set; }
        public String Country { get; set; }
        public String Gender { get; set; }
        public ParseObject Father { get; set; }
        public ParseObject Mother { get; set; }
        public ParseObject Spouse { get; set; }
        public String FatherId { get; set; }
        public String MotherId { get; set; }
        public String SpouseId { get; set; }
        public Person()
        {

        }
        public Person(String name, BitmapImage img)
        {
            this.Name = name;
            this.Imagel = img;
        }
        public Person(String personId,String Name, String LastName,BitmapImage img)
       {
            
            this.Name = Name;
            this.LastName = LastName;
            this.Imagel = img;
         
    }
    }
}