const express = require('express')
const router = express.Router()
const db = require('./../infra/database')






router.get('/A', async function (req, res) {

//const sqlite3 = require('sqlite3').verbose();
 // const sqlite3 = require('sqlite3').verbose();
 // const db = new sqlite3.Database('./data/dw.db');

 try{
  //const db = new sqlite3.Database('C:\\ModelagemDimencional\\base\\dw.db');
  db.serialize(function () {

      var sql=`
      
      select t.year ,ct.descricao, count(*) as qt from  dim_category  ct join 
      ft_sales fs on ct.sk_category=fs.sk_tf_category
      join dim_time t on t.sk_time=fs.sk_tf_sales_time
      group by t.year,ct.descricao order by t.year, qt desc
          
      
      
      
      `

      db.all(sql, function (err, table) {
          console.log(err,table);
          res.json([
            {
              status: "ok",
              data:table
            }
          ])

      });
     // db.close()
  });



 }catch(e){}
  


/*
  const sqlite3 = require('sqlite3').verbose();
  const db = new sqlite3.Database('./data/dw.db');
  db.serialize(function () {
      db.all("SELECT sk_category,descricao FROM dim_category;", function (err, table) {
          console.log(err,table);
          res.json([
            {
              status: "ok",
              data:table
            }
          ])

      });
      db.close()
  });

*/

 
})


module.exports = router

