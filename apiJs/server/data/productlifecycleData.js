

exports.productlifeBy = async function (id) {
    try {
        const SQLITE =require("../infra/async")
        var cycleProducts = []
        sql = `
       
        select t.year ,ct.descricao as departament,fs.unit_price_sales as subtotal, count(*) as qt,
        sum(fs.unit_price_sales * fs.quantity_of_items) as sales,
        sum(fs.discount) as discont,g.latitude,g.longitude from  dim_category  ct join 
        ft_sales fs on ct.sk_category=fs.sk_tf_category
        join dim_time t on t.sk_time=fs.sk_tf_sales_time 
        join dim_geo_sales g on fs.sk_ft_geo_sales=g.sk_geosales
        where fs.sk_ft_product=`+ id + `
        group by t.year,ct.descricao,fs.unit_price_sales order by t.year, qt desc
    `
 
        var rows = await SQLITE.db_all(sql)
        var products = {
            "sales_amount": { "sales_amount": 0, "discont_amount": 0 },
            "lifeCycles": []
        }

        for (interacting = 0; interacting < rows.length; interacting++) {
            products.sales_amount.sales_amount += rows[interacting].sales
            products.sales_amount.discont_amount += rows[interacting].discont

            rows[interacting].latitude = parseFloat(rows[interacting].latitude.replace("'", ''))
            rows[interacting].longitude = parseFloat(rows[interacting].longitude.replace("'", ''))
            cycleProducts.push(rows[interacting])
        }
        products.lifeCycles = cycleProducts
        return products

    } catch (e) {
        throw new Error(e.message);
    }
}



exports.productlifeAllYear = async function (year) {
    try {
        const SQLITE =require("../infra/async")
        var cycleProducts = []
        sql = `
       
        select t.year,t.month ,ct.descricao as departament,fs.unit_price_sales as subtotal, count(*) as qt,
        sum(fs.unit_price_sales * fs.quantity_of_items) as sales,
        sum(fs.discount) as discont,g.latitude,g.longitude from  dim_category  ct join 
        ft_sales fs on ct.sk_category=fs.sk_tf_category
        join dim_time t on t.sk_time=fs.sk_tf_sales_time 
        join dim_geo_sales g on fs.sk_ft_geo_sales=g.sk_geosales
        where t.year=`+ year + `
        group by t.year,t.month,ct.descricao,fs.unit_price_sales  order by t.year, qt desc
    `
        var rows = await SQLITE.db_all(sql)
        for (interacting = 0; interacting < rows.length; interacting++) {
            rows[interacting].latitude = parseFloat(rows[interacting].latitude.replace("'", ''))
            rows[interacting].longitude = parseFloat(rows[interacting].longitude.replace("'", ''))
            cycleProducts.push(rows[interacting])
        }

        return cycleProducts
    } catch (e) {

        throw new Error(e.message);
    }
}



exports.productlifeYearMonthOrderQualitySold=async function(year,month,order){
    try {
        const SQLITE =require("../infra/async")
        var cycleProducts = []
        var orderDefined=" order by fs.quantity_of_items desc"
        if(order=="price"){
            orderDefined=' order by fs.unit_price_sales desc'
        }
        sql = `
            select p.sk_product,p.id as uuid,p.descricao as description,p.photo_path,ct.descricao as departement,
            fs.unit_price_sales as sale_price,fs.quantity_of_items as quality_sold,fs.discount
            from ft_sales fs join dim_time t on t.sk_time=fs.sk_tf_sales_time
            join dim_product p on fs.sk_ft_product=p.sk_product
            join dim_category ct on fs.sk_tf_category=ct.sk_category
            where t.year=`+year+` and t.month=`+month+`  `+orderDefined+`
        `
        var rows = await SQLITE.db_all(sql)
        for (interacting = 0; interacting < rows.length; interacting++) {
            cycleProducts.push(rows[interacting])
        }

        return cycleProducts
    } catch (e) {

        throw new Error(e.message);
    }
}




exports.summaryMonthlySales=async function(year){
    try {
        const SQLITE =require("../infra/async")
        var cycleProducts = []
       
        sql = `
                select  t.month,
                sum(fs.quantity_of_items) as qualityYear,
                sum(fs.unit_price_sales * fs.quantity_of_items) as salesYear,
                sum(fs.discount) as discontYear
                from ft_sales fs
                join dim_time t on t.sk_time=fs.sk_tf_sales_time
                where t.year=`+year+`   group by t.month

        `
        var rows = await SQLITE.db_all(sql)
        for (interacting = 0; interacting < rows.length; interacting++) {
            cycleProducts.push(rows[interacting])
        }

        return cycleProducts
    } catch (e) {

        throw new Error(e.message);
    }
}




exports.accumulatedProductsMonthlySales=async function(year){
    try {
        const SQLITE =require("../infra/async")
        var cycleProducts = []
       
        sql = `
                select p.sk_product,p.id as uuid,p.descricao as description,p.photo_path,
                sum(fs.unit_price_sales*fs.quantity_of_items) as sales
                from ft_sales fs
                join dim_time t on t.sk_time=fs.sk_tf_sales_time
                join dim_product p on fs.sk_ft_product=p.sk_product
                where t.year=`+year+`    group by  p.sk_product,p.id ,p.descricao
                order by sales desc
        `
        var rows = await SQLITE.db_all(sql)
        for (interacting = 0; interacting < rows.length; interacting++) {
            cycleProducts.push(rows[interacting])
        }

        return cycleProducts
    } catch (e) {

        throw new Error(e.message);
    }
}
