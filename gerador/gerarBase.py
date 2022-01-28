from code import interact
import json
from operator import ge
from random import randint
import random
from uuid import uuid4
import uuid
 
import Lib
import DaoProducts
from datetime import date
import csv
import os


 
def loadGeo():
    CURR_DIR = os.path.dirname(os.path.realpath(__file__))
    geoPositions=[]
    with open(CURR_DIR+"\geo.csv",'r',encoding="utf8") as f:
        file_csv=csv.reader(f,delimiter=",")
        for i,linha in enumerate(file_csv):
            if i !=0:
                geoPositions.append(str(linha))
    

    return geoPositions



 

def generationSalesYear(year,data_atual,address):
     Sales=[]

     #data_atual.day, data_atual.month,data_atual.year

     yearActive=int(data_atual.year)
     monthActive=int(data_atual.month)
     rowsAddress=len(address)


     if year!=yearActive :
         monthActive=12


     for salesMonth in range(monthActive):
         month=salesMonth+1
         for salesDay in range(29):
             day=salesDay+1
             if(salesDay == 1):
                 break

                
             amountSales=randint(1,6)
             salesAmount=""
             for salesAmountDay in range(amountSales):
                 amountItens=randint(1,10)
                 uuid_= str(uuid.uuid4()).replace("-","")
                 
                 timeSales=Lib.random_date("1/1/1998 1:30 PM", "1/1/2022 12:50 AM", random.random()).split(" ")
                 timeSales=timeSales[1]
                 geo=address[randint(0,rowsAddress-1)]

                 for itens in range(amountItens):
                     amountItensSales=randint(1,10)
                     product=DaoProducts.products[randint(0,len(DaoProducts.products)-1)]
                     SalesCsv=str(uuid_)+";"+str(day)+"-"+str(month)+"-"+str(year)+" "+timeSales+";"+product.description+";"+product.key+";"+product.category+";"+product.photo+";"+str(product.price)+";"+product.note+";"+str(amountItensSales)+";"+str(randint(0,25))+";"+str(geo)
                     salesAmount=salesAmount+SalesCsv+"\n"
                     
                     Sales.append({
                         "uuid":uuid_,"created_at":str(day)+"-"+str(month)+"-"+str(year)+" "+timeSales,
                         "product":product.description,"product_key":product.key,
                         "category":product.category,"photo":product.photo,
                         "price":product.price,"note":product.note,"amount":amountItensSales,
                         "discount":str(randint(0,25)),
                         "geo":geo
                     })

             print("")
             with open("data\\base_"+str(next(Lib.uniqueid()))+".csv","a") as f:
                     f.write(salesAmount)
     



     ##with open("data\\base_"+str(year)+".json","a") as f:
   #      f.write(json.dumps(Sales))

     #with open("data\\base_"+str(year)+".csv","a") as f:
    #    f.write(salesAmount)
         



                    


             
            
geoMaps=loadGeo()
year=2022
while year !=2023:
    generationSalesYear(year,date.today(),geoMaps )
    year=year+1
         