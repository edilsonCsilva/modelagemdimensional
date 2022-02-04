const express = require('express')
const router=express.Router()



router.get('/A', function (req, res) {
    res.send('Hello World A')
  })


module.exports=router

