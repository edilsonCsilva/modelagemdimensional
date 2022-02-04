async function db_all(query) {
    const database = require('../infra/database')
    return new Promise(function (resolve, reject) {
        database.all(query, function (err, rows) {
            if (err) { return reject(err); }
            resolve(rows);
        });
    });
}


exports.productlifeBy = async function (id) {
    var cycleProducts = []
    sql = `
       
        select t.year ,ct.descricao,fs.unit_price_sales as subtotal, count(*) as qt,
        sum(fs.unit_price_sales * fs.quantity_of_items) as sales,
        sum(fs.discount) as discont,g.latitude,g.longitude from  dim_category  ct join 
        ft_sales fs on ct.sk_category=fs.sk_tf_category
        join dim_time t on t.sk_time=fs.sk_tf_sales_time 
        join dim_geo_sales g on fs.sk_ft_geo_sales=g.sk_geosales
        where fs.sk_ft_product=`+id+`
        group by t.year,ct.descricao,fs.unit_price_sales order by t.year, qt desc
    `
    var rows = await db_all(sql)
    var products = {
        "sales_amount": { "sales_amount": 0, "discont_amount": 0 },
        "lifeCycles": []
    }

    for (interacting = 0; interacting < rows.length; interacting++) {
        products.sales_amount.sales_amount += rows[interacting].sales
        products.sales_amount.discont_amount += rows[interacting].discont

        rows[interacting].latitude=parseFloat(rows[interacting].latitude.replace("'",''))
        rows[interacting].longitude=parseFloat(rows[interacting].longitude.replace("'",''))
        cycleProducts.push(rows[interacting])
    }
    products.lifeCycles = cycleProducts
    return products
}



exports.productlifeAllYear = async function (year) {
    var cycleProducts = []
    sql = `
       
        select t.year,t.month ,ct.descricao,fs.unit_price_sales as subtotal, count(*) as qt,
        sum(fs.unit_price_sales * fs.quantity_of_items) as sales,
        sum(fs.discount) as discont,g.latitude,g.longitude from  dim_category  ct join 
        ft_sales fs on ct.sk_category=fs.sk_tf_category
        join dim_time t on t.sk_time=fs.sk_tf_sales_time 
        join dim_geo_sales g on fs.sk_ft_geo_sales=g.sk_geosales
        where t.year=`+year+`
        group by t.year,t.month,ct.descricao,fs.unit_price_sales  order by t.year, qt desc
    `
    var rows = await db_all(sql)
    for (interacting = 0; interacting < rows.length; interacting++) {
        rows[interacting].latitude=parseFloat(rows[interacting].latitude.replace("'",''))
        rows[interacting].longitude=parseFloat(rows[interacting].longitude.replace("'",''))
        cycleProducts.push(rows[interacting])
    }
 
    return cycleProducts
}

