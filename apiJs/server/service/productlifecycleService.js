const productlifecycleData = require("../data/productlifecycleData")

exports.productlifeBy = function (id) {
   
    try{
        if(typeof id === 'number')  new Error("Parametes as Not Number")
        return productlifecycleData.productlifeBy(id)
    }catch(e){
        new Error(e)
    }
    

}




exports.productlifeAllYear = async function (year) {
  
    try{
        if(typeof year === 'number') new Error( "Parametes as Not Number")   
        return productlifecycleData.productlifeAllYear(year)
    }catch(e){
        new Error(e)
    }
    
}