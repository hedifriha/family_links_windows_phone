using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace App8
{
    public class SourceData
    {
        private static IEnumerable<SourceData> handMadeSpouse;

        public string belongs { get; set; }

        public string Rela { get; set; }

        public string Address { get; set; }
        public string LastName { get; set; }
        public BitmapImage image { get; set; }

        public static IEnumerable<SourceData> GetData()
        {
            if (!sharedInformation.sharedSpouse.personId.Equals("RyQlY5U6UM")) {
                handMadeSpouse = new[]
                {
                     new SourceData { belongs = "Spouse",
                Rela = sharedInformation.sharedSpouse.personId,
                Address = sharedInformation.sharedSpouse.Name+" "+sharedInformation.sharedSpouse.LastName,
                LastName = sharedInformation.sharedSpouse.Gender,
                image = sharedInformation.sharedSpouse.Imagel, },
                 };
            }
            


            var handMadeParent =
            from i in sharedInformation.sharedListP
            select new SourceData
            {
                belongs = "Parent",
                Rela = i.personId,
                Address = i.Name + " " + i.LastName,                
                LastName = i.Gender,
                image = i.Imagel,
            };
            var handMadeOther =
           from i in sharedInformation.sharedListO
           select new SourceData
           {
               belongs = "Other",
               Rela = i.personId,
               Address = i.Name + " " + i.LastName,
               LastName = i.Gender,
               image = i.Imagel,
           };
            var handMadeChildren =
          from i in sharedInformation.sharedListC
          select new SourceData
          {
              belongs = "Children",
              Rela = i.personId,
              Address = i.Name + " " + i.LastName,
              LastName = i.Gender,
              image = i.Imagel,
          };
            var handMadeBrother =
          from i in sharedInformation.sharedListB
          select new SourceData
          {
              belongs = "Brother",
              Rela = i.personId,
              Address = i.Name + " " + i.LastName,
              LastName = i.Gender,
              image = i.Imagel,
          };
            var xxx = handMadeBrother;
            try { xxx.Concat(handMadeSpouse); } catch { };
            return xxx.Concat(handMadeParent).Concat(handMadeChildren);

        }
    }
}