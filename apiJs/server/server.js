const express = require('express')
const app = express()
const body_parser = require("body-parser");
const cors = require('cors')

app.use(cors())


app.use('/',require('./route/productlifecycleRoute'))





 




app.listen(3000)