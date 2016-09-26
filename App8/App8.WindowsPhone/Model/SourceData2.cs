using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace App8.Model
{
 public   class SourceData2
    {
        public string belongs { get; set; }

        public string Rela { get; set; }

       
        public string LastName { get; set; }
        public BitmapImage image { get; set; }

        public static IEnumerable<SourceData2> GetData()
        {
          /*  var handMadeSpouse = new[]
                 {
                     new SourceData2 { belongs = "Spouse",
                Rela = sharedInformation.sharedSpouse.personId,
               
                LastName = sharedInformation.sharedSpouse.Name+" "+sharedInformation.sharedSpouse.LastName,
                image = sharedInformation.sharedSpouse.Imagel, },
                 };

            var handMadeParent =
            from i in sharedInformation.sharedListP
            select new SourceData2
            {
                belongs = "Parent",
                Rela = i.personId,
                
                LastName = i.Name + " " + i.LastName,
                image = i.Imagel,
            };
            var handMadeOther =
           from i in sharedInformation.sharedListO
           select new SourceData2
           {
               belongs = "Other",
               Rela = i.personId,
             
               LastName = i.Gender,
               image = i.Imagel,
           };*/
          
            var handMadeBrother =
          from i in sharedInformation.sharedListB
          select new SourceData2
          {
              belongs = "Brother",
              Rela = i.personId,
             
              LastName = i.Name,
              image = i.Imagel,
          };

            //    return handMadeSpouse.Concat(handMadeParent).Concat(handMadeBrother).Concat(handMadeChildren);
            return handMadeBrother;
        }
        public static IEnumerable<SourceData2> GetData2()
        {
           
           
            var handMadeChildren =
          from i in sharedInformation.sharedListC
          select new SourceData2
          {
              belongs = "Children",
              Rela = i.personId,

              LastName = i.Name,
              image = i.Imagel,
          };
           

            //    return handMadeSpouse.Concat(handMadeParent).Concat(handMadeBrother).Concat(handMadeChildren);
            return handMadeChildren;
        }
        public static IEnumerable<SourceData2> GetDataP()
        {
          

              var handMadeParent =
              from i in sharedInformation.sharedListP
              select new SourceData2
              {
                  belongs = "Parent",
                  Rela = i.personId,

                  LastName = i.Name ,
                  image = i.Imagel,
              };
             

           

              return handMadeParent;
         
        }
          public static IEnumerable<SourceData2> GetDataS()
          {


              var handMadeSpouse = new[]
                    {
                       new SourceData2 { belongs = "Spouse",
                  Rela = sharedInformation.sharedSpouse.personId,

                  LastName = sharedInformation.sharedSpouse.Name+" "+sharedInformation.sharedSpouse.LastName,
                  image = sharedInformation.sharedSpouse.Imagel, },
                   };


              //    return handMadeSpouse.Concat(handMadeParent).Concat(handMadeBrother).Concat(handMadeChildren);
              return handMadeSpouse;
          }
    }
}