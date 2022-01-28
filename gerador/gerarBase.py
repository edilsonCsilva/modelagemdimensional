from code import interact
import json
from random import randint
import random
import Lib
import DaoProducts
from datetime import date

 


 

def generationSalesYear(year,data_atual ):
     Sales=[]

     #data_atual.day, data_atual.month,data_atual.year

     yearActive=int(data_atual.year)
     monthActive=int(data_atual.month)

     if year!=yearActive :
         monthActive=12


     for salesMonth in range(monthActive):
         month=salesMonth+1
         for salesDay in range(29):
             day=salesDay+1
             amountSales=randint(1,6)
             salesAmount=""
             for salesAmountDay in range(amountSales):
                 amountItens=randint(1,10)
                 uuid=Lib.uniqueid()
                 uuid=next(uuid)
                 timeSales=Lib.random_date("1/1/1998 1:30 PM", "1/1/2022 12:50 AM", random.random()).split(" ")
                 timeSales=timeSales[1]

                 for itens in range(amountItens):
                     amountItensSales=randint(1,10)
                     product=DaoProducts.products[randint(0,len(DaoProducts.products)-1)]
                     SalesCsv=str(uuid)+";"+str(day)+"-"+str(month)+"-"+str(year)+" "+timeSales+";"+product.description+";"+product.key+";"+product.category+";"+product.photo+";"+str(product.price)+";"+product.note+";"+str(amountItensSales)+";"+str(randint(0,25))
                     salesAmount=salesAmount+SalesCsv+"\n"
                     
                     Sales.append({
                         "uuid":uuid,"created_at":str(day)+"-"+str(month)+"-"+str(year)+" "+timeSales,
                         "product":product.description,"product_key":product.key,
                         "category":product.category,"photo":product.photo,
                         "price":product.price,"note":product.note,"amount":amountItensSales,
                         "discount":str(randint(0,25))
                     })

             print("")
             with open("data\\base_"+str(next(Lib.uniqueid()))+".csv","a") as f:
                     f.write(salesAmount)
     



     ##with open("data\\base_"+str(year)+".json","a") as f:
   #      f.write(json.dumps(Sales))

     #with open("data\\base_"+str(year)+".csv","a") as f:
    #    f.write(salesAmount)
         



                    


             
            
          
year=2022
while year !=2023:
    generationSalesYear(year,date.today() )
    year=year+1
         