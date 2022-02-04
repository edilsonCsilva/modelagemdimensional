const express = require('express')
const { productlifeBy } = require('../data/productlifecycleData')
const router = express.Router()
const productlifecycleService = require("./../service/productlifecycleService")


router.get('/productlifecycle/:id', async function (req, res) {
    // console.log(req)
    try {
        const productionLifes = await productlifecycleService.productlifeBy(parseInt(req.params.id))
        res.status(200).json(productionLifes)
    } catch (e) {
        res.status(404).end()
    }
})

router.get('/productlifecycleyear/:year', async function (req, res) {
    try {
        const productionLifes = await productlifecycleService.productlifeAllYear(parseInt(req.params.year))
        res.status(200).json(productionLifes)
    } catch (e) {
        res.status(500).end()
    }
})





module.exports = router