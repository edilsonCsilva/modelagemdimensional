
  const sqlite3 = require('sqlite3').verbose();
   

  let db = new sqlite3.Database('C:\\ModelagemDimencional\\base\\dw.db', (err) => {
    if (err) {
      console.error(err.message);
    }
    console.log('Connected to the Modelagens database.');
  });



  module.exports=db