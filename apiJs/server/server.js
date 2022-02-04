const express = require('express')
const app = express()
const body_parser = require("body-parser");



app.use('/',require('./route/productlifecycleRoute'))





 




app.listen(3000)