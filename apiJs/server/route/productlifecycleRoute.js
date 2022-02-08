const express = require('express')
const { productlifeBy } = require('../data/productlifecycleData')
const router = express.Router()
const productlifecycleService = require("./../service/productlifecycleService")


router.get('/productlifecycle/:id', async function (req, res) {
    // console.log(req)
    try {
        const productionLifes = await productlifecycleService.productlifeBy((req.params.id))
        res.status(200).json(productionLifes)
    } catch (e) {
        res.status(500).json({ error: e.message })
    }
})

router.get('/productlifecycleuuid/:uuid', async function (req, res) {
    // console.log(req)
    try {
        const productionLifes = await productlifecycleService.productlifeUuid((req.params.uuid))
        res.status(200).json(productionLifes)
    } catch (e) {
        res.status(500).json({ error: e.message })
    }
})

router.get('/productlifecycleyear/:year', async function (req, res) {
    try {
        const productionLifes = await productlifecycleService.productlifeAllYear((req.params.year))
        res.status(200).json(productionLifes)
    } catch (e) {
        res.status(500).json({ error: e.message })
    }
})


router.get('/productlifecycleyearmonthorderquatitysolid/:year/:month/:order?', async function (req, res) {
    try {
        var order = (req.params.order == undefined) ? 'quality' : req.params.order
        const productionLifes = await productlifecycleService.
            productlifeYearMonthOrderQualitySold(req.params.year, req.params.month, order)
        res.status(200).json(productionLifes)
    } catch (e) {
        res.status(500).json({ error: e.message })
    }
})


router.get('/productlifecycleyearmonthlysales/:year', async function (req, res) {
    try {

        const productionLifes = await productlifecycleService.summaryMonthlySales(req.params.year)
        res.status(200).json(productionLifes)
    } catch (e) {
        res.status(500).json({ error: e.message })
    }
})


router.get('/productlifecycleyearysales8020/:year', async function (req, res) {
    try {

        const productionLifes = await productlifecycleService.accumulatedProductsMonthlySales(req.params.year)
        res.status(200).json(productionLifes)
    } catch (e) {
        res.status(500).json({ error: e.message })
    }
})




router.get('/productlifeYearMonthOrderQualitySoldHours/:year/:month/:day/:hours/:order?', async function (req, res) {
    try {
        var order = (req.params.order == undefined) ? 'quality' : req.params.order


        const productionLifes = await productlifecycleService.
            productlifeYearMonthOrderQualitySoldHours(
                req.params.year,
                req.params.month,
                req.params.hours,
                req.params.day,
                order)



        res.status(200).json(productionLifes)
    } catch (e) {
        res.status(500).json({ error: e.message })
    }
})



router.get('/salesProductsYears/:year?', async function (req, res) {
    try {

        var year=(req.params.year==undefined || req.params.year==-1) ? null : req.params.year
       
        const productionLifes = await productlifecycleService.salesProductsYears(year)
        res.status(200).json(productionLifes)
    } catch (e) {
        res.status(500).json({ error: e.message })
    }
})


router.get('/dimTimeAllYears', async function (req, res) {
    try {

     
       
        const productionLifes = await productlifecycleService.dimTimeAllYears()
        res.status(200).json(productionLifes)
    } catch (e) {
        res.status(500).json({ error: e.message })
    }
})
 




module.exports = router