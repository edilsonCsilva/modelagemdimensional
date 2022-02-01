using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractorCore
{
    public class Class1
    {

       public Class1()
        {


            string data = "dw.db", dirData= @"C:\ModelagemDimencional\base";
            SQliteBaseConection contex = new SQliteBaseConection(dirData, data);
            if (contex.hasConnection())
            {
                Console.WriteLine("Ativa");

                DimTime dimTimeDao = new DimTime(contex);
                DimCategory dimCategory = new DimCategory(contex);

                FileControl fc = new FileControl(contex);

                //  fc.Add("12312s.scv");

               //Console.WriteLine(fc.BySkNameFile("12312s.scv"));

              //  fc.UpdateStatusProcess(fc.BySkNameFile("12312s.scv"),2);





                //dimCategory.Add("TEste 2022");

                //  List<Entity.DimCategoryEntity> dimCategoryEntities = dimCategory.All();

                // dimTimeDao.Add(30, 1, 2022, 9, 33);
                // Console.WriteLine(dimTimeDao.BySk(30, 1, 2022, 9, 33));

                //List<Entity.DimTimeEntity> dimTimeEntities = dimTimeDao.By(30, 1, 2022, 9, 33);

                //           List<Entity.DimTimeEntity> dimTimeEntitiess = dimTimeDao.All();


                Console.WriteLine("d");


            }



        }
        
    }
}
