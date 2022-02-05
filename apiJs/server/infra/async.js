/*Criada para gerar o Async pis o sqlite no possui e os retorno sao em callBacks*/

async function db_all(query) {
    const database = require('../infra/database')
    return new Promise(function (resolve, reject) {
        database.all(query, function (err, rows) {
            if (err) { return reject(err); }
            resolve(rows);
        });
    });
}



exports.db_all=db_all